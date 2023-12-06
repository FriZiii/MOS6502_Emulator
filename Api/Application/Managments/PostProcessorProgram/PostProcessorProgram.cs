using MediatR;

namespace Application.Managments.PostProcessorProgram
{
    public class PostProcessorProgram : IRequest<ProcessorResultDto>
    {
        public string Program { get; set; }
    }
}
