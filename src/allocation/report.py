"""Allocation summary used by the OTS export."""
from __future__ import annotations

from allocation.splitter import allocate


def summarise(po: str, units: int, weights: dict[str, float]) -> str:
    shares = allocate(units, weights)
    lines = [f"PO {po}: {units} units"]
    lines += [f"  {store}: {n}" for store, n in sorted(shares.items())]
    return "\n".join(lines)
