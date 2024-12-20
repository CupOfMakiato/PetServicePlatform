﻿using Server.Application.Common;
using Server.Contracts.Abstractions.Shared;
using Server.Contracts.DTO.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface ISearchService
    {
        Task<Result<object>> SearchServicePagin(int pageIndex, int pageSize, string name);
    }
}
