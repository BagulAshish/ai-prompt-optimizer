using AiPromptOptimizer.Application.DTOs;
using AiPromptOptimizer.Application.Enums;
using AiPromptOptimizer.Application.Interfaces;

namespace AiPromptOptimizer.Application.Services;

public class PromptBuilderService : IPromptBuilderService
{
    private string InitialPromptResponse = @"Return STRICT JSON:
    {{
      ""ImprovedPrompt"": ""..."",
      ""Issues"": [""...""],
      ""Suggestions"": [""...""]
    }}

    Return ONLY valid JSON. No markdown.";

    private string RefinementPromptResponse = @"Return STRICT JSON:
    {{
      ""ImprovedPrompt"": ""..."",
      ""Changes"": [""...""]
    }}

    Return ONLY valid JSON. No markdown.";

    public string BuildInitialPrompt(ChatRequest request)
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
                {request.Messages.First().Content}

                {InitialPromptResponse}",

            PromptCategory.Writing => $@"
                You are a professional writer.

                Improve the following prompt by:
                - Adding tone (formal/casual)
                - Defining audience
                - Structuring format (email, post, etc.)

                Prompt:
                {request.Messages.First().Content}

                {InitialPromptResponse}",


            PromptCategory.Study => $@"
                You are a teacher.

                Improve the prompt by:
                - Asking for simple explanation
                - Adding examples
                - Making it beginner-friendly

                Prompt:
                {request.Messages.First().Content}

                {InitialPromptResponse}",

            PromptCategory.Career => $@"
                You are a career coach.

                Improve the prompt by:
                - Adding experience level
                - Defining target role
                - Asking for actionable advice

                Prompt:
                {request.Messages.First().Content}

                {InitialPromptResponse}",

            PromptCategory.Ideas => $@"
                You are a creative thinker.

                Improve the prompt by:
                - Adding constraints
                - Specifying domain
                - Asking for multiple ideas

                Prompt:
                {request.Messages.First().Content}

                {InitialPromptResponse}",

            _ => request.Messages.First().Content
        };
    }

    public string BuildRefinementPrompt(ChatRequest request)
    {
        var conversation = BuildConversation(request.Messages);

        return request.PromptCategory switch
        {
            PromptCategory.Coding => $@"
                You are an expert software engineer and prompt engineer.

                Refine the coding prompt based on the conversation.

                Focus on:
                - Programming language
                - Error details
                - Expected output
                - Code clarity

                Conversation:
                {conversation}

                Tasks:
                1. Improve the latest prompt
                2. List changes made

                {RefinementPromptResponse}",

            PromptCategory.Writing => $@"
                You are a professional writer and prompt engineer.

                Refine the writing prompt based on the conversation.

                Focus on:
                - Tone (formal/casual)
                - Audience
                - Structure (email, post, etc.)
                - Clarity and conciseness

                Conversation:
                {conversation}

                Tasks:
                1. Improve the latest prompt
                2. List changes made

                {RefinementPromptResponse}",

            PromptCategory.Study => $@"
                You are a teacher and prompt engineer.

                Refine the learning prompt based on the conversation.

                Focus on:
                - Simplicity
                - Step-by-step explanation
                - Examples
                - Beginner friendliness

                Conversation:
                {conversation}

                Tasks:
                1. Improve the latest prompt
                2. List changes made

                {RefinementPromptResponse}",

            PromptCategory.Career => $@"
                You are a career coach and prompt engineer.

                Refine the career prompt based on the conversation.

                Focus on:
                - Experience level
                - Target role
                - Actionable advice
                - Clarity and relevance

                Conversation:
                {conversation}

                Tasks:
                1. Improve the latest prompt
                2. List changes made

                {RefinementPromptResponse}",

            PromptCategory.Ideas => $@"
                You are a creative strategist and prompt engineer.

                Refine the idea generation prompt based on the conversation.

                Focus on:
                - Creativity
                - Constraints
                - Domain clarity
                - Generating unique ideas

                Conversation:
                {conversation}

                Tasks:
                1. Improve the latest prompt
                2. List changes made

                {RefinementPromptResponse}",

            _ => request.Messages.First().Content
        };
    }

    private static string BuildConversation(IList<ChatMessage> messages)
    {
        return string.Join("\n",
            messages.Select(m => $"{m.Role}: {m.Content}")
        );
    }
}