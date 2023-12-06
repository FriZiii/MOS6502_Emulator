using Application.Services;

namespace Application.Managments
{
    public class ProcessorResultDto
    {
        public int Accumulator { get; set; }
        public int YRegister { get; set; }
        public int XRegister { get; set; }
        public int ProgramCounter { get; set; }
        public string Program { get; set; }
        public IEnumerable<MemoryRowModel> MemoryMap { get; set; }
    }
}
