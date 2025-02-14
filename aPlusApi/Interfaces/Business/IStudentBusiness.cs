using aPlusApi.Models;
using System.Collections.Generic;

namespace aPlusApi.Interfaces.Business
{
    public interface IStudentBusiness
    {
        string  CreateStudent(StudentDTO studentDto);
        List<StudentDTO> GetAllStudents(int pageNumber, int pageSize);
        StudentDTO GetStudentByRa(string ra);
        List<StudentDTO> GetStudentByName(string name);
        bool DeleteStudent(string ra);
        bool UpdateStudent(string ra, StudentDTO updatedStudentDto);
    }
}
