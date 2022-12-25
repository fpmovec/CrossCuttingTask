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
        var file = File.OpenRead(improve.InFilePath ?? string.Empty);
        ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read);
        
        foreach (var item in zip.Entries)
        {
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
        FileStream file = File.OpenRead(improve.InFilePath);
        RarArchive rar = RarArchive.Open(file);
        foreach (RarArchiveEntry entry in rar.Entries)
        {
            improve.archiveInStream = entry.OpenEntryStream();
            return improve;
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
                string? exp = "";
                    var inp = JsonDocument.Parse(improve.archiveInStream);
               
                   using (StreamReader sr = new StreamReader(improve.archiveInStream))
                   {
                       using (JsonReader reader = new JsonTextReader(sr))
                       {
                           while (reader.Read())
                           {
                               if (reader.TokenType == JsonToken.StartObject)
                               {
                                   exp = serializer.Deserialize<string>(reader);
                               }
                           }
                       }
                   }
                   improve.SetExpression(exp);
                   improve.archiveInStream.Close();
            }
            else
            {
                var obj = JsonConvert.DeserializeObject<JsonFile>(System.IO.File.ReadAllText(improve.InFilePath)); // JSON
                improve.SetExpression(obj?.expression);
            }
            return improve;
        }
    }

public class EncryptDecorator : FileDecorator
{
    public EncryptDecorator(IFileImprovement file) : base(file) { }

    public override FileItem FileImprovement()
    {
        var improve = base.FileImprovement();
        File.Encrypt(improve.InFilePath);
        return improve;
        
    }
    
}
    