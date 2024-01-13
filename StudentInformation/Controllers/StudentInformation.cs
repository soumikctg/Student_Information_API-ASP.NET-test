using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using Microsoft.JSInterop.Implementation;
using Newtonsoft.Json.Linq;
using StudentInformation.DataAccessLayer;
using StudentInformation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentInformation : ControllerBase
    {
        //first step
        private readonly IStudentInformationDL _studentInformationDL;

        public StudentInformation (IStudentInformationDL studentInformationDL)
        {
            _studentInformationDL = studentInformationDL;
        }
        //firststep

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(StudentInfo studentInformation)
        {
            StudentInfoResponse response = new StudentInfoResponse();
            try
            {
                response = await _studentInformationDL.AddStudent(studentInformation);
            }
            catch(Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs" + ex.Message;
            }
            
            
            return Ok(response);
        }

        [HttpPost("AddStudents")]
        public async Task<IActionResult> AddStudents([FromBody] List<StudentInfo> students)
        {
            if(students == null || !students.Any())
            {
                return BadRequest("No students provided");
            }

            await _studentInformationDL.AddStudents(students);


            return Ok(students);
        }

        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent()
        {
           var studentInfos = await _studentInformationDL.GetAllStudent();
           return Ok(studentInfos);
        }

        [HttpGet("GetStudentsByPage")]
        public async Task<IActionResult> GetStudentsByPAge( [FromQuery]int page, [FromQuery] int pageSize)
        {
            var pagedStudents = await _studentInformationDL.GetStudentsByPage(page,pageSize);
            return Ok(pagedStudents);
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudent([FromQuery] string id)
        {
            var student = await _studentInformationDL.GetStudent(id);
            return Ok(student);
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(string id, StudentInfo StudentDto)
        {
            var student = await _studentInformationDL.GetStudent(id);
            
            if(student is null)
            {
                return Ok(NotFound());
            }
            student.FirstName = StudentDto.FirstName;
            student.LastName = StudentDto.LastName;
            student.Contact=StudentDto.Contact;
            student.Address = StudentDto.Address;
            student.Department = StudentDto.Department;

            await _studentInformationDL.UpdateStudent(student);

            return Ok(NoContent());
        }

        [HttpPatch("PartialUpdate")]
        public async Task<IActionResult> PartialUpdate(string id,[FromBody] ContactAddressDto updateDto)
        {
            var student = await _studentInformationDL.GetStudent(id);

            if(student is null)
            {
                return NotFound();
            }

            student.Contact = updateDto.Contact;
            student.Address = updateDto.Address;

            await _studentInformationDL.UpdateStudent(student);

            return Ok(NoContent());
        }


        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            var oldStudent = await _studentInformationDL.GetStudent(id);
            if(oldStudent is null)
            {
                return Ok(NotFound());
            }
            await _studentInformationDL.DeleteStudent(id);
            return Ok(NoContent());
        }


        [HttpDelete("DeleteAllStudents")]
        public async Task<IActionResult> DeleteAllStudents()
        {

            await _studentInformationDL.DeleteAllStudents();
            return Ok(NoContent());
        }

    }
}
