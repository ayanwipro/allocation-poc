# Allocation POC

A .NET 9 command-line application that splits a whole-unit allocation across stores by weight. It uses the largest-remainder method so the store totals always add up to the requested quantity.

## Run

```powershell
dotnet run --project src/Allocation.App -- 12 S1=2 S2=1
```

Expected output:

```text
Allocation for 12 units:
S1: 8
S2: 4
Total: 12
```

## Test

```powershell
dotnet test AllocationPoc.sln
```

The solution contains the allocation library, CLI, and xUnit test project. The `infra/main.bicep` file is retained from the original sample.