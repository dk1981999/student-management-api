using AutoMapper;
using student_management_api.Models;
using student_management_api.Models.RequestModel;
using student_management_api.Models.ResponseModel;

namespace student_management_api.EntityMapper
{
    public class EntityModelMapper : Profile
    {
        public EntityModelMapper()
        {
            CreateMap<StudentModel, StudentRequestModel>().ReverseMap();

            CreateMap<StudentModel, StudentUpdateRequestModel>().ReverseMap();

            CreateMap<StudentModel, StudentListModel>()
                .ForMember(d => d.firstGraduate, o => o.MapFrom(s => (s.firstGraduate == true) ? "Yes" : "No"));
        }
    }
}
