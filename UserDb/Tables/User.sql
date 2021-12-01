CREATE TABLE [dbo].[User]
(
	[Id]			INT PRIMARY KEY IDENTITY			NOT NULL,
	[Date]			Date								NOT NULL,
	[Name]			NVARCHAR				(max)		NOT NULL,		
	[LastName]		NVARCHAR				(max)		NOT NULL,		
	[Patronymic]	NVARCHAR				(max)		NULL,		
	[City]			NVARCHAR				(max)		NOT NULL,			
	[Country]		NVARCHAR				(max)		NOT NULL,			
)
