using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Contracts.Generator;
using RealEstate.Generator.Kafka.Host.Generator;

namespace RealEstate.Generator.Kafka.Host.Controllers;

/// <summary>
/// API controller that generates random real-estate request DTOs and publishes them to Kafka in batches.
/// </summary>
/// <param name="producer">Kafka producer used to publish generated messages.</param>
/// <param name="logger">Logger instance.</param>
[ApiController]
[Route("api/[controller]")]
public class GeneratorController(
    KafkaProducer producer,
    ILogger<GeneratorController> logger) : ControllerBase
{
    /// <summary>
    /// Generates and publishes DTOs to Kafka in batches.
    /// </summary>
    /// <param name="totalCount">Total number of items to generate and send.</param>
    /// <param name="batchSize">Number of items per batch.</param>
    /// <param name="delayMs">Delay between batches in milliseconds.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Result containing how many items were sent.</returns>
    [HttpPost("requests")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<GenerateRequestsResultDto>> GenerateRequests(
        [FromQuery] int totalCount,
        [FromQuery] int batchSize,
        [FromQuery] int delayMs,
        CancellationToken cancellationToken)
    {
        if (totalCount <= 0)
            return BadRequest("totalCount must be greater than 0.");

        if (batchSize <= 0)
            return BadRequest("batchSize must be greater than 0.");

        if (delayMs < 0)
            return BadRequest("delayMs must be greater than or equal to 0.");

        logger.LogInformation("Generation requested. TotalCount={TotalCount}, BatchSize={BatchSize}, DelayMs={DelayMs}", totalCount, batchSize, delayMs);

        var sent = 0;
        var batches = 0;

        try
        {
            while (sent < totalCount && !cancellationToken.IsCancellationRequested)
            {
                var remaining = totalCount - sent;
                var currentBatchSize = Math.Min(batchSize, remaining);

                var batch = RequestGenerator.Generate(currentBatchSize);

                await producer.ProduceMany(batch, cancellationToken);

                sent += currentBatchSize;
                batches++;

                logger.LogInformation("Batch sent. BatchNumber={BatchNumber}, BatchSize={BatchSize}, TotalSent={TotalSent}/{TotalCount}", batches, currentBatchSize, sent, totalCount);

                if (sent < totalCount)
                {
                    await Task.Delay(delayMs, cancellationToken);
                }
            }

            logger.LogInformation("Generation finished. TotalSent={TotalSent}, Batches={Batches}", sent, batches);

            return Ok(new GenerateRequestsResultDto(
                TotalRequested: totalCount,
                TotalSent: sent,
                BatchSize: batchSize,
                DelayMs: delayMs,
                Batches: batches,
                Canceled: false
            ));
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Generation was canceled. TotalSent={TotalSent}/{TotalCount}", sent, totalCount);

            return Ok(new GenerateRequestsResultDto(
                TotalRequested: totalCount,
                TotalSent: sent,
                BatchSize: batchSize,
                DelayMs: delayMs,
                Batches: batches,
                Canceled: true
            ));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during generation/publishing. TotalSent={TotalSent}/{TotalCount}", sent, totalCount);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}