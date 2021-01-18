using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using Shared.Helper;

namespace Task_IP_Magix.Controllers
{
    [Route(APIRoute.Template)]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// Add new student
        /// </summary>
        /// <param name="addEditStudentInputDto"></param>
        /// <returns>true if added successfully , false otherwize</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool),(int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> AddStudent(AddEditStudentInputDto addEditStudentInputDto)
        {
            bool added = await _studentService.AddStudent(addEditStudentInputDto);
            if (added)
                return Ok(added);
            else
                return BadRequest();
        }

        /// <summary>
        /// get specific student by his id
        /// </summary>
        /// <param name="studentIDentityDto"></param>
        /// <returns> returns the data if exists , if not exists returns bad request</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(StudentDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<StudentDto>> GetStudentById(StudentIDentityDto studentIDentityDto)
        {
            StudentDto studentDto = await _studentService.GetStudentById(studentIDentityDto);
            if (studentDto != null)
                return Ok(studentDto);
            else
                return BadRequest();
        }

        /// <summary>
        /// update data of existing student
        /// </summary>
        /// <param name="addEditStudentInputDto"></param>
        /// <returns>true in case of success , false oherwize</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateStudent(AddEditStudentInputDto addEditStudentInputDto)
        {
            bool updated = await _studentService.UpdateStudent(addEditStudentInputDto);
            if (updated)
                return Ok(updated);
            else
                return BadRequest(updated);
        }

        /// <summary>
        /// delete student by his id (soft delete)
        /// </summary>
        /// <param name="studentIDentityDto"></param>
        /// <returns>true in case of success , false oherwize</returns>
        [HttpPost]
        public async Task<ActionResult<bool>> DeleteStudent(StudentIDentityDto studentIDentityDto)
        {
            bool deleted = await _studentService.DeleteStudent(studentIDentityDto);
            if (deleted)
                return Ok(deleted);
            else
                return BadRequest();
        }
        
        /// <summary>
        /// Get all students data
        /// </summary>
        /// <returns> returns data if exists</returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<StudentDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<StudentDto>>> GetAll()
        {
            List<StudentDto> List = await _studentService.GetAll();
            if (List != null)
                return Ok(List);
            else
                return BadRequest();
        }
    }
}