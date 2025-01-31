using Azure;
using Azure.AI.Inference;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

var builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();

var configuration = builder.Build();

// These variables are needed to access the GitHub Models
string token = configuration["GH_TOKEN"] ?? 
    throw new InvalidOperationException("Make sure to add GH_TOKEN value to the user secrets.");

AzureKeyCredential credential = new(token);
Uri modelEndpoint = new ("https://models.inference.ai.azure.com");
string modelName = "DeepSeek-R1";


IChatClient chatClient = new ChatCompletionsClient(modelEndpoint, credential)
    .AsChatClient(modelName);

string question = "If I have 3 apples and eat 2, how many bananas do I have?";
var response = chatClient.CompleteStreamingAsync(question);

Console.WriteLine($">>> User: {question}");
Console.Write(">>>");
Console.WriteLine(">>> DeepSeek (might be a while): ");

await foreach (var item in response)
{
    Console.Write(item);
}