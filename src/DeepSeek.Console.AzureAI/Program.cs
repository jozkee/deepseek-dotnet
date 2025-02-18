using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var configuration = builder.Build();

// Make sure the Azure key is set in the user secrets
string azureKey = configuration["AZURE_AI_KEY"] ??
    throw new InvalidOperationException("Make sure to add AZURE_AI_KEY value to the user secrets.");

// These variables are needed to access the GitHub Models
AzureKeyCredential credential = new(azureKey);
Uri modelEndpoint = new("GET THIS VALUE FROM YOUR DEPLOYMENT IN AZURE AI FOUNDRY");
string modelName = "GET THIS VALUE FROM YOUR DEPLOYMENT IN AZURE AI FOUNDRY";


IChatClient chatClient = new ChatCompletionsClient(modelEndpoint, credential)
    .AsChatClient(modelName);

string question = "If I have 3 apples and eat 2, how many bananas do I have?";
var response = chatClient.GetStreamingResponseAsync(question);

Console.WriteLine($">>> User: {question}");
Console.Write(">>>");
Console.WriteLine(">>> DeepSeek (might be a while): ");

await foreach (var item in response)
{
    Console.Write(item);
}