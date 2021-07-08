using AutoMapper;
using student_management_api.Models;
using student_management_api.Models.Dtos;

namespace student_management_api.EntityMapper
{
    public class EntityModelMapper : Profile
    {
        public EntityModelMapper()
        {
            CreateMap<StudentModel, StudentModelDto>().ReverseMap();
        }
    }
}
