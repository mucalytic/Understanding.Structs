using Xunit.Abstractions;
using FluentAssertions;

namespace Understanding.Structs.Tests;

public class ReadOnlyStructTests(ITestOutputHelper output)
{
    private readonly struct Test(int value)
    {
        public int Value { get; } = value;
    }

    [Fact]
    public void TwoReadOnlyStructs_WithTheSameValue_AreEquivalentToTheSameStruct()
    {
        // Arrange
        var t01 = new Test(69);
        var t02 = new Test(69);
        
        // Assert
        t01.Should().Be(t02);
        t01.Should().NotBeSameAs(t02);
    }
    
    [Fact]
    public void PassingStructToInParameterFunction_CanOnlyReadValues()
    {
        void WriteToTestOutput(in Test value)
        {
            output.WriteLine(value.Value.ToString());
        }
        
        // Arrange
        var t01 = new Test(69);
        
        // Act
        WriteToTestOutput(t01);
        
        // Assert
        t01.Value.Should().Be(69);
    }
}
