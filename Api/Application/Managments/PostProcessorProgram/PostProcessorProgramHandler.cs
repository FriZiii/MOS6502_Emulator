using Application.Services;
using Domain.Interfaces;
using MediatR;

namespace Application.Managments.PostProcessorProgram
{
    public class PostProcessorProgramHandler : IRequestHandler<PostProcessorProgram, ProcessorResultDto>
    {
        private readonly IProcessor processor;
        private readonly IGenerateMemoryMapService generateMemoryMapService;

        public PostProcessorProgramHandler(IProcessor processor, IGenerateMemoryMapService generateMemoryMapService)
        {
            this.processor = processor;
            this.generateMemoryMapService = generateMemoryMapService;
        }

        public async Task<ProcessorResultDto> Handle(PostProcessorProgram request, CancellationToken cancellationToken)
        {
            processor.ClearMemory();
            processor.ClearState();
            processor.Reset();
            byte[] program = Array.ConvertAll(request.Program.Split(','), byte.Parse);
            processor.LoadProgram(0, program);

            var dto = new ProcessorResultDto()
            {
                ProgramCounter = processor.ProgramCounter,
                Program = request.Program,

                XRegister = processor.XRegister,
                Accumulator = processor.Accumulator,
                YRegister = processor.YRegister,
                MemoryMap = generateMemoryMapService.GetMemoryMap(),
                CarryFlag = processor.CarryFlag,
                DecimalFlag = processor.DecimalFlag,
                NegativeFlag = processor.NegativeFlag,
                OverflowFlag = processor.OverflowFlag,
                ZeroFlag = processor.ZeroFlag,
            };

            return dto;
        }
    }
}
