using Server.Application.Interfaces;
using Server.Application.Repositories;
using Server.Domain.Entities;
using Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.Repositories
{
    public class PaymentRepository : GenericRepository<ApplicationUser>, IPaymentRepository
    {
        private readonly AppDbContext _appDbContext;

        public PaymentRepository(AppDbContext appDbContext, ICurrentTime currentTime) : base(appDbContext, currentTime)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddPayment(Payment payment)
        {
            await _appDbContext.Payment.AddAsync(payment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdatePayment(Payment payment)
        {
            _appDbContext.Payment.Update(payment);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
