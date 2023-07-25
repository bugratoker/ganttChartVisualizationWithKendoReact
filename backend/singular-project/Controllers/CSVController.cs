using Microsoft.AspNetCore.Mvc;
using singular_project.Entities;
using singular_project.Entities.DTOs;
using singular_project.Services.Interfaces;

namespace singular_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        private readonly ICSVService csvService;
        public CSVController(ICSVService csvService)
        {
            this.csvService = csvService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CSVRequest csvRequest)
        {
            return Ok(await csvService.Post(csvRequest));
        }


        [HttpGet]
        public async Task<List<Entities.Task>> Get(string csvName)
        {

            return await csvService.Get(csvName);
        }
        [HttpGet("/getCSVNames")]
        public async Task<List<string>> GetAllCSVs()
        {
            return await csvService.GetCSVNames();
        }

    }
}
