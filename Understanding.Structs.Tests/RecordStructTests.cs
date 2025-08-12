using FluentAssertions;

namespace Understanding.Structs.Tests;

public class RecordStructTests
{
    private record struct Test(int Value);

    [Fact]
    public void PassingStructToFunction_ReturnsCopyOfStruct_WithModifiedValue()
    {
        static Test AddOneToTestValueAndReturnCopy(Test value)
        {
            value.Value += 1;
            return value;
        }
        
        // Arrange
        var t01 = new Test(69);
        
        // Act
        var t02 = AddOneToTestValueAndReturnCopy(t01);

        // Assert
        t01.Value.Should().Be(69);
        t02.Value.Should().Be(70);
        t01.Should().NotBeSameAs(t02); // not the same object in memory
        t01.Should().NotBe(t02);       // not the same value in object
    }

    [Fact]
    public void PassingStructToFunctionByRef_AndModifyingItsValue_ModifiesTheValueOfTheStruct()
    {
        static void AddOneToTestValueByRef(ref Test value)
        {
            value.Value += 1;
        }
        
        // Arrange
        var t01 = new Test(69);
        
        // Act
        AddOneToTestValueByRef(ref t01);

        // Assert
        t01.Value.Should().Be(70); // value of object has changed
    }
    
    [Fact]
    public void PassingStructToFunctionByRef_AndModifyingItsValue_ThenReturningStruct_ReturnsCopyOfTheStructWithModifiedValue()
    {
        static Test AddOneToTestValueByRefAndReturnRef(ref Test value)
        {
            value.Value += 1;
            return value;
        }

        // Arrange
        var t01 = new Test(69);
        
        // Act
        var t02 = AddOneToTestValueByRefAndReturnRef(ref t01);

        // Assert
        t01.Value.Should().Be(70);
        t02.Value.Should().Be(70);
        t01.Should().NotBeSameAs(t02); // not the same object in memory
        t01.Should().Be(t02);          // the same value in object
    }
        
    [Fact]
    public void PassingStructToFunctionByRef_AndModifyingItsValue_ThenReturningStructByRef_ReturnsCopyOfTheStructWithModifiedValue()
    {
        static ref Test AddOneToTestValueByRefAndReturnRef(ref Test value)
        {
            value.Value += 1;
            return ref value;
        }

        // Arrange
        var t01 = new Test(69);
        
        // Act
        var t02 = AddOneToTestValueByRefAndReturnRef(ref t01);

        // Assert
        t01.Value.Should().Be(70);
        t02.Value.Should().Be(70);
        t01.Should().NotBeSameAs(t02); // not the same object in memory
        t01.Should().Be(t02);          // the same value in object
    }
}
