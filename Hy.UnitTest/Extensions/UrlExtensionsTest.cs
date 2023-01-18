using Hy.Extensions;

namespace Hy.UnitTest.Extensions;

public class UrlExtensionsTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCreateGetMethodUrl()
    {
        var url = "Admin".CreateGetMethodUrl(new object []
        {
            new ViewModel(MyEnum.Blue, null),
            new
            {
                ids = new [] {1, 2, 3}
            }
        });
        
        Assert.AreEqual(url, "Admin?myenum=2&ids=1&ids=2&ids=3");
    }
    
    private enum MyEnum
    {
        Red = 1,
        Blue =2
    }

    private record ViewModel(MyEnum? MyEnum, MyEnum? Enum);
}