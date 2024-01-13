using StudentInformation.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentInformation.DataAccessLayer
{
    public interface IStudentInformationDL
    {
        public Task<StudentInfoResponse> AddStudent(StudentInfo studentInfo);
        public Task<IList<StudentInfo>> GetAllStudent();
        public Task<StudentInfo> GetStudent(string id);
        
    }
}
