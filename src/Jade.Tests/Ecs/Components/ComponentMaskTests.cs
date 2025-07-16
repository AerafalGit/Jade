// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

using Jade.Ecs.Components;

namespace Jade.Tests.Ecs.Components;

public sealed class ComponentMaskTests
{
    [Fact]
    public void DefaultConstructor_ShouldCreateEmptyMask()
    {
        // Arrange & Act
        var mask = new ComponentMask();

        // Assert
        Assert.True(mask.IsEmpty);
        Assert.Equal(0, mask.PopCount());
        Assert.Equal([], mask.GetComponents());
    }

    [Fact]
    public void IsEmpty_WithNoComponents_ShouldReturnTrue()
    {
        // Arrange
        var mask = new ComponentMask();

        // Act & Assert
        Assert.True(mask.IsEmpty);
    }

    [Fact]
    public void IsEmpty_WithComponents_ShouldReturnFalse()
    {
        // Arrange
        var mask = new ComponentMask().With(new ComponentId(0));

        // Act & Assert
        Assert.False(mask.IsEmpty);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(128)]
    [InlineData(255)]
    public void With_ValidComponentId_ShouldSetBit(int componentId)
    {
        // Arrange
        var mask = new ComponentMask();
        var id = new ComponentId(componentId);

        // Act
        var result = mask.With(id);

        // Assert
        Assert.True(result.Has(id));
        Assert.False(result.IsEmpty);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(256)]
    [InlineData(300)]
    public void With_InvalidComponentId_ShouldThrowArgumentOutOfRangeException(int componentId)
    {
        // Arrange
        var mask = new ComponentMask();
        var id = new ComponentId(componentId);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => mask.With(id));
    }

    [Fact]
    public void With_SameComponentIdTwice_ShouldHaveIdempotentBehavior()
    {
        // Arrange
        var mask = new ComponentMask();
        var id = new ComponentId(42);

        // Act
        var result1 = mask.With(id);
        var result2 = result1.With(id);

        // Assert
        Assert.Equal(result1, result2);
        Assert.True(result2.Has(id));
        Assert.Equal(1, result2.PopCount());
    }

    [Fact]
    public void Without_ExistingComponent_ShouldRemoveBit()
    {
        // Arrange
        var id = new ComponentId(42);
        var mask = new ComponentMask().With(id);

        // Act
        var result = mask.Without(id);

        // Assert
        Assert.False(result.Has(id));
        Assert.True(result.IsEmpty);
    }

    [Fact]
    public void Without_NonExistingComponent_ShouldNotChange()
    {
        // Arrange
        var id1 = new ComponentId(42);
        var id2 = new ComponentId(43);
        var mask = new ComponentMask().With(id1);

        // Act
        var result = mask.Without(id2);

        // Assert
        Assert.Equal(mask, result);
        Assert.True(result.Has(id1));
        Assert.False(result.Has(id2));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(256)]
    [InlineData(300)]
    public void Without_InvalidComponentId_ShouldThrowArgumentOutOfRangeException(int componentId)
    {
        // Arrange
        var mask = new ComponentMask();
        var id = new ComponentId(componentId);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => mask.Without(id));
    }

    [Fact]
    public void Has_ExistingComponent_ShouldReturnTrue()
    {
        // Arrange
        var id = new ComponentId(42);
        var mask = new ComponentMask().With(id);

        // Act & Assert
        Assert.True(mask.Has(id));
    }

    [Fact]
    public void Has_NonExistingComponent_ShouldReturnFalse()
    {
        // Arrange
        var id1 = new ComponentId(42);
        var id2 = new ComponentId(43);
        var mask = new ComponentMask().With(id1);

        // Act & Assert
        Assert.False(mask.Has(id2));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(256)]
    [InlineData(300)]
    public void Has_InvalidComponentId_ShouldThrowArgumentOutOfRangeException(int componentId)
    {
        // Arrange
        var mask = new ComponentMask();
        var id = new ComponentId(componentId);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => mask.Has(id));
    }

    [Fact]
    public void HasAll_EmptyMask_ShouldReturnTrue()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask();

        // Act & Assert
        Assert.True(mask1.HasAll(mask2));
    }

    [Fact]
    public void HasAll_SubsetMask_ShouldReturnTrue()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2)).With(new ComponentId(3));
        var mask2 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(3));

        // Act & Assert
        Assert.True(mask1.HasAll(mask2));
    }

    [Fact]
    public void HasAll_NotSubsetMask_ShouldReturnFalse()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(3));

        // Act & Assert
        Assert.False(mask1.HasAll(mask2));
    }

    [Fact]
    public void HasAny_EmptyMask_ShouldReturnFalse()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask();

        // Act & Assert
        Assert.False(mask1.HasAny(mask2));
    }

    [Fact]
    public void HasAny_IntersectingMask_ShouldReturnTrue()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask().With(new ComponentId(2)).With(new ComponentId(3));

        // Act & Assert
        Assert.True(mask1.HasAny(mask2));
    }

    [Fact]
    public void HasAny_NonIntersectingMask_ShouldReturnFalse()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask().With(new ComponentId(3)).With(new ComponentId(4));

        // Act & Assert
        Assert.False(mask1.HasAny(mask2));
    }

    [Fact]
    public void GetComponents_EmptyMask_ShouldReturnEmptyArray()
    {
        // Arrange
        var mask = new ComponentMask();

        // Act
        var components = mask.GetComponents();

        // Assert
        Assert.Empty(components);
    }

    [Fact]
    public void GetComponents_SingleComponent_ShouldReturnCorrectId()
    {
        // Arrange
        var id = new ComponentId(42);
        var mask = new ComponentMask().With(id);

        // Act
        var components = mask.GetComponents();

        // Assert
        Assert.Single(components);
        Assert.Equal(id, components[0]);
    }

    [Fact]
    public void GetComponents_MultipleComponents_ShouldReturnSortedIds()
    {
        // Arrange
        var ids = new[] { new ComponentId(5), new ComponentId(1), new ComponentId(3) };
        var mask = new ComponentMask();
        foreach (var id in ids)
        {
            mask = mask.With(id);
        }

        // Act
        var components = mask.GetComponents();

        // Assert
        Assert.Equal(3, components.Length);
        Assert.Equal(new ComponentId(1), components[0]);
        Assert.Equal(new ComponentId(3), components[1]);
        Assert.Equal(new ComponentId(5), components[2]);
    }

    [Fact]
    public void GetComponents_SpanningMultipleWords_ShouldReturnAllComponents()
    {
        // Arrange
        var ids = new[] { new ComponentId(0), new ComponentId(63), new ComponentId(64), new ComponentId(127), new ComponentId(255) };
        var mask = new ComponentMask();
        foreach (var id in ids)
        {
            mask = mask.With(id);
        }

        // Act
        var components = mask.GetComponents();

        // Assert
        Assert.Equal(5, components.Length);
        Assert.Equal(ids.OrderBy(x => x.Id).ToArray(), components);
    }

    [Fact]
    public void PopCount_EmptyMask_ShouldReturnZero()
    {
        // Arrange
        var mask = new ComponentMask();

        // Act
        var count = mask.PopCount();

        // Assert
        Assert.Equal(0, count);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(50)]
    public void PopCount_MultipleComponents_ShouldReturnCorrectCount(int componentCount)
    {
        // Arrange
        var mask = new ComponentMask();
        for (var i = 0; i < componentCount; i++)
        {
            mask = mask.With(new ComponentId(i));
        }

        // Act
        var count = mask.PopCount();

        // Assert
        Assert.Equal(componentCount, count);
    }

    [Fact]
    public void FirstSetBit_EmptyMask_ShouldReturnMinusOne()
    {
        // Arrange
        var mask = new ComponentMask();

        // Act
        var firstBit = mask.FirstSetBit();

        // Assert
        Assert.Equal(-1, firstBit.Id);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(63)]
    [InlineData(64)]
    [InlineData(127)]
    [InlineData(255)]
    public void FirstSetBit_SingleComponent_ShouldReturnCorrectId(int componentId)
    {
        // Arrange
        var id = new ComponentId(componentId);
        var mask = new ComponentMask().With(id);

        // Act
        var firstBit = mask.FirstSetBit();

        // Assert
        Assert.Equal(id, firstBit);
    }

    [Fact]
    public void FirstSetBit_MultipleComponents_ShouldReturnLowestId()
    {
        // Arrange
        var ids = new[] { new ComponentId(10), new ComponentId(5), new ComponentId(20) };
        var mask = new ComponentMask();
        foreach (var id in ids)
        {
            mask = mask.With(id);
        }

        // Act
        var firstBit = mask.FirstSetBit();

        // Assert
        Assert.Equal(new ComponentId(5), firstBit);
    }

    [Fact]
    public void BitwiseAnd_EmptyMasks_ShouldReturnEmpty()
    {
        // Arrange
        var mask1 = new ComponentMask();
        var mask2 = new ComponentMask();

        // Act
        var result = mask1 & mask2;

        // Assert
        Assert.True(result.IsEmpty);
    }

    [Fact]
    public void BitwiseAnd_IntersectingMasks_ShouldReturnIntersection()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2)).With(new ComponentId(3));
        var mask2 = new ComponentMask().With(new ComponentId(2)).With(new ComponentId(3)).With(new ComponentId(4));

        // Act
        var result = mask1 & mask2;

        // Assert
        Assert.True(result.Has(new ComponentId(2)));
        Assert.True(result.Has(new ComponentId(3)));
        Assert.False(result.Has(new ComponentId(1)));
        Assert.False(result.Has(new ComponentId(4)));
        Assert.Equal(2, result.PopCount());
    }

    [Fact]
    public void BitwiseOr_EmptyMasks_ShouldReturnEmpty()
    {
        // Arrange
        var mask1 = new ComponentMask();
        var mask2 = new ComponentMask();

        // Act
        var result = mask1 | mask2;

        // Assert
        Assert.True(result.IsEmpty);
    }

    [Fact]
    public void BitwiseOr_NonEmptyMasks_ShouldReturnUnion()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask().With(new ComponentId(2)).With(new ComponentId(3));

        // Act
        var result = mask1 | mask2;

        // Assert
        Assert.True(result.Has(new ComponentId(1)));
        Assert.True(result.Has(new ComponentId(2)));
        Assert.True(result.Has(new ComponentId(3)));
        Assert.Equal(3, result.PopCount());
    }

    [Fact]
    public void BitwiseXor_EmptyMasks_ShouldReturnEmpty()
    {
        // Arrange
        var mask1 = new ComponentMask();
        var mask2 = new ComponentMask();

        // Act
        var result = mask1 ^ mask2;

        // Assert
        Assert.True(result.IsEmpty);
    }

    [Fact]
    public void BitwiseXor_PartiallyOverlappingMasks_ShouldReturnSymmetricDifference()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2)).With(new ComponentId(3));
        var mask2 = new ComponentMask().With(new ComponentId(2)).With(new ComponentId(3)).With(new ComponentId(4));

        // Act
        var result = mask1 ^ mask2;

        // Assert
        Assert.True(result.Has(new ComponentId(1)));
        Assert.False(result.Has(new ComponentId(2)));
        Assert.False(result.Has(new ComponentId(3)));
        Assert.True(result.Has(new ComponentId(4)));
        Assert.Equal(2, result.PopCount());
    }

    [Fact]
    public void BitwiseNot_EmptyMask_ShouldReturnFullMask()
    {
        // Arrange
        var mask = new ComponentMask();

        // Act
        var result = ~mask;

        // Assert
        Assert.False(result.IsEmpty);
        Assert.Equal(ComponentMask.MaxComponents, result.PopCount());
    }

    [Fact]
    public void BitwiseNot_SingleComponent_ShouldFlipAllBits()
    {
        // Arrange
        var id = new ComponentId(42);
        var mask = new ComponentMask().With(id);

        // Act
        var result = ~mask;

        // Assert
        Assert.False(result.Has(id));
        Assert.Equal(ComponentMask.MaxComponents - 1, result.PopCount());
    }

    [Fact]
    public void BitwiseNot_DoubleNegation_ShouldReturnOriginal()
    {
        // Arrange
        var mask = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));

        // Act
        var result = ~~mask;

        // Assert
        Assert.Equal(mask, result);
    }

    [Fact]
    public void Equals_SameMask_ShouldReturnTrue()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));

        // Act & Assert
        Assert.True(mask1.Equals(mask2));
        Assert.True(mask1 == mask2);
        Assert.False(mask1 != mask2);
    }

    [Fact]
    public void Equals_DifferentMask_ShouldReturnFalse()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(3));

        // Act & Assert
        Assert.False(mask1.Equals(mask2));
        Assert.False(mask1 == mask2);
        Assert.True(mask1 != mask2);
    }

    [Fact]
    public void Equals_WithObject_ShouldWork()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1));
        var mask2 = new ComponentMask().With(new ComponentId(1));
        object obj = mask2;

        // Act & Assert
        Assert.True(mask1.Equals(obj));
    }

    [Fact]
    public void Equals_WithNull_ShouldReturnFalse()
    {
        // Arrange
        var mask = new ComponentMask().With(new ComponentId(1));

        // Act & Assert
        Assert.False(mask.Equals(null));
    }

    [Fact]
    public void Equals_WithDifferentType_ShouldReturnFalse()
    {
        // Arrange
        var mask = new ComponentMask().With(new ComponentId(1));

        // Act & Assert
        Assert.False(mask.Equals("string"));
    }

    [Fact]
    public void GetHashCode_SameMasks_ShouldReturnSameHashCode()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));
        var mask2 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(2));

        // Act
        var hash1 = mask1.GetHashCode();
        var hash2 = mask2.GetHashCode();

        // Assert
        Assert.Equal(hash1, hash2);
    }

    [Fact]
    public void GetHashCode_DifferentMasks_ShouldReturnDifferentHashCodes()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1));
        var mask2 = new ComponentMask().With(new ComponentId(2));

        // Act
        var hash1 = mask1.GetHashCode();
        var hash2 = mask2.GetHashCode();

        // Assert
        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void EdgeCase_MaxComponentId_ShouldWork()
    {
        // Arrange
        var maxId = new ComponentId(ComponentMask.MaxComponents - 1);
        var mask = new ComponentMask();

        // Act
        var result = mask.With(maxId);

        // Assert
        Assert.True(result.Has(maxId));
        Assert.Equal(1, result.PopCount());
        Assert.Equal(maxId, result.FirstSetBit());
    }

    [Fact]
    public void EdgeCase_AllComponentsSet_ShouldWork()
    {
        // Arrange
        var mask = new ComponentMask();
        for (var i = 0; i < ComponentMask.MaxComponents; i++)
        {
            mask = mask.With(new ComponentId(i));
        }

        // Act & Assert
        Assert.Equal(ComponentMask.MaxComponents, mask.PopCount());
        Assert.Equal(new ComponentId(0), mask.FirstSetBit());
        Assert.False(mask.IsEmpty);
    }

    [Fact]
    public void EdgeCase_WordBoundaries_ShouldWork()
    {
        // Arrange
        var boundaries = new[] { 0, 63, 64, 127, 128, 191, 192, 255 };
        var mask = new ComponentMask();

        // Act
        foreach (var boundary in boundaries)
        {
            mask = mask.With(new ComponentId(boundary));
        }

        // Assert
        Assert.Equal(boundaries.Length, mask.PopCount());
        foreach (var boundary in boundaries)
        {
            Assert.True(mask.Has(new ComponentId(boundary)));
        }
    }

    [Fact]
    public void Performance_LargeNumberOfOperations_ShouldComplete()
    {
        // Arrange
        var mask = new ComponentMask();
        const int operationCount = 1000;

        // Act
        for (var i = 0; i < operationCount; i++)
        {
            var id = new ComponentId(i % ComponentMask.MaxComponents);
            mask = mask.With(id);
        }

        // Assert
        Assert.Equal(ComponentMask.MaxComponents, mask.PopCount());
    }

    [Fact]
    public void HardwareAcceleration_AVX2Support_ShouldProduceCorrectResults()
    {
        // Arrange
        var mask1 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(65)).With(new ComponentId(129));
        var mask2 = new ComponentMask().With(new ComponentId(1)).With(new ComponentId(130)).With(new ComponentId(255));

        // Act
        var andResult = mask1 & mask2;
        var orResult = mask1 | mask2;
        var xorResult = mask1 ^ mask2;

        // Assert
        // These operations should work correctly regardless of AVX2 support
        Assert.True(andResult.Has(new ComponentId(1)));
        Assert.Equal(1, andResult.PopCount());

        Assert.Equal(5, orResult.PopCount());
        Assert.True(orResult.Has(new ComponentId(1)));
        Assert.True(orResult.Has(new ComponentId(65)));
        Assert.True(orResult.Has(new ComponentId(129)));
        Assert.True(orResult.Has(new ComponentId(130)));
        Assert.True(orResult.Has(new ComponentId(255)));

        Assert.Equal(4, xorResult.PopCount());
        Assert.False(xorResult.Has(new ComponentId(1)));
    }

    [Fact]
    public void HardwareAcceleration_PopCountWithPopcnt_ShouldMatchManualCount()
    {
        // Arrange
        var mask = new ComponentMask()
            .With(new ComponentId(0))
            .With(new ComponentId(63))
            .With(new ComponentId(64))
            .With(new ComponentId(127))
            .With(new ComponentId(128))
            .With(new ComponentId(191))
            .With(new ComponentId(192))
            .With(new ComponentId(255));

        // Act
        var popCount = mask.PopCount();
        var manualCount = mask.GetComponents().Length;

        // Assert
        Assert.Equal(8, popCount);
        Assert.Equal(manualCount, popCount);
    }
}
