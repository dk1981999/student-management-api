using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using student_management_api.Models;
using student_management_api.Models.RequestModel;
using student_management_api.Models.ResponseModel;
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

        /// <summary>
        /// Get list of students.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult getStudents()
        {
            var objList = _studentRepo.GetStudentsAsync();

            var result = new List<StudentListModel>();

            foreach (var obj in objList)
            {
                result.Add(_mapper.Map<StudentListModel>(obj));
            }
            return Ok(result);
        }

        /// <summary>
        /// Get individual student
        /// </summary>
        /// <param name="id">The id of the student</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult getStudentById(int id)
        {
            var result = _studentRepo.GetStudentByIdAsync(id);
            if (result == null)
            {
                return NotFound($"{id} Student does not exist");
            }
            return Ok(result);
        }

        /// <summary>
        /// Post Student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult postStudent(StudentRequestModel model)
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

        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="id">The id of the student</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult putStudent(int id, StudentUpdateRequestModel model)
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

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="id">The id of the student</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
