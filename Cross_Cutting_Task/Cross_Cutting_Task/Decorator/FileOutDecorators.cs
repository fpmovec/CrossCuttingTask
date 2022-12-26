using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Cross_Cutting_Task.FileItems;
using Newtonsoft.Json;

namespace Cross_Cutting_Task.Decorator;

class XmlOutDecorator : FileDecorator
{
    public XmlOutDecorator(IFileImprovement file) : base(file) { }

    public override FileItem FileImprovement()
    {
        FileItem improve = base.FileImprovement();

        improve.OutFileName = "D:\\result.xml";
        XmlWriter xmlWriter = XmlWriter.Create(improve.OutFileName);
        xmlWriter.WriteStartDocument();

        xmlWriter.WriteStartElement("Results");

        xmlWriter.WriteStartElement("Result");
        xmlWriter.WriteString(improve.Result.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("MD5_Result");
        xmlWriter.WriteString(improve.EncryptedResult);
        xmlWriter.WriteEndElement();

        xmlWriter.WriteEndElement();
        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
        return improve;
    }
}

class JsonOutDecorator : FileDecorator
{
    public JsonOutDecorator(IFileImprovement file) : base(file) { }

    public override FileItem FileImprovement()
    {
        FileItem improve = base.FileImprovement();

        var json = JsonConvert.SerializeObject(new
        {
            exp = improve.ExpressionParsing(),
            md5exp = improve.EncryptedResult
        },
        Newtonsoft.Json.Formatting.Indented);
        improve.OutFileName = "D:\\JSONResult.json";
        File.WriteAllText(improve.OutFileName, json);
        return improve;
    }    
}

class TxtOutDecorator : FileDecorator
{
    public TxtOutDecorator(IFileImprovement file) : base(file) { }

    public override FileItem FileImprovement()
    {
        FileItem improve = base.FileImprovement();
        improve.OutFileName = "D:\\TxtResult.txt";
        using (StreamWriter output = new StreamWriter(improve.OutFileName))
        {
            output.WriteLine($"Result: {improve.ExpressionParsing()}\n");
            output.WriteLine($"Encrypted result: {improve.EncryptedResult}");
        }
        return improve;
    }
}

class Md5OutDecorator : FileDecorator
{
    public Md5OutDecorator(IFileImprovement file) : base(file) { }
    public override FileItem FileImprovement()
    {
        FileItem improve = base.FileImprovement();
        string exp = improve.ExpressionParsing().ToString();
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(exp));
        improve.EncryptedResult = BitConverter.ToString(checkSum).Replace("-", String.Empty);
        return improve;
    }

}

//class EncryptOutDecorator : FileDecorator
//{
    // public EncryptOutDecorator(IFileImprovement file) : base(file) { }
    // public override FileItem FileImprovement()
    // {
    //     FileItem improve = base.FileImprovement();
    //     File.Encrypt(improve.O);
    // }
//}
class ZipOutDecorator : FileDecorator
{
    public ZipOutDecorator(IFileImprovement file) : base(file) { }

    public override FileItem FileImprovement()
    {
        FileItem improve = base.FileImprovement();

        using (FileStream zipFile = new FileStream("D:\\ZipResult.zip", FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Update))
            {
               archive.CreateEntryFromFile(improve.OutFileName, "result" + Path.GetExtension(improve.OutFileName));
            }
        }

        return improve;
    }
}

// class RarOutDecorator : FileDecorator
// {
//     public RarOutDecorator(IFileImprovement file) : base(file) { }
//
//     public override FileItem FileImprovement()
//     {
//         var improve = base.FileImprovement();
//         System.Diagnostics.ProcessStartInfo sdp = new System.Diagnostics.ProcessStartInfo();
//         string cmdArgs = string.Format("A {0} {1} -a -ep",
//             String.Format("\"{0}\"", @"D:\\resultRAR.rar"),
//             improve.OutFileName);
//         sdp.ErrorDialog = true;
//         sdp.UseShellExecute = true;
//         sdp.Arguments = cmdArgs;
//         sdp.FileName = @"E:\\Programms\\WinRar\\WinRar.exe";//Winrar.exe path
//         sdp.CreateNoWindow = false;
//         sdp.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
//         System.Diagnostics.Process process = System.Diagnostics.Process.Start(sdp);
//         process?.WaitForExit();
//
//         return improve;
//     }
// }