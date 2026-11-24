using Allocation.Core;
using Xunit;

namespace Allocation.Tests;

public class AllocationSplitterTests
{
    [Fact]
    public void Allocate_SplitsByWeight()
    {
        var result = AllocationSplitter.Allocate(12, new Dictionary<string, decimal>
        {
            ["S1"] = 2,
            ["S2"] = 1,
        });

        Assert.Equal(8, result["S1"]);
        Assert.Equal(4, result["S2"]);
        Assert.Equal(12, result.Values.Sum());
    }

    [Fact]
    public void Allocate_DistributesRemainderByLargestFraction()
    {
        var result = AllocationSplitter.Allocate(10, new Dictionary<string, decimal>
        {
            ["A"] = 1,
            ["B"] = 1,
            ["C"] = 1,
        });

        Assert.Equal(4, result["A"]);
        Assert.Equal(3, result["B"]);
        Assert.Equal(3, result["C"]);
        Assert.Equal(10, result.Values.Sum());
    }

    [Fact]
    public void Allocate_UsesStoreNameToBreakTies()
    {
        var result = AllocationSplitter.Allocate(1, new Dictionary<string, decimal>
        {
            ["B"] = 1,
            ["A"] = 1,
        });

        Assert.Equal(1, result["A"]);
        Assert.Equal(0, result["B"]);
    }

    [Fact]
    public void Allocate_RejectsNegativeUnits()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            AllocationSplitter.Allocate(-1, new Dictionary<string, decimal> { ["S1"] = 1 }));
    }

    [Fact]
    public void Allocate_RejectsEmptyOrNonPositiveWeights()
    {
        Assert.Throws<ArgumentException>(() =>
            AllocationSplitter.Allocate(5, new Dictionary<string, decimal>()));
        Assert.Throws<ArgumentException>(() =>
            AllocationSplitter.Allocate(5, new Dictionary<string, decimal> { ["S1"] = 0 }));
    }

    [Fact]
    public void Allocate_RejectsNegativeWeights()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            AllocationSplitter.Allocate(5, new Dictionary<string, decimal> { ["S1"] = -1, ["S2"] = 2 }));
    }
}