using System;
namespace CSVreader;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestCharToInt() {
        Assert.That(CSVReaderClass.CharToInt('2').Equals(2));
        Assert.That(CSVReaderClass.CharToInt('m').Equals(Int32.MinValue));
    }
    [Test]
    public void TestStringToInt() {
        Assert.That(CSVReaderClass.StringToInt("5").Equals(5),"Method returned: {0}"
            ,CSVReaderClass.StringToInt("5"));
        Assert.That(CSVReaderClass.StringToInt("47").Equals(47),"Method returned: {0}"
        ,CSVReaderClass.StringToInt("47"));
        Assert.That(CSVReaderClass.StringToInt("-13").Equals(-13),"Method returned: {0}"
            ,CSVReaderClass.StringToInt("-13"));
        Assert.That(CSVReaderClass.StringToInt("-7234").Equals(-7234),"Method returned: {0}"
            ,CSVReaderClass.StringToInt("-7234"));
        Assert.That(CSVReaderClass.StringToInt("-72 34/#").Equals(-7234),"Method returned: {0}"
            ,CSVReaderClass.StringToInt("-7234"));
    }
    [Test]
    public void TestStringToIntArray() {
        Assert.That(CSVReaderClass.StringToIntArray("1,2,3")[1].Equals(2),"Element [1] of TestStringToIntArray({0}) returned: {1}"
            ,"1,2,3",CSVReaderClass.StringToIntArray("1,2,3")[1]);
        Assert.That(CSVReaderClass.StringToIntArray("10,- 2,3-2,4")[1].Equals(-2),"Element [1] of TestStringToIntArray({0}) returned: {1}"
            ,"1,2,3",CSVReaderClass.StringToIntArray("1,2,3")[1]);
    }
}