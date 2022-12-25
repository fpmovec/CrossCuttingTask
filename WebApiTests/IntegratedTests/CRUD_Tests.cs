using Cross_Cutting_Task.FileItems;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApiTests.IntegratedTests;

public class WebServiceTests
{
    private WebApplicationFactory<Program> _waf;
    private readonly HttpClient _client;
    private int i = 22;
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

    [Fact]
    public async Task PostAsyncTest1()
    {
        string json = JsonConvert.SerializeObject(new FileItem
        {
            InFilePath = "path",
            InArchiveType = "zip",
            OutFileName = "name",
            OutArchiveType = "rar"
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
        i--;
        var responseCode = response.StatusCode;
        Assert.Equal(System.Net.HttpStatusCode.OK, responseCode);
    }
    
}