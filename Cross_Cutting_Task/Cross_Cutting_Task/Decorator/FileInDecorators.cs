using System.IO.Compression;
using System.Text.Json;
using System.Xml;
using Cross_Cutting_Task.FileItems;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SharpCompress.Archives.Rar;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Cross_Cutting_Task.Decorator;

public class ZipInDecorator : FileDecorator
{
    public ZipInDecorator(IFileImprovement file) :  base(file) {}

    public override FileItem FileImprovement()
    {
        var improve = base.FileImprovement();
        
          File.Decrypt(improve.InFilePath);
       if (improve.archiveInStream == null)    
           improve.archiveInStream = File.OpenRead(improve.InFilePath ?? string.Empty);
        ZipArchive zip = new ZipArchive(improve.archiveInStream, ZipArchiveMode.Read);
       
        foreach (var item in zip.Entries)
        {
            if (Path.GetExtension(item.FullName).ToUpper() == ".ZIP")
            {
                improve.archiveInStream = item.Open();
                return new ZipInDecorator(improve).FileImprovement();
            }
            if (Path.GetExtension(item.FullName).ToUpper() == ".RAR")
            {
                improve.archiveInStream = item.Open();
                return new RarInDecorator(improve).FileImprovement();
            }
            improve.archiveInStream = item.Open();
        }

        return improve;
    }
}

public class RarInDecorator : FileDecorator
{
    public RarInDecorator(IFileImprovement file) : base(file) { }
    public override FileItem  FileImprovement()
    {
        FileItem improve = base.FileImprovement();
        
        
           improve.archiveInStream = File.OpenRead(improve.InFilePath);
        
        RarArchive rar = RarArchive.Open(improve.archiveInStream);
        foreach (RarArchiveEntry entry in rar.Entries)
        {
            if (Path.GetExtension(entry.Key).ToUpper() == ".ZIP")
            {
                improve.archiveInStream = entry.OpenEntryStream();
                return new ZipInDecorator(improve).FileImprovement();
            }
            if (Path.GetExtension(entry.Key).ToUpper() == ".RAR")
            {
                improve.archiveInStream = entry.OpenEntryStream();
                return new RarInDecorator(improve).FileImprovement();
            }
            improve.archiveInStream = entry.OpenEntryStream();
        }
        return improve;
    }

}

 class TxtInDecorator : FileDecorator
    {
        public TxtInDecorator(IFileImprovement file) : base(file) { }

        public override FileItem FileImprovement()
        {
            var improve = base.FileImprovement();
            if (improve.archiveInStream != null)
            {
                using (StreamReader input = new StreamReader(improve.archiveInStream))
                {
             
                    improve.SetExpression(input.ReadLine());
                }
                improve.archiveInStream.Close();
                
            }
            else
            {
                File.Decrypt(improve.InFilePath);
               using (StreamReader input = new StreamReader(improve.InFilePath))
               {
                    improve.SetExpression(input.ReadLine());
               }
               
            }
            return improve;
        }
    }
    class XmlInDecorator : FileDecorator
    {
        public XmlInDecorator(IFileImprovement file) : base(file) { }
         public override FileItem FileImprovement()
         {
            var improve = base.FileImprovement();

            if (improve.archiveInStream != null)
            {

                using (XmlReader xml = XmlReader.Create(improve.archiveInStream))
                {
                    if (xml.MoveToContent() == XmlNodeType.Element)
                    {
                        improve.SetExpression(xml.ReadElementContentAsString());
                    }
                    (improve.archiveInStream).Close();
                }
            }
            else
            {
                File.Decrypt(improve.InFilePath);
                XmlTextReader xmlRead = new XmlTextReader(improve.InFilePath); // XML
                xmlRead.WhitespaceHandling = WhitespaceHandling.None;
                while (xmlRead.Read())
                {
                    if (xmlRead.NodeType == XmlNodeType.Text)
                    {
                        improve.SetExpression(xmlRead.Value);
                    }
                }
            }
            return improve;
         }
    }

    public class JsonFile
    {
        [JsonProperty("expression")]
        public string? expression { get; set; }
    }
    class JsonInDecorator : FileDecorator
    {
        public JsonInDecorator(IFileImprovement file) : base(file) { }
        public override FileItem FileImprovement()
        {
           
            var improve = base.FileImprovement();

            if (improve.archiveInStream != null)
            {
                JsonSerializer serializer = new JsonSerializer();
                string? expression = "";
             
                   
                    using (var inp = JsonDocument.Parse(improve.archiveInStream))
                    {
                        JsonElement exp = inp.RootElement.GetProperty("expression");
                        expression = exp.GetString();
                    }
                    improve.SetExpression(expression);
                   improve.archiveInStream.Close();
            }
            else
            {
                File.Decrypt(improve.InFilePath);
                var obj = JsonConvert.DeserializeObject<JsonFile>(System.IO.File.ReadAllText(improve.InFilePath)); 
                improve.SetExpression(obj?.expression);
            }
            return improve;
        }
    }


    