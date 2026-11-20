from refdata.stores import store_name


def test_known_store():
    assert store_name("S2") == "Leeds"
