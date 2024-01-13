using Newtonsoft.Json.Linq;
using StudentInformation.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentInformation.DataAccessLayer
{
    public interface IStudentInformationDL
    {
        public Task<StudentInfoResponse> AddStudent(StudentInfo studentInfo);
        public Task AddStudents(List<StudentInfo> students);
        public Task<IList<StudentInfo>> GetAllStudent();
        public Task<IEnumerable<StudentInfo>> GetStudentsByPage(int page, int pageSize);
        public Task<StudentInfo> GetStudent(string id);
        public Task UpdateStudent(StudentInfo student);

        public Task DeleteStudent(string id);
        public Task DeleteAllStudents();
        
    }
}
