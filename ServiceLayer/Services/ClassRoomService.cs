using Infrastructure.Interfaces.Repository.Common;
using Infrastructure.Model;
using ServiceLayer.DTOs;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ClassRoomService : IClassRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassRoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddClassRoom(AddEditClassRoomInputDto addEditClassRoomInputDto)
        {
            bool added = default;
            bool isExist = await _unitOfWork.ClassRoom.GetAnyAsync(p => p.Deleted == false && p.Name.ToLower().Trim().Contains(addEditClassRoomInputDto.Name.Trim().ToLower()));
            if (!isExist)
            {
                ClassRoom classRoom = new ClassRoom
                {
                    Name = addEditClassRoomInputDto.Name,
                    Capacity = addEditClassRoomInputDto.Capacity,
                    CreationDate = DateTime.Now
                };
                _unitOfWork.ClassRoom.CreateAsyn(classRoom);
                added = await _unitOfWork.Commit() > default(int);
            }
            return added;
        }

        public async Task<bool> DeleteClassRoom(ClassRoomIDentityDto ClassRoomIDentityDto)
        {
            bool deleted = default;
            ClassRoom classRoom = await _unitOfWork.ClassRoom.FirstOrDefaultAsync(p => p.Id == ClassRoomIDentityDto.Id);
            if (classRoom != null)
            {
                classRoom.Deleted = true;
                _unitOfWork.ClassRoom.Update(classRoom);
                deleted = await _unitOfWork.Commit() > default(int);
            }
            return deleted;
        }
        public async Task<List<ClassRoomDto>> GetAll()
        {
            List<ClassRoom> classRooms = new List<ClassRoom>();
            List<ClassRoomDto> DataList = new List<ClassRoomDto>();
            classRooms = await _unitOfWork.ClassRoom.GetAllAsync();
            if (classRooms?.Any() ?? default)
            {
                DataList = classRooms.Select(p => new ClassRoomDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Capacity = p.Capacity,
                    LastUpdated = p.LastUpdated
                }).ToList();
            }
            return DataList;
        }

        public async Task<ClassRoomDto> GetClassRoomById(ClassRoomIDentityDto classRoomIDentityDto)
        {
            ClassRoomDto classRoomDto = null;
            ClassRoom classRoom = await _unitOfWork.ClassRoom.FirstOrDefaultAsync(p => p.Id == classRoomIDentityDto.Id);
            if (classRoom != null)
                classRoomDto = new ClassRoomDto
                {
                    Id = classRoom.Id,
                    Name = classRoom.Name,
                    Capacity = classRoom.Capacity,
                    LastUpdated = classRoom.LastUpdated.GetValueOrDefault(),
                    CreationDate = classRoom.CreationDate
                };
            return classRoomDto;
        }
        public async Task<bool> UpdateClassRoom(AddEditClassRoomInputDto addEditClassRoomInputDto)
        {
            bool updated = default;
            bool exists = await _unitOfWork.ClassRoom.GetAnyAsync(p =>
                                                                p.Name.Trim().ToLower().Contains(addEditClassRoomInputDto.Name.Trim().ToLower())
                                                                && p.Id != addEditClassRoomInputDto.Id);
            if (!exists)
            {
                ClassRoom classRoom = await _unitOfWork.ClassRoom.FirstOrDefaultAsync(p => p.Id == addEditClassRoomInputDto.Id);
                if (classRoom != null)
                {
                    classRoom.Name = addEditClassRoomInputDto.Name;
                    classRoom.Capacity = addEditClassRoomInputDto.Capacity;
                    classRoom.LastUpdated = DateTime.Now;
                    _unitOfWork.ClassRoom.Update(classRoom);
                    updated = await _unitOfWork.Commit() > default(int);
                }
            }
            return updated;
        }

    }
}
