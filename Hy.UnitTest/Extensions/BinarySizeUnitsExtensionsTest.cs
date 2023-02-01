using Hy.Extensions;
using Hy.Types;

namespace Hy.UnitTest.Extensions;

public class BinarySizeUnitsExtensionsTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestText()
    {
        var size = BinarySizeUnits.MB.Text(1024 * 1024 * 1024);
        Console.WriteLine(size);
    }
}