using Cross_Cutting_Task.Contexts;
using Cross_Cutting_Task.FileItems;

using Microsoft.EntityFrameworkCore;
namespace Cross_Cutting_Task.Repositories;

public class FileItemRepository : IFileItemRepository
{
    private readonly FileItemContext _context;
    public FileItemRepository(FileItemContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }
    public async Task<List<FileItem>> GetAllAsync()
        => await _context.FileItems.ToListAsync();
    public async Task<FileItem> GetItemByIdAsync(int id)
        => await _context.FileItems.FirstAsync(x => x.Id == id);
    public List<FileItem> GetItems()
        => _context.FileItems.ToList();
    public async Task<bool> IsEmpty()
        => await Task.FromResult(_context.FileItems.Count() == 0);
       
    public async Task<bool> AddAsync(FileItem item)
    {
        await _context.FileItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return await Task.FromResult(true);
    }
    public async Task<bool> UpdateAsync(FileItem item)
    {
        _context.Update(item);
        await _context.SaveChangesAsync();
        return await Task.FromResult(true);
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var item = await GetItemByIdAsync(id);
        _context.Remove(item);
        await _context.SaveChangesAsync();
        return await Task.FromResult(true);
    }

    public async Task<bool> MainOperations(FileItem item)
    {
        return await Task.FromResult(true);
    }
}