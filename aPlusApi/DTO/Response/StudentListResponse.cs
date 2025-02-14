namespace aPlusApi.Models;

public class StudentListResponse
{
    public List<StudentDTO> Students { get; set; }
    public int TotalCount { get; set; }
}