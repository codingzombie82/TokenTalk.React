CREATE TABLE [dbo].[Talks]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	Title NVarchar(255) Not Null,
	Description NVarchar(Max) Null,

	Created DateTime Default(GetDate()) Null,
)
Go