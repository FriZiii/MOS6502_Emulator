namespace Application.Services
{
    public interface IGenerateMemoryMapService
    {
        IEnumerable<MemoryRowModel> GetMemoryMap();
    }
}