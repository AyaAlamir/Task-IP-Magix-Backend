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
        //private readonly IConfiguration configuration;

        public ClassRoomService(IUnitOfWork unitOfWork)//, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            //this.configuration = configuration;
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
        public async Task<PageList<ClassRoomDto>> GetAll(ClassRoomSearchDto classRoomSearchDto)
        {
            PageList<ClassRoomDto> pageList = new PageList<ClassRoomDto>();
            Expression<Func<ClassRoom, bool>> filter = e => string.IsNullOrWhiteSpace(e.Name) || e.Name.Contains(classRoomSearchDto.Name);
            List<ClassRoom> classRooms = new List<ClassRoom>();
            classRooms = classRoomSearchDto.SortingModel.SortingExpression switch
            {
                "Name" => await _unitOfWork.ClassRoom.GetPageAsync(classRoomSearchDto.PageNumber, classRoomSearchDto.PageSize, filter, p => p.Name, classRoomSearchDto.SortingModel.SortingDirection),
                _ => await _unitOfWork.ClassRoom.GetPageAsync(classRoomSearchDto.PageNumber, classRoomSearchDto.PageSize, filter, p => p.CreationDate, SortDirectionEnum.Descending),
            };
            if (classRooms?.Any() ?? default)
            {
                pageList = new PageList<ClassRoomDto>
                {
                    DataList = classRooms.Select(p => new ClassRoomDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Capacity = p.Capacity,
                        LastUpdated = p.LastUpdated
                    }).ToList(),
                    TotalCount = await _unitOfWork.ClassRoom.GetCountAsync(filter)
                };
            }
            return pageList;
        }

        public Task<ClassRoomDto> GetClassRoomById(ClassRoomIDentityDto ClassRoomIDentityDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProductById(ProductIDentityDto productIDentityDto)
        {
            ProductDto productDto = null;
            Product product = await _unitOfWork.Product.FirstOrDefaultAsync(p => p.Id == productIDentityDto.Id);
            if (product != null)
                productDto = new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Photo = product.Photo,
                    Price = product.Price,
                    LastUpdated = product.LastUpdated.GetValueOrDefault()
                };
            return productDto;
        }

        public Task<bool> UpdateClassRoom(AddEditClassRoomInputDto addEditClassRoomInputDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateProduct(AddEditProductInputDto addEditProductInputDto)
        {
            bool updated = default;
            bool exists = await _unitOfWork.Product.GetAnyAsync(p =>
                                                                p.Name.Trim().ToLower().Contains(addEditProductInputDto.Name.Trim().ToLower())
                                                                && p.Id != addEditProductInputDto.Id);
            if (!exists)
            {
                Product product = await _unitOfWork.Product.FirstOrDefaultAsync(p => p.Id == addEditProductInputDto.Id);
                if (product != null)
                {
                    product.Name = addEditProductInputDto.Name;
                    product.Photo = addEditProductInputDto.Photo;
                    product.Price = addEditProductInputDto.Price;
                    product.LastUpdated = DateTime.Now;
                    _unitOfWork.Product.Update(product);
                    updated = await _unitOfWork.Commit() > default(int);
                }
            }
            return updated;
        }

    }
}
