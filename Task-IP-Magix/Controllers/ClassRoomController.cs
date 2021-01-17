using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using Shared.Helper;

namespace Task_IP_Magix.Controllers
{
    [Route(APIRoute.Template)]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
         private readonly IClassRoomService _classRoomService;
        public ClassRoomController(IClassRoomService classRoomService)
        {
            _classRoomService = classRoomService;
        }
        /// <summary>
        /// Add new class room info
        /// </summary>
        /// <param name="addEditClassRoomInputDto"></param>
        /// <returns>true if added successfully , false otherwize</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> AddClassRoom(AddEditClassRoomInputDto addEditClassRoomInputDto)
        {
            bool added = await _classRoomService.AddClassRoom(addEditClassRoomInputDto);
            if (added)
                return Ok(added);
            else
                return BadRequest(added);
        }

        /// <summary>
        /// Get specific classroom by its id
        /// </summary>
        /// <param name="classRoomIDentityDto"></param>
        /// <returns>if exists return the data , bad request if not exist</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ClassRoomDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ClassRoomDto>> GetClassRoomById(ClassRoomIDentityDto classRoomIDentityDto)
        {
            ClassRoomDto ClassRoomDto = await _classRoomService.GetClassRoomById(classRoomIDentityDto);
            if (ClassRoomDto != null)
                return Ok(ClassRoomDto);
            else
                return BadRequest();
        }

        /// <summary>
        /// update the data of existing classroom
        /// </summary>
        /// <param name="addEditClassRoomInputDto"></param>
        /// <returns>return true if the operation completed successfully , false otherwize</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateClassRoom(AddEditClassRoomInputDto addEditClassRoomInputDto)
        {
            bool updated = await _classRoomService.UpdateClassRoom(addEditClassRoomInputDto);
            if (updated)
                return Ok(updated);
            else
                return BadRequest(updated);
        }

        /// <summary>
        /// delete the data of existing classroom (soft delete)
        /// </summary>
        /// <param name="classRoomIDentityDto"></param>
        /// <returns>return true if the operation completed successfully , false otherwize</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteClassRoom(ClassRoomIDentityDto classRoomIDentityDto)
        {
            bool deleted = await _classRoomService.DeleteClassRoom(classRoomIDentityDto);
            if (deleted)
                return Ok(deleted);
            else
                return BadRequest(deleted);
        }

        /// <summary>
        /// return all classrooms
        /// </summary>
        /// <returns>return the data if exists</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(List<ClassRoomDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ClassRoomDto>>> GetAll()
        {
            List<ClassRoomDto> pageList = await _classRoomService.GetAll();
            if (pageList != null)
                return Ok(pageList);
            else
                return BadRequest();
        }
    }
}