using Microsoft.AspNetCore.Mvc;
using singular_project.Entities.DTOs;

namespace singular_project.Repositories.Interfaces
{
    public interface ICSVRepository
    {
        Task<List<Entities.Task>> GetAsync(string name);
        Task<List<Entities.Task>> PostAsync(CSVRequest file);
        Task<List<string>> GetCSVNamesAsync();
        Task<bool> IsCSVNameExist(string name);
    }
}
