﻿using BusinessLogic.DTOs.OrderDTOs;
using BusinessLogic.IServices;
using BusinessLogic.Services.Base;
using DataAccess.UnitOfWork.Interface;
using MapsterMapper;
using Models;

namespace BusinessLogic.Services
{
    public class OrderService : Service<OrderCreateDTO, OrderReadDTO, OrderUpdateDTO, Order>, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public OrderService(IMapper mapper, IUOW uow) : base(mapper, uow)
        {
            _mapper = mapper;
            _uow = uow;
        }
    }
}