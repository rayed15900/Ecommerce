using BusinessLogic.DTOs.InventoryDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class InventoryService : Service<InventoryCreateDTO, InventoryReadDTO, InventoryUpdateDTO, Inventory>, IInventoryService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public InventoryService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}
