using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VentionTask.Data;
using VentionTask.Interfaces;

namespace VentionTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ICsvReaderService _csvReader;
        private readonly IUserRepository _userRepository;

        public UsersController(ICsvReaderService csvReader, IUserRepository userRepository)
        {
            _csvReader = csvReader;
            _userRepository = userRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadUsers(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            try
            {
                var users = _csvReader.ReadCsv(file);
                await _userRepository.AddOrUpdateUsersAsync(users);
                return Ok("Users uploaded successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAscending(int limit = 10)
        {
            try
            {
                var users = await _userRepository.GetUsersAscending(limit);

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
