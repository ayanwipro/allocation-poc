namespace Allocation.Core;

public static class AllocationSplitter
{
    public static IReadOnlyDictionary<string, int> Allocate(
        int units,
        IReadOnlyDictionary<string, decimal> weights)
    {
        ArgumentNullException.ThrowIfNull(weights);

        if (units < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(units), "Units must be non-negative.");
        }

        if (weights.Count == 0)
        {
            throw new ArgumentException("At least one store weight is required.", nameof(weights));
        }

        if (weights.Any(pair => string.IsNullOrWhiteSpace(pair.Key)))
        {
            throw new ArgumentException("Store names must not be empty.", nameof(weights));
        }

        if (weights.Any(pair => pair.Value < 0))
        {
            throw new ArgumentOutOfRangeException(nameof(weights), "Weights must be non-negative.");
        }

        var totalWeight = weights.Values.Sum();
        if (totalWeight <= 0)
        {
            throw new ArgumentException("Weights must sum to a positive number.", nameof(weights));
        }

        var allocations = new Dictionary<string, int>(StringComparer.Ordinal);
        var remainders = new List<(string Store, decimal Fraction)>();
        long allocatedUnits = 0;

        foreach (var (store, weight) in weights.OrderBy(pair => pair.Key, StringComparer.Ordinal))
        {
            var exactShare = units * weight / totalWeight;
            var wholeUnits = (int)decimal.Floor(exactShare);

            allocations[store] = wholeUnits;
            allocatedUnits += wholeUnits;
            remainders.Add((store, exactShare - wholeUnits));
        }

        var remainingUnits = units - (int)allocatedUnits;
        foreach (var remainder in remainders
                     .OrderByDescending(item => item.Fraction)
                     .ThenBy(item => item.Store, StringComparer.Ordinal)
                     .Take(remainingUnits))
        {
            allocations[remainder.Store]++;
        }

        return allocations;
    }
}