using ServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IClassRoomService
    {
        Task<bool> AddClassRoom(AddEditClassRoomInputDto addEditClassRoomInputDto);
        Task<ClassRoomDto> GetClassRoomById(ClassRoomIDentityDto ClassRoomIDentityDto);
        Task<bool> UpdateClassRoom(AddEditClassRoomInputDto addEditClassRoomInputDto);
        Task<bool> DeleteClassRoom(ClassRoomIDentityDto ClassRoomIDentityDto);
        Task<List<ClassRoomDto>> GetAll();
    }
}
