using ConsoleApp.Services;
using Newtonsoft.Json;

namespace ConsoleApp.Test__xUnit;

public class FileManager_tests
{
    private readonly FileService fileService;
    private readonly MenuService menu;
    string content;

    public FileManager_tests()
    {
        fileService = new FileService();
        menu = new MenuService();
        menu.FilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\content.json";
        content = JsonConvert.SerializeObject(new { FirstName = "Maria", LastName = "Larsson", Email = "maria@larsson.se", PhoneNumber = "07934512645", Street = "Levendelstigen 6", ZipCode = "789 65", City = "Malmö" });
    }

    [Fact]
    public void Should_Create_a_File_With_Json_Content()
    {
        fileService.Save(menu.FilePath, content);
        string result = fileService.Read(menu.FilePath);

        Assert.True(File.Exists(menu.FilePath));
        Assert.Equal(result.Trim(), content);
    }
}