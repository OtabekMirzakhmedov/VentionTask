using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VentionTask.Data;

namespace VentionTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadUsers(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            try
            {
                var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                    TrimOptions = TrimOptions.Trim
                };

                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, configuration))
                {
                    var users = csv.GetRecords<User>().ToList();
                    foreach (var user in users)
                    {
                        var existingUser = await _context.Users
                            .FirstOrDefaultAsync(u => u.UserName == user.UserName && u.UserIdentifier == user.UserIdentifier);

                        if (existingUser == null)
                            _context.Users.Add(user);
                        else
                        {
                            existingUser.UserName = user.UserName;
                            existingUser.Age = user.Age;
                            existingUser.City = user.City;
                            existingUser.PhoneNumber = user.PhoneNumber;
                            existingUser.Email = user.Email;
                        }
                    }

                    await _context.SaveChangesAsync();
                }

                return Ok("Users uploaded successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
