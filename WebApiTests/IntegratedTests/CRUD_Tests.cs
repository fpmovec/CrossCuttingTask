using Cross_Cutting_Task.FileItems;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApiTests.IntegratedTests;

public class WebServiceTests
{
    private WebApplicationFactory<Program> _waf;
    private readonly HttpClient _client;

    public WebServiceTests()
    {
        _waf = new WebApplicationFactory<Program>();
        _client = _waf.CreateClient();
    }

    [Fact]
    public async Task GetAsyncTest()
    {
        var response = await _client.GetAsync("api/FileItem/fileslist");
        response.EnsureSuccessStatusCode();

        var responseCode = response.StatusCode;
        Assert.Equal(System.Net.HttpStatusCode.OK, responseCode);
    }
    
    // TODO #1: Change the PostAsyncTest test method 
    [Fact]
    public async Task PostAsyncTest1()
    {
        string json = JsonConvert.SerializeObject(new IntermediateClass()
        {
            InFilePath = "D:\\input.xml",
            InArchiveType = "None",
            InFileType = "xml",
            OutFileName = "name",
            OutFileType = "json",
            OutArchiveType = "zip"
        });

        StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("api/FileItem/addfile", httpContent);
        var responseCode = response.StatusCode;
        Assert.Equal(System.Net.HttpStatusCode.Created, responseCode);
    }

    [Fact]
    public async Task DeleteAsyncTest()
    {
        var response = await _client.DeleteAsync($"api/FileItem/delete/15");
       
        var responseCode = response.StatusCode;
        Assert.Equal(System.Net.HttpStatusCode.OK, responseCode);
    }
    
}