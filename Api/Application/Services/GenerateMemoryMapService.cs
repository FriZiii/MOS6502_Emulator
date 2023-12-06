using Domain.Interfaces;

namespace Application.Services
{
    public class GenerateMemoryMapService : IGenerateMemoryMapService
    {
        private readonly IProcessor processor;
        public GenerateMemoryMapService(IProcessor processor)
        {
            this.processor = processor;
        }

        public IEnumerable<MemoryRowModel> GetMemoryMap()
        {
            var memoryPage = new List<MemoryRowModel>();

            var multiplyer = 0;
            for (var i = 0; i < 256; i++)
            {

                memoryPage.Add(new MemoryRowModel
                {
                    Offset = ((16 * multiplyer) + 0).ToString("X").PadLeft(4, '0'),
                    Location00 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location01 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location02 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location03 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location04 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location05 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location06 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location07 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location08 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location09 = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location0A = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location0B = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location0C = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location0D = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location0E = processor.ReadMemoryValue(i++).ToString("X").PadLeft(2, '0'),
                    Location0F = processor.ReadMemoryValue(i).ToString("X").PadLeft(2, '0'),
                });
                multiplyer++;
            }

            return memoryPage;
        }
    }
}
