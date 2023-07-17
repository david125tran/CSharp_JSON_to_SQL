CREATE DATABASE WrestlingTeamDatabase
GO

USE WrestlingTeamDatabase

DROP TABLE IF EXISTS WrestlingTeam.Wrestlers
GO

DROP SCHEMA IF EXISTS WrestlingTeam
GO

CREATE SCHEMA WrestlingTeam
GO



-- To delete the database run these code in order:

-- ALTER DATABASE WrestlingTeamDatabase SET SINGLE_USER WITH ROLLBACK IMMEDIATE
-- GO

-- DROP DATABASE WrestlingTeamDatabase
