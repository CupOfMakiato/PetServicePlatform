using Server.Application;
using Server.Application.Repositories;
using Server.Infrastructure.Data;
using Server.Infrastructure.Repositories;

namespace Server.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFeedbackRepository _feedbackRepository;
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly ISearchRepository _searchRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IPaymentRepository _paymentRepository;


    public UnitOfWork(AppDbContext dbContext, ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository,
        IAuthRepository authRepository, IUserRepository userRepository, IFeedbackRepository feedbackRepository, IServiceRepository serviceRepository, IBookingRepository bookingRepository, IPaymentRepository paymentRepository)
    {
        _dbContext = dbContext;
        _subCategoryRepository = subCategoryRepository;
        _categoryRepository = categoryRepository;
        _authRepository = authRepository;
        _userRepository = userRepository;
        _feedbackRepository = feedbackRepository;
        _serviceRepository = serviceRepository;
        _searchRepository = searchRepository;

        _bookingRepository = bookingRepository;
        _paymentRepository = paymentRepository;
    }

    public ICategoryRepository categoryRepository => _categoryRepository;

    public ISubCategoryRepository subCategoryRepository => _subCategoryRepository;
    public IAuthRepository authRepository => _authRepository;
    public IUserRepository userRepository => _userRepository;
    public IFeedbackRepository feedbackRepository => _feedbackRepository;
    public IServiceRepository serviceRepository => _serviceRepository;
    public ISearchRepository searchRepository => _searchRepository;
    public IBookingRepository bookingRepository => _bookingRepository;
    public IPaymentRepository paymentRepository => _paymentRepository;

    public async Task<int> SaveChangeAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

}
