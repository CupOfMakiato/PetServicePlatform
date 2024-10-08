﻿using Server.Contracts.DTO.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Contracts.DTO.Category
{
    public class CreateCategoryWithSubDTO
    {
        public CreateCategoryWithSubDTO()
        {
            SubCategories = new HashSet<CreateSubCategoryDTO>();
        }
        public required string CategoryName { get; set; }
        public int CategoryStatus { get; set; }
        public virtual ICollection<CreateSubCategoryDTO> SubCategories { get; set; }
    }
}
