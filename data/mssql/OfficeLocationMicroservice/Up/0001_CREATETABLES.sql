CREATE SCHEMA [OfficeLocation]
GO

CREATE TABLE [OfficeLocation].[Office](
	[OfficeId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY ,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255),
	[Country] [nvarchar](255) NOT NULL, 
	[Switchboard] [nvarchar](255),
	[Fax] [nvarchar](255),
	[TimeZone] [nvarchar](255),
	[Operating] [bit]
)
GO

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Amsterdam', '***REMOVED*** ***REMOVED***',
	'Netherlands','***REMOVED***', '***REMOVED***', 'CEST', 1);
GO

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Austin','***REMOVED***
	***REMOVED*** ***REMOVED***','United States','***REMOVED***',
	'+***REMOVED***','CST', 1);


INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Berlin','***REMOVED*** Kurfürstendamm 194, 
	***REMOVED***','Germany','***REMOVED***','***REMOVED***','CET', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Charlotte','201 North Tryon Street, ste. 
	2300 ***REMOVED***','United States','***REMOVED***','***REMOVED***','EST', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('London','***REMOVED*** ***REMOVED***',
	'United Kingdom','***REMOVED***','***REMOVED***','BST', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Melbourne','***REMOVED*** ***REMOVED***',
	'Australia','***REMOVED***','','AEST', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Santa Monica','***REMOVED*** ***REMOVED***',
	'United States','***REMOVED***','***REMOVED***','PST', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Singapore','***REMOVED*** ***REMOVED***',
	'Singapore','***REMOVED***','***REMOVED***','SGT', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Sydney','***REMOVED*** ***REMOVED***',
	'Australia','***REMOVED***','***REMOVED***','AEST', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Tokyo','***REMOVED*** ***REMOVED*** ***REMOVED***',
	'Japan','***REMOVED***','***REMOVED***','JST', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Toronto','***REMOVED*** ***REMOVED***',
	'Canada','***REMOVED***','***REMOVED***','EDT', 1);

INSERT INTO [OfficeLocation].[Office] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Vancouver','***REMOVED*** ***REMOVED******REMOVED***',
	'Canada','***REMOVED***','***REMOVED***','PDT', 1);
