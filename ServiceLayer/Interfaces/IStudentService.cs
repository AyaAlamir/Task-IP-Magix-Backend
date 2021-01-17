using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IStudentService
    {
        Task<bool> AddStudent(AddEditStudentInputDto addEditStudentInputDto);
        Task<StudentDto> GetStudentById(StudentIDentityDto StudentIDentityDto);
        Task<bool> UpdateStudent(AddEditStudentInputDto addEditStudentInputDto);
        Task<bool> DeleteStudent(StudentIDentityDto StudentIDentityDto);
        Task<List<StudentDto>> GetAll();
    }
}
