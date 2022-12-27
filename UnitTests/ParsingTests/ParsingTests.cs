using Cross_Cutting_Task.FileItems;

namespace UnitTests;

public class ParsingTests
{
    private FileItem _item;
    public ParsingTests()
    {
        _item = new FileItem();
    }
    [Fact]
    public void ParsingTestOne()
    {
        _item.SetExpression("5 + 4");
        var actual = _item.ExpressionParsing();
        double expected = 9.0;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParsingTestTwo()
    {
        _item.SetExpression("5 * 4");
        var actual = _item.ExpressionParsing();
        double expected = 20;
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParsingTestThree()
    {
        _item.SetExpression("(((6+7)*2)^2)");
        var actual = _item.ExpressionParsing();
        double expected = 676;
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ParsingTestFour()
    {
        _item.SetExpression("5!");
        var actual = _item.ExpressionParsing();
        double expected = 120;
        
        Assert.Equal(expected, actual);
    }

}