using MediatR;

namespace Application.Managments.PostNextStep
{
    public class PostNextStep : IRequest<ProcessorResultDto>
    {
        public string Program { get; set; }
        public int ProgramCounter { get; set; }
    }
}
