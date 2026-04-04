using AiPromptOptimizer.Application.DTOs;
using AiPromptOptimizer.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AiPromptOptimizer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromptController : ControllerBase
{
    private readonly IPromptService _promptService;

    public PromptController(IPromptService promptService)
    {
        _promptService = promptService;
    }

    [HttpPost("improve")]
    public async Task<IActionResult> ImprovePrompt([FromBody] ChatRequest request)
    {
        var response = await _promptService.GetImprovedPromptAsync(request);
        return Ok(response);
    }
}