using Server.Application;
using Server.Application.Repositories;
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


    public UnitOfWork(AppDbContext dbContext, ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository, 
        IAuthRepository authRepository, IUserRepository userRepository, IFeedbackRepository feedbackRepository)
    {
        _dbContext = dbContext;
        _subCategoryRepository = subCategoryRepository;
        _categoryRepository = categoryRepository;
        _authRepository = authRepository;
        _userRepository = userRepository;
        _feedbackRepository = feedbackRepository;
    }

    public ICategoryRepository categoryRepository => _categoryRepository;

    public ISubCategoryRepository subCategoryRepository => _subCategoryRepository;
    public IAuthRepository authRepository => _authRepository;
    public IUserRepository userRepository => _userRepository;
    public IFeedbackRepository feedbackRepository => _feedbackRepository;

    public async Task<int> SaveChangeAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

}
