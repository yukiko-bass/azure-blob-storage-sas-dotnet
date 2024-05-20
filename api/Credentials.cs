using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobStorageSAS.Functions;
public class Credentials
{
    private readonly ILogger<Credentials> _logger;
    private readonly BlobContainerClient _blobContainerClient;
    private readonly GenerateSASToken _generateSASToken;

    public Credentials(ILogger<Credentials> logger, BlobContainerClient blobContainerClient, GenerateSASToken generateSASToken)
    {
        _logger = logger;
        _blobContainerClient = blobContainerClient;
        _generateSASToken = generateSASToken;
    }

    [Function("credentials")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var sasuri = _generateSASToken.CreateServiceSASBlob(_blobContainerClient, null);
        return new OkObjectResult(new { sasuri = sasuri });
    }
}

