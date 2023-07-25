using AutoMapper;
using singular_project.Entities;
using singular_project.Entities.DTOs;

namespace singular_project.Configurations
{
    public class MapperProfileConfig :Profile
    {
        public MapperProfileConfig() { 

            CreateMap<TaskRequest, Entities.Task>().ReverseMap();
            
        }
    }
}
