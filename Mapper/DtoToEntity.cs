using AutoMapper;
using Mobiroller.Common;
using Mobiroller.Data.Entity;

namespace Mobiroller.Mapper
{
    public class DtoToEntity : Profile
    {
        public DtoToEntity()
        {
            CreateMap<ImportIncidentsModel, Incident>();
        }
    }
}