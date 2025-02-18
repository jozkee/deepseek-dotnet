# Building intelligent apps with .NET, MEAI, and DeepSeek

Thanks to the Microsoft.Extensions.AI library it's pretty straight forward to build applications that make use of the new DeepSeek R1 model.

In fact, using MEAI it's straight forward to make use of any model, whether it's hosted in GitHub Models, Azure AI Foundry, or even running locally in Ollama.

That's what this repo is all about - using MEAI to build a quick .NET Console to access the DeepSeek R1 model.

See the accompanying [blog post to walk you through the details](https://devblogs.microsoft.com/dotnet/start-building-an-intelligent-app-with-dotnet-and-deep-seek/).

## The source

[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/codemillmatt/deepseek-dotnet)

You can run the applications any way you like, but you may find it easiest to run them via Codespaces as that will setup all the dependencies for you, including setting up the GitHub Personal Access Token automatically so you can get going with GH Models without any friction at all.

> Note: If you do go the Codespaces route, just know it will take a couple of minutes to pull down Ollama and the DeepSeek R1 model and it uses about 8GB of space. So you may want to delete the Codespace after you're finished with it depending on your plan.

### Why MEAI

The Microsoft.Extensions.AI library provides a common set of abstractions over AI services. This means that no matter which service you're using - DeepSeek, OpenAI, Mistral - or where it's hosted - GH Models, Ollama, Azure AI Foundry - you get to the use the same API surface to interact with the model. This lowers the friction of getting started - you don't have to remember the particulars of many different libraries - just one, MEAI.

And most functionality that you will use comes from the `IChatClient` interface. Since it is an interface, it needs to be instantiated as a concrete implementation - as that's the one thing that will be different based on the underlying AI Service.

So if you're using Ollama - you'll use the **Microsoft.Extensions.AI.Ollama** package to create the `IChatClient`. If you're using GitHub Models, it'll be **Microsoft.Extensions.AI.AzureAIInference**. But once you instantiate `IChatClient` - you're off to the races.

### Running with GitHub Models

GitHub Models are a great way to explore various AI models quickly. DeepSeek R1 is no exception and we can build an app quickly with MEAI.

You'll need a GitHub Personal Access Token to get started. You can follow the [steps here](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/managing-your-personal-access-tokens#creating-a-personal-access-token-classic) ... OR ... just [run this repo in a Codespace](https://codespaces.new/codemillmatt/deepseek-dotnet) and it will take care of all that for you.

If you do run locally, make sure you setup a user secret in the **DeepSeek.Console.GHModels** project with the key name of `GITHUB_TOKEN` and the value of the PAT.

Then check out **[Program.cs](https://github.com/codemillmatt/deepseek-dotnet/blob/d9754a47c61e8da00015e1bcc3b282502dee4fd7/src/DeepSeek.Console.GHModels/Program.cs#L22)** for how to use MEAI to access DeepSeek on GH Models.

### Running on Azure AI Foundry

The major - and only difference - between running DeepSeek on GitHub Models and Azure AI Foundry is deploying the model onto Azure AI Foundry. [This post](https://azure.microsoft.com/blog/deepseek-r1-is-now-available-on-azure-ai-foundry-and-github/) gives you the directions to deploy.

Once you do, make note of the key and put it into the user secrets of the **DeepSeek.Console.AzureAI** project. Name the secret's key `AZURE_AI_KEY` and put the key as its value.

You'll get the endpoint URL and model name from the deployment on the AI Foundry portal.

### Running on Ollama

You don't need the cloud involved to create a .NET application to access DeepSeek. In fact you can do everything locally with Ollama. I have set things up so you can run this part easily in [Codespaces](https://codespaces.new/codemillmatt/deepseek-dotnet).

The Codespace will download the Ollama image and start it up as a container inside your devcontainer. Then it will pull a distilled version of the R1 model. For our purposes, distilled means smaller.

Once the devcontainer/Codespace finishes provisioning checkout **Program.cs** from the **DeepSeek.Console.Ollama** project. It has a different NuGet installed **OllamaSharp**.

And to initialize the `IClient` interface:

```csharp
IChatClient chatClient = new OllamaApiClient(modelEndpoint, modelName);
```

## Summary

So give this all a run through. It's really more about using MEAI than it is about DeepSeek to be honest. I'll add in some more example's - including how to use Ollama with .NET Aspire - it's pretty slick!
