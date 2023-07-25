using Microsoft.AspNetCore.Mvc;
using singular_project.Entities.DTOs;
using singular_project.Repositories.Interfaces;
using singular_project.Services.Interfaces;

namespace singular_project.Services
{
    public class CSVService : ICSVService
    {
        public ICSVRepository CSVRepository { get; set; }
        public CSVService(ICSVRepository cSVRepository)
        {
            CSVRepository = cSVRepository;
        }
        public async Task<List<Entities.Task>> Get(string name)
        {
            var tasks = await CSVRepository.GetAsync(name);
            return tasks;
        }

        public async Task<List<Entities.Task>> Post(CSVRequest cSVRequest)
        {
            return await CSVRepository.PostAsync(cSVRequest);
        }

        public async Task<List<string>> GetCSVNames()
        {
            return await CSVRepository.GetCSVNamesAsync();
        }
    }
}
