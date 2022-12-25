using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.Repositories;

public interface IFileItemRepository
{
    Task<bool> AddAsync(FileItem item);
    Task<bool> UpdateAsync(FileItem item);
    Task<List<FileItem>> GetAllAsync();
    List<FileItem> GetItems();
    Task<FileItem> GetItemByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<bool> IsEmpty();
        
}