CREATE TABLE [dbo].[Members]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	Name NVarChar(255) Not Null,
	Address NVarChar(Max) Null,
	Created DateTime Default(GetDate()) Null,
)
