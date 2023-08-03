using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using VentionTask.Data;
using VentionTask.Interfaces;

namespace VentionTask.Services
{
    public class CsvReaderService : ICsvReaderService
    {
        public IEnumerable<User> ReadCsv(IFormFile file)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                TrimOptions = TrimOptions.Trim
            };

            List<User> users;

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, configuration))
            {
                users = csv.GetRecords<User>().ToList();
            }

            return users;
        }

    }
}
