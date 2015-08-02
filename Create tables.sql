-- Get information about table (columns name, type etc.)
PRAGMA table_info(cloud)

-- Add specified column to specified table
ALTER TABLE cloud ADD COLUMN IconPath TEXT
ALTER TABLE wind ADD COLUMN IconPath TEXT
ALTER TABLE windForce ADD COLUMN IconPath TEXT


CREATE TABLE wether
(
	ID,
	Date,
	Time,
	Temperature,
	Pressure,
	Cloud_ID,
	Wind_ID,
)

CREATE TABLE cloud
(
	ID,
	Name
)

CREATE TABLE wind
(
	ID,
	Name
)