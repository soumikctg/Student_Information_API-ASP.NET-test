using MongoDB.Bson;
using MongoDB.Driver;
using StudentInformation.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentInformation.DataAccessLayer
{
    public class StudentProfile : IStudentProfile
    {
        private readonly IMongoCollection<StudentInfo> _mongoCollection;
        private readonly FilterDefinitionBuilder<StudentInfo> _filterBuilder = Builders<StudentInfo>.Filter;
        private const string CollectionName = "StudentDetails";
        private const string DatabaseName = "StudentInfoDB";
        public StudentProfile(IMongoClient _mongoClient)
        {
            var _MongoDatabase = _mongoClient.GetDatabase(DatabaseName);
            _mongoCollection = _MongoDatabase.GetCollection<StudentInfo>(CollectionName);

        }
        //first step
        public async Task<StudentInfoResponse> AddStudent(StudentInfo studentInfo)
        {
            StudentInfoResponse response = new StudentInfoResponse();
            response.IsSuccess = true;
            response.Message = "Data Successfully Inserted";
            try
            {
                await _mongoCollection.InsertOneAsync(studentInfo);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task AddStudents(List<StudentInfo> students)
        {

            await _mongoCollection.InsertManyAsync(students);
        }


        public async Task<IList<StudentInfo>> GetAllStudent()
        {
            return await _mongoCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<IEnumerable<StudentInfo>> GetStudentsByPage(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            return await _mongoCollection.Find(_ => true).Skip(skip).Limit(pageSize).ToListAsync();
        }

        public async Task<StudentInfo> GetStudent(string id)
        {
            var filter = _filterBuilder.Eq(StudentInfo => StudentInfo.Id, id);
            return await _mongoCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task UpdateStudent(StudentInfo student)
        {
            var filter = _filterBuilder.Eq(studentInfo => studentInfo.Id, student.Id);
            await _mongoCollection.ReplaceOneAsync(filter, student);
        }

        public async Task DeleteStudent(string id)
        {
            var filter = _filterBuilder.Eq(StudentInfo => StudentInfo.Id, id);
            await _mongoCollection.DeleteOneAsync(filter);
        }

        public async Task DeleteAllStudents()
        {
            await _mongoCollection.DeleteManyAsync(new BsonDocument());
        }

    }
}
