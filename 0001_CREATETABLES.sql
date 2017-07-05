CREATE SCHEMA [OfficeLocation]
GO

CREATE TABLE [OfficeLocation].[Offices](
	[Name] [nvarchar](255) NOT NULL PRIMARY KEY,
	[Address] [nvarchar](255),
	[Country] [nvarchar](255) NOT NULL, 
	[Switchboard] [nvarchar](255),
	[Fax] [nvarchar](255),
	[TimeZone] [nvarchar](255),
	[Operating] [boolean]
)
GO



INSERT [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES (N'Amsterdam', '***REMOVED*** ***REMOVED***',
	'Netherlands','***REMOVED***', '***REMOVED***', 'CEST', True);
GO