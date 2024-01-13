using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentInformation.Model
{
    public class StudentInfo
    {
        public StudentInfo() 
        {
            Id = Guid.NewGuid().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string? Id { get; set; }
        [Required]
        [BsonElement("Name")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Contact {  get; set; }

        public string Address { get; set; }
        public string Department { get; set; }

    }

    public class StudentInfoResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static implicit operator StudentInfoResponse(List<StudentInfo> v)
        {
            throw new NotImplementedException();
        }
    }
}
