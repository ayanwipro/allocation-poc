using System.Globalization;
using Allocation.Core;

if (args.Length < 2 || !int.TryParse(args[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var units))
{
    return ShowUsage();
}

var weights = new Dictionary<string, decimal>(StringComparer.Ordinal);
foreach (var argument in args.Skip(1))
{
    var separator = argument.IndexOf('=');
    if (separator <= 0 || separator == argument.Length - 1 ||
        !decimal.TryParse(argument[(separator + 1)..], NumberStyles.Number, CultureInfo.InvariantCulture, out var weight) ||
        !weights.TryAdd(argument[..separator], weight))
    {
        Console.Error.WriteLine($"Invalid store weight: {argument}");
        return 2;
    }
}

try
{
    var allocations = AllocationSplitter.Allocate(units, weights);
    Console.WriteLine($"Allocation for {units} units:");
    foreach (var (store, allocatedUnits) in allocations)
    {
        Console.WriteLine($"{store}: {allocatedUnits}");
    }

    Console.WriteLine($"Total: {allocations.Values.Sum()}");
    return 0;
}
catch (ArgumentException exception)
{
    Console.Error.WriteLine(exception.Message);
    return 2;
}

static int ShowUsage()
{
    Console.Error.WriteLine("Usage: Allocation.App <units> <store=weight> [<store=weight> ...]");
    Console.Error.WriteLine("Example: Allocation.App 12 S1=2 S2=1");
    return 2;
}