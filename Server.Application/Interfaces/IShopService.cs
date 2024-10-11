using Server.Contracts.DTO.Email;
using Server.Contracts.DTO.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Application.Interfaces
{
    public interface IShopService
    {
        //APPROVE - REJECT SHOP
        Task ApproveShopAsync(Guid userID);
        Task RejectInstructorAsync(ApproveRejectShopDTO rejectDto);
        //GET PENDING SHOP
        Task<List<PendingShopDTO>> GetPendingShopsAsync();

        Task<string> ChangeStatusShop(ContentEmailDTO contentEmailDto, Guid id);
    }
}
