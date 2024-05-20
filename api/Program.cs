using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
using Azure.Storage.Blobs;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddAzureClients(builder =>
        {
            builder.AddBlobServiceClient("<使用するBlob Storage の接続文字列>");
        });

        // Register BlobClient
        services.AddSingleton(x => 
        {
            var blobServiceClient = x.GetRequiredService<BlobServiceClient>();
            var containerClient = blobServiceClient.GetBlobContainerClient("image");
            return containerClient;
        });

        services.AddSingleton<GenerateSASToken>();
    })
    .Build();

host.Run();
