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
            try
            {
                var result = mediator.Send(postProcessorProgram);
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest("Provided program is not supported");
            }
        }

        [HttpPost("/NextStep")]
        public IActionResult NextStep(PostNextStep postNextStep)
        {
            try
            {
                var resutl = mediator.Send(postNextStep);
                return Ok(resutl);
            }
            catch (Exception)
            {

                return BadRequest("Something goes wrong!");
            }
        }
    }
}
