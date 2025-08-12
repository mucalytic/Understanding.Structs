using Xunit.Abstractions;
using FluentAssertions;
using System.Text.Json;

namespace Understanding.Structs.Tests;

public class ReadOnlyRecordStructTests(ITestOutputHelper output)
{
    private readonly record struct Test(int Value);

    [Fact]
    public void TwoReadOnlyStructs_WithTheSameValue_AreEquivalentToTheSameStruct()
    {
        // Arrange
        var t01 = new Test(69);
        var t02 = new Test(69);
        
        // Assert
        t01.Value.Should().Be(69);
        t02.Value.Should().Be(69);
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

    [Fact]
    public void SerialisedAndDeserialisedStructs_AreNotTheSameObjectInMemory()
    {
        // Arrange
        var t01 = new Test(69);
        
        // Act
        var json = JsonSerializer.Serialize(t01);
        var t02 = JsonSerializer.Deserialize<Test>(json);
        
        // Assert
        t01.Should().Be(t02);
        t01.Should().NotBeSameAs(t02);
    }

    [Fact]
    public void SerialisedAndDeserialisedStructs_PassedByRef_AreNotTheSameObjectInMemory()
    {
        Test SerialiseAndDeserialisedStructPassedByRef(ref Test value)
        {
            var json = JsonSerializer.Serialize(value);
            return JsonSerializer.Deserialize<Test>(json);
        }
        
        // Arrange
        var t01 = new Test(69);
        
        // Act
        var t02 = SerialiseAndDeserialisedStructPassedByRef(ref t01);
        
        // Assert
        t01.Should().Be(t02);
        t01.Should().NotBeSameAs(t02);
    }
}
