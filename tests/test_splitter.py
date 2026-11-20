import pytest

from allocation.splitter import allocate


def test_proportional_split():
    assert allocate(12, {"A": 2, "B": 1}) == {"A": 8, "B": 4}


def test_rejects_negative_units():
    with pytest.raises(ValueError):
        allocate(-1, {"A": 1})


def test_rejects_zero_weights():
    with pytest.raises(ValueError):
        allocate(5, {"A": 0})
