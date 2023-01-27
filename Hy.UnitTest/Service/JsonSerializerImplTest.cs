using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hy.JsonConverters;
using Hy.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Hy.UnitTest.Service;

public class JsonSerializerImplTest
{
    private IJsonSerializer _jsonSerializer;
    
    [SetUp]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.Configure<JsonSerializerOptions>(options =>
        {
            options.AllowTrailingCommas = true;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;    // 忽略null值的属性
            options.PropertyNameCaseInsensitive = true;    //忽略大小写
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;    // 驼峰式
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;    // 序列化中文时的编码问题
            options.Converters.Add(new DateTimeConverter());
            options.Converters.Add(new NullableDateTimeConverter());
        });
        
        serviceCollection.AddHyService();
        
        var provider = serviceCollection.BuildServiceProvider();

        _jsonSerializer = provider.GetRequiredService<IJsonSerializer>();
    }

    [Test]
    public void TestSerialize()
    {
        var queryObject = new object[]
        {
            new ViewModel(MyEnum.Blue, null, DateTime.Parse("2023-01-27 11:48:54"), new ViewModel2("haha")),
            new
            {
                ids = new[] { 1, 2, 3 }
            }
        };
        var json = _jsonSerializer.Serialize(queryObject);
        Assert.AreEqual(json, """
        [{"myEnum":2,"dateTime":"2023-01-27 11:48:54","viewModel2":{"name":"haha"}},{"ids":[1,2,3]}]
        """);
        // Console.WriteLine(json);
    }
    
    private enum MyEnum
    {
        Red = 1,
        Blue =2
    }

    private record ViewModel(MyEnum? MyEnum, MyEnum? Enum, DateTime? DateTime, ViewModel2 ViewModel2);
    private record ViewModel2(string name);
}