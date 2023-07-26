using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using singular_project.Data;
using singular_project.Entities;
using singular_project.Entities.DTOs;
using singular_project.Repositories.Interfaces;
using System.Collections.Generic;
using System.Globalization;

namespace singular_project.Repositories
{
    public class CSVRepository : ICSVRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public CSVRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }



        public async Task<List<Entities.Task>> PostAsync(CSVRequest cSVRequest)
            {

            

            var csv = await _db.Set<CSV>().AddAsync(new CSV { Name = cSVRequest.CSVName });
            // to getting id of csv for its tasks
            await _db.SaveChangesAsync();
            var savedEntity = csv.Entity;

            #region formating date
            string format = "dd.MM.yyyy";
            cSVRequest.Tasks.ForEach(t => t.EndDate = DateTime.ParseExact(
                t.EndDate.ToString(), format, CultureInfo.InvariantCulture).ToString());

            cSVRequest.Tasks.ForEach(t => t.StartDate = DateTime.ParseExact(
                t.StartDate.ToString(), format, CultureInfo.InvariantCulture).ToString());
            #endregion
            var list = _mapper.Map<List<TaskRequest>, List<Entities.Task>>(cSVRequest.Tasks);

            //assign csv id for each task
            list.ForEach(t=>t.CSVId = savedEntity.Id);

            await _db.Set<Entities.Task>().AddRangeAsync(list);
            await _db.SaveChangesAsync();

            return list;
        }

        public async Task<List<Entities.Task>> GetAsync(string name)
        {
            return await _db.Set<Entities.Task>().Include(t=>t.CSV).Where(t=>t.CSV.Name==name).ToListAsync();
        }

        public async Task<List<string>> GetCSVNamesAsync()
        {

            List<string> csvs = await _db.Set<CSV>().Select(c => c.Name).ToListAsync();

            return csvs;
        }

        public async Task<bool> IsCSVNameExist(string name)
        {
            var isExist = await _db.Set<CSV>().AnyAsync(x=>x.Name==name);
            return isExist;
        }
    }
}
