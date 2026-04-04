using AiPromptOptimizer.Application.DTOs.Prompt;
using AiPromptOptimizer.Application.Enums;
using AiPromptOptimizer.Application.Interfaces;

namespace AiPromptOptimizer.Application.Services;

public class PromptBuilderService : IPromptBuilderService
{
    public string BuildPrompt(PromptRequest request)
    {
        return request.PromptCategory switch
        {
            PromptCategory.Coding => $@"
                You are an expert software engineer.

                Improve the following coding prompt by:
                - Adding programming language
                - Including error details
                - Specifying expected output

                Prompt:
                {request.UserPrompt}

                Return response STRICTLY in JSON format:
                {{
                    ""ImprovedPrompt"": ""..."",
                    ""Issues"": [""...""],
                    ""Suggestions"": [""...""]
                }}
                ",

            PromptCategory.Writing => $@"
                You are a professional writer.

                Improve the following prompt by:
                - Adding tone (formal/casual)
                - Defining audience
                - Structuring format (email, post, etc.)

                Prompt:
                {request.UserPrompt}

                Return response STRICTLY in JSON format:
                {{
                    ""ImprovedPrompt"": ""..."",
                    ""Issues"": [""...""],
                    ""Suggestions"": [""...""]
                }}
                ",

            PromptCategory.Study => $@"
                You are a teacher.

                Improve the prompt by:
                - Asking for simple explanation
                - Adding examples
                - Making it beginner-friendly

                Prompt:
                {request.UserPrompt}

                Return response STRICTLY in JSON format:
                {{
                    ""ImprovedPrompt"": ""..."",
                    ""Issues"": [""...""],
                    ""Suggestions"": [""...""]
                }}
                ",

            PromptCategory.Career => $@"
                You are a career coach.

                Improve the prompt by:
                - Adding experience level
                - Defining target role
                - Asking for actionable advice

                Prompt:
                {request.UserPrompt}

                Return response STRICTLY in JSON format:
                {{
                    ""ImprovedPrompt"": ""..."",
                    ""Issues"": [""...""],
                    ""Suggestions"": [""...""]
                }}
                ",

            PromptCategory.Ideas => $@"
                You are a creative thinker.

                Improve the prompt by:
                - Adding constraints
                - Specifying domain
                - Asking for multiple ideas

                Prompt:
                {request.UserPrompt}

                Return response STRICTLY in JSON format:
                {{
                    ""ImprovedPrompt"": ""..."",
                    ""Issues"": [""...""],
                    ""Suggestions"": [""...""]
                }}
                ",

            _ => request.UserPrompt
        };
    }
}