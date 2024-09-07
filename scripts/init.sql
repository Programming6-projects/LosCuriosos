CREATE TABLE IF NOT EXISTS test_table (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

INSERT INTO test_table (name) VALUES ('some_value');
