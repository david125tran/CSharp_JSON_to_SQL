CREATE TABLE WrestlingTeam.Wrestlers
(
    WrestlerId INT IDENTITY(1, 1) PRIMARY KEY,
    FirstName NVARCHAR(255),
    LastName NVARCHAR(255),
    WeightClass INT
)

