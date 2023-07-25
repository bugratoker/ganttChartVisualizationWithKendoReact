using Microsoft.AspNetCore.Mvc;
using singular_project.Entities;
using singular_project.Entities.DTOs;

namespace singular_project.Services.Interfaces
{
    public interface ICSVService
    {
        Task<List<Entities.Task>> Get(string name);
        Task<List<Entities.Task>> Post(CSVRequest cSVRequest);
        Task<List<string>> GetCSVNames();
    }
}
