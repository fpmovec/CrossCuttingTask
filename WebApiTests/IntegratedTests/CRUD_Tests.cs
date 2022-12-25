using Cross_Cutting_Task.FileItems;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApiTests.IntegratedTests;

public class WebServiceTests
{
    private WebApplicationFactory<Program> waf;
    private HttpClient client;
    public WebServiceTests()
    {
        waf = new WebApplicationFactory<Program>();
        client = waf.CreateClient();
    }

    [Fact]
    public async Task GetAsyncTest()
    {
        var response = await client.GetAsync("api/FileItem/fileslist");
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
           
        var responseCode = response.StatusCode;
        Assert.Equal(System.Net.HttpStatusCode.OK, responseCode);
    }

    [Fact]
    public async Task PostAsyncTest1()
    {
        FileItem item = new FileItem();
    
        string json = JsonConvert.SerializeObject(new FileItem
        {
            InFilePath = "path",
            InArchiveType = "zip",
            OutFileName = "name",
            OutArchiveType = "rar"
        });
        
        

        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await client.PostAsync("api/FileItem/addfile", httpContent);
        var responseCode = response.StatusCode;
        Assert.Equal(System.Net.HttpStatusCode.Created, responseCode);
    }
}