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



INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Amsterdam', '***REMOVED*** ***REMOVED***',
	'Netherlands','***REMOVED***', '***REMOVED***', 'CEST', True);
GO

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Austin','***REMOVED***
	***REMOVED*** ***REMOVED***','United States','***REMOVED***',
	'+***REMOVED***','CST', True);


INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Berlin','***REMOVED*** Kurfürstendamm 194, 
	***REMOVED***','Germany','***REMOVED***','***REMOVED***','CET', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Charlotte','201 North Tryon Street, ste. 
	2300 ***REMOVED***','United States','***REMOVED***','***REMOVED***','EST', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('London','***REMOVED*** ***REMOVED***',
	'United Kingdom','***REMOVED***','***REMOVED***','BST', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Melbourne','***REMOVED*** ***REMOVED***',
	'Australia','***REMOVED***','','AEST', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Santa Monica','***REMOVED*** ***REMOVED***',
	'United States','***REMOVED***','***REMOVED***','PST', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Singapore','***REMOVED*** ***REMOVED***',
	'Singapore','***REMOVED***','***REMOVED***','SGT', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Sydney','***REMOVED*** ***REMOVED***',
	'Australia','***REMOVED***','***REMOVED***','AEST', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Tokyo','***REMOVED*** ***REMOVED*** ***REMOVED***',
	'Japan','***REMOVED***','***REMOVED***','JST', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Toronto','***REMOVED*** ***REMOVED***',
	'Canada','***REMOVED***','***REMOVED***','EDT', True);

INSERT INTO [OfficeLocation].[Offices] ([Name], [Address], 
	[Country], [Switchboard], [Fax], [TimeZone], 
	[Operating]) VALUES ('Vancouver','***REMOVED*** ***REMOVED******REMOVED***',
	'Canada','***REMOVED***','***REMOVED***','PDT', True);
