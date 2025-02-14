using aPlusApi.Models;
using System.Collections.Generic;

namespace aPlusApi.Interfaces.Business
{
    public interface IStudentBusiness
    {
        string CreateStudent(StudentDTO studentDto);
        StudentListResponse GetAllStudents(int pageNumber, int pageSize); // Alterado para retornar StudentListResponse
        StudentDTO GetStudentByRa(string ra);
        List<StudentDTO> GetStudentByName(string name);
        bool DeleteStudent(string ra);
        bool UpdateStudent(string ra, StudentDTO updatedStudentDto);
    }
}
