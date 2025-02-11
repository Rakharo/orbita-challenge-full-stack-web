using aPlusApi.Models;
using System.Collections.Generic;

namespace aPlusApi.Interfaces.Business
{
    public interface IStudentBusiness
    {
        long CreateStudent(Student student);
        List<StudentDTO> GetAllStudents(int pageNumber, int pageSize);
        StudentDTO GetStudentById(int ra);
        List<StudentDTO> GetStudentByName(string name);
        bool DeleteStudent(int ra);
        bool UpdateStudent(int ra, Student updatedStudent);
    }
}
