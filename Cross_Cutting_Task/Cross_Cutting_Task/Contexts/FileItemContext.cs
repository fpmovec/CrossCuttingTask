using Cross_Cutting_Task.FileItems;
using Microsoft.EntityFrameworkCore;
namespace Cross_Cutting_Task.Contexts;

public class FileItemContext : DbContext
{
    public FileItemContext(DbContextOptions<FileItemContext> options)
        : base(options) { }
    public FileItemContext() => Database.EnsureCreated();
    public DbSet<IntermediateClass> FileItems { get; set; }
}