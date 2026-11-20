"""Split a store allocation across stores by weight (OTS)."""
from __future__ import annotations


def allocate(units: int, weights: dict[str, float]) -> dict[str, int]:
    """Return whole units per store, proportional to each store's weight."""
    if units < 0:
        raise ValueError("units must be non-negative")
    total = sum(weights.values())
    if total <= 0:
        raise ValueError("weights must sum to a positive number")
    return {store: int(units * w / total) for store, w in weights.items()}
