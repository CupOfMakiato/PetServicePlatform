using Server.Application;
using Server.Application.Repositories;
using Server.Infrastructure.Repositories;

namespace Server.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;


    public UnitOfWork(AppDbContext dbContext, ISubCategoryRepository subCategoryRepository, ICategoryRepository categoryRepository)
    {
        _dbContext = dbContext;
        _subCategoryRepository = subCategoryRepository;
        _categoryRepository = categoryRepository;

    }

    public ICategoryRepository categoryRepository => _categoryRepository;

    public ISubCategoryRepository subCategoryRepository => _subCategoryRepository;

    public async Task<int> SaveChangeAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

}
