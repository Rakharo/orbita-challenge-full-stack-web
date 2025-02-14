using aPlusApi.Models;

namespace aPlusApi.Interfaces.Repositories;

public interface IStudentRepository
{
    //creates new student
    string  CreateStudent(Student studentData);
    
    //get all student with pagination
    List<StudentDTO> GetAll(int pageNumber, int pageSize); 

     //Get student by ra
    StudentDTO GetStudentByRa(string ra);
    StudentDTO GetStudentByCpf(string cpf);

    //get student by name
    List<StudentDTO> GetStudentByName(string name);

    //update a student
    bool UpdateStudent(string ra, Student studentData);

    //delete a student
    bool DeleteStudent(string ra);
}
