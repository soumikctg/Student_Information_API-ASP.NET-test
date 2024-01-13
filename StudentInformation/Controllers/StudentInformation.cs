using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentInformation.DataAccessLayer;
using StudentInformation.Model;
using System;
using System.Collections.Generic;
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
        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent()
        {
           var studentInfos = await _studentInformationDL.GetAllStudent();
           return Ok(studentInfos);
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudent([FromQuery] string id)
        {
            var student = await _studentInformationDL.GetStudent(id);
            return Ok(student);
        }

    }
}
