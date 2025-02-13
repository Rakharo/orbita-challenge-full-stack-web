using aPlusApi.Models;

namespace aPlusApi.Interfaces.Repositories;

public interface IStudentRepository
{
    //creates new student
    long CreateStudent(Student studentData);
    
    //get all student with pagination
    List<StudentDTO> GetAll(int pageNumber, int pageSize); 

     //Get student by ra
    StudentDTO GetStudentById(int ra);
    StudentDTO GetStudentByCpf(string cpf);

    //get student by name
    List<StudentDTO> GetStudentByName(string name);

    //update a student
    bool UpdateStudent(int ra, Student studentData);

    //delete a student
    bool DeleteStudent(int ra);
}
