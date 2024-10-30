using Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Repositories
{
    public interface ISearchRepository
    {
        Task<List<Service>> GetPagedServices(string searchQuery, int pageIndex, int pageSize);
        Task<int> GetTotalServiceCount(string searchQuery);
    }
}
