using AutoMapper;
using Mobiroller.Data.Entity;
using Mobiroller.DTOs;

namespace Mobiroller.Mapper
{
    public class EntityToDtoMapper : Profile
    {
        public EntityToDtoMapper()
        {
            CreateMap<Incident, IncidentDto>();
        }
    }
}