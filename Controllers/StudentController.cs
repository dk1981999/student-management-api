using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using student_management_api.Models;
using student_management_api.Models.Dtos;
using student_management_api.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_management_api.Controllers
{
    public class StudentController : BaseApiController
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepo studentRepo, IMapper mapper)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult getStudents()
        {
            var objList = _studentRepo.GetStudentsAsync();

            var result = new List<StudentModelDto>();

            foreach (var obj in objList)
            {
                result.Add(_mapper.Map<StudentModelDto>(obj));
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult getStudentById(int id)
        {
            var result = _studentRepo.GetStudentByIdAsync(id);
            if (result == null)
            {
                return NotFound($"{id} Student does not exist");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult postStudent(StudentModelDto model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }

            var obj = _mapper.Map<StudentModel>(model);
            if (!_studentRepo.CreateStudentAsync(obj))
            {
                ModelState.AddModelError("", $"Unable to create student, please contact support");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult putStudent(int id, StudentModelDto model)
        {
            if (model == null || id != model.id)
            {
                return BadRequest("Id and Model Id do not match");
            }
            if (!_studentRepo.ExistAsync(id))
            {
                return NotFound($"{id} Student does not exist");
            }

            var obj = _mapper.Map<StudentModel>(model);

            if (!_studentRepo.UpdateStudentAsync(obj))
            {
                ModelState.AddModelError("", $"Unable to update student, please contact support");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {
            if (!_studentRepo.ExistAsync(id))
            {
                return NotFound($"{id} Student does not exist");
            }

            var result = _studentRepo.GetStudentByIdAsync(id);
            if (!_studentRepo.DeleteStudentAsync(result))
            {
                ModelState.AddModelError("", $"Unable to delete student");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
