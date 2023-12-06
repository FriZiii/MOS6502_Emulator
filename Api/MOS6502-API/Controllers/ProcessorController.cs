using Application.Managments.PostNextStep;
using Application.Managments.PostProcessorProgram;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MOS6502_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessorController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator mediator = mediator;

        [HttpPost("/LoadProgram")]
        public IActionResult LoadProgram(PostProcessorProgram postProcessorProgram)
        {
            var result = mediator.Send(postProcessorProgram);
            return Ok(result);
        }

        [HttpPost("/NextStep")]
        public IActionResult NextStep(PostNextStep postNextStep)
        {
            var resutl = mediator.Send(postNextStep);
            return Ok(resutl);
        }
    }
}
