using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.Decorator;

public interface IFileImprovement
{
    FileItem FileImprovement();
}

public class FileDecorator : IFileImprovement
{
    private readonly IFileImprovement _file;

    public FileDecorator(IFileImprovement file) => _file = file;

    public virtual FileItem FileImprovement()
    {
        return _file.FileImprovement();
    }
}