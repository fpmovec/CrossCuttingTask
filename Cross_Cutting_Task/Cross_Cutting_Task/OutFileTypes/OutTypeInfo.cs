using Cross_Cutting_Task.FileItems;

namespace Cross_Cutting_Task.OutFileTypes;

public class OutTypeInfo
{
    private Dictionary<string, IOutFileTypes> _types;

    public OutTypeInfo()
    {
        _types = new Dictionary<string, IOutFileTypes>(3);
        _types.Add("TXT", new OutTxtType { });
        _types.Add("JSON", new OutJsonType { });
        _types.Add("XML", new OutXmlType { });
    }

    public void TypeOut(string type, FileItem item)
    {
        _types.TryGetValue(type.ToUpper(), out IOutFileTypes? fileTypes);
            fileTypes?.Type(item);
    }
    
}