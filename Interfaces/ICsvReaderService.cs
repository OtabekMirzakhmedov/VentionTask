using VentionTask.Data;

namespace VentionTask.Interfaces
{
    public interface ICsvReaderService
    {
        IEnumerable<User> ReadCsv(IFormFile file);
    }
}
