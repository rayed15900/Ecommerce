﻿using BusinessLogic.DTOs.Interfaces;
using Models;

namespace BusinessLogic.DTOs.CategoryDTOs
{
    public class CategoryUpdateDTO : IUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
