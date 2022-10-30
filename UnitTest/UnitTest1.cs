using CalculatorApp;

namespace UnitTest;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethodEquation1()
    {
        string equation = "1 + 1";
        var expectedOutput = 2;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    
    [TestMethod]
    public void TestMethodEquation2()
    {
        string equation = "2 * 2";
        var expectedOutput = 4;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation3()
    {
        string equation = "1 + 2 + 3";
        var expectedOutput = 6;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation4()
    {
        string equation = "6 / 2";
        var expectedOutput = 3;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation5()
    {
        string equation = "11 + 23";
        var expectedOutput = 34;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation6()
    {
        string equation = "11.1 + 23";
        var expectedOutput = 34.1;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation7()
    {
        string equation = "1 + 1 * 3";
        var expectedOutput = 4;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation8()
    {
        string equation = "( 11.5 + 15.4 ) + 10.1";
        var expectedOutput = 37;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation9()
    {
        string equation = "23 - ( 29.3 - 12.5 )";
        var expectedOutput = 6.2;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation10()
    {
        string equation = "( 1 / 2 ) - 1 + 1";
        var expectedOutput = 0.5;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
    [TestMethod]
    public void TestMethodEquation11()
    {
        string equation = "10 - ( 2 + 3 * ( 7 - 5 ) )";
        var expectedOutput = 2;
        var actualOutput = Calculator.Calculations(equation);
        Assert.AreEqual(expectedOutput, actualOutput);
    }
}