using aPlusApi.Models;
using System.Collections.Generic;

namespace aPlusApi.Interfaces.Business
{
    public interface IStudentBusiness
    {
        long CreateStudent(StudentDTO studentDto);
        List<StudentDTO> GetAllStudents(int pageNumber, int pageSize);
        StudentDTO GetStudentByRa(int ra);
        List<StudentDTO> GetStudentByName(string name);
        bool DeleteStudent(int ra);
        bool UpdateStudent(int ra, StudentDTO updatedStudentDto);
    }
}
