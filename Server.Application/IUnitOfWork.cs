using Server.Application.Repositories;

namespace Server.Application;

public interface IUnitOfWork
{
    Task<int> SaveChangeAsync();
    ICategoryRepository categoryRepository { get; }
    ISubCategoryRepository subCategoryRepository { get; }
    IFeedbackRepository feedbackRepository { get; }
}
