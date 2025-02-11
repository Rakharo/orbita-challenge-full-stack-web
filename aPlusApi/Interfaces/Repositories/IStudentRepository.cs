using aPlusApi.Models;

namespace aPlusApi.Interfaces.Repositories;

public interface IStudentRepository
{
    long CreateStudent(Student studentData); // Criar um novo estudante
    List<StudentDTO> GetAll(int pageNumber, int pageSize); // Paginação ao buscar todos

    StudentDTO GetStudentById(int ra); // Buscar estudante pelo RA
    List<StudentDTO> GetStudentByName(string name); // Buscar estudante pelo nome

    bool UpdateStudent(int ra, Student studentData); // Atualizar um estudante
    bool DeleteStudent(int ra); // Deletar um estudante
}
