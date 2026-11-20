"""Store reference data (REF DATA)."""
from __future__ import annotations

STORES = {"S1": "Boston", "S2": "Leeds", "S3": "Warsaw"}


def store_name(code: str) -> str:
    """Look up a store's display name."""
    return STORES.get(code, "unknown store")
