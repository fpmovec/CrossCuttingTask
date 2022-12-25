using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.Repositories;

public interface IFileItemRepository
{
    Task<bool> AddAsync(IntermediateClass item);
    Task<bool> UpdateAsync(IntermediateClass item);
    Task<List<IntermediateClass>> GetAllAsync();
    List<IntermediateClass> GetItems();
    Task<IntermediateClass> GetItemByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<bool> IsEmpty();
    Task<bool> MainOperations(IntermediateClass item);

}