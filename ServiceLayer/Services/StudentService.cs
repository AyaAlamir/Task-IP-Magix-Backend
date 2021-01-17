using Infrastructure.Interfaces.Repository.Common;
using Infrastructure.Model;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddStudent(AddEditStudentInputDto addEditStudentInputDto)
        {
            bool added = default;
            bool isExist = await _unitOfWork.Student.GetAnyAsync(p => p.Deleted == false && p.Name.ToLower().Trim().Contains(addEditStudentInputDto.Name.Trim().ToLower()));
            if (!isExist)
            {
                Student student = new Student
                {
                    Name = addEditStudentInputDto.Name,
                    BirthDate = addEditStudentInputDto.BirthDate,
                    CreationDate = DateTime.Now
                };
                _unitOfWork.Student.CreateAsyn(student);
                added = await _unitOfWork.Commit() > default(int);
            }
            return added;
        }

        public async Task<bool> DeleteStudent(StudentIDentityDto StudentIDentityDto)
        {
            bool deleted = default;
            Student student = await _unitOfWork.Student.FirstOrDefaultAsync(p => p.Id == StudentIDentityDto.Id);
            if (student != null)
            {
                student.Deleted = true;
                _unitOfWork.Student.Update(student);
                deleted = await _unitOfWork.Commit() > default(int);
            }
            return deleted;
        }
        public async Task<List<StudentDto>> GetAll()
        {
            List<Student> students = new List<Student>();
            List<StudentDto> DataList = new List<StudentDto>();
            students = await _unitOfWork.Student.GetAllAsync();
            if (students?.Any() ?? default)
            {
                DataList = students.Select(p => new StudentDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    BirthDate = p.BirthDate,
                    CreationDate = p.CreationDate,
                    LastUpdated = p.LastUpdated
                }).ToList();
            }
            return DataList;
        }
        public async Task<StudentDto> GetStudentById(StudentIDentityDto StudentIDentityDto)
        {
            StudentDto studentDto = null;
            Student student = await _unitOfWork.Student.FirstOrDefaultAsync(p => p.Id == StudentIDentityDto.Id);
            if (student != null)
                studentDto = new StudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    BirthDate = student.BirthDate,
                    LastUpdated = student.LastUpdated.GetValueOrDefault(),
                    CreationDate = student.CreationDate
                };
            return studentDto;
        }

        public async Task<bool> UpdateStudent(AddEditStudentInputDto addEditStudentInputDto)
        {
            bool updated = default;
            bool exists = await _unitOfWork.Student.GetAnyAsync(p =>
                                                                p.Name.Trim().ToLower().Contains(addEditStudentInputDto.Name.Trim().ToLower())
                                                                && p.Id != addEditStudentInputDto.Id);
            if (!exists)
            {
                Student student = await _unitOfWork.Student.FirstOrDefaultAsync(p => p.Id == addEditStudentInputDto.Id);
                if (student != null)
                {
                    student.Name = addEditStudentInputDto.Name;
                    student.BirthDate = addEditStudentInputDto.BirthDate;
                    student.LastUpdated = DateTime.Now;
                    _unitOfWork.Student.Update(student);
                    updated = await _unitOfWork.Commit() > default(int);
                }
            }
            return updated;
        }
    }
}
