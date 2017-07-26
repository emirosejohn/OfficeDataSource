ALTER TABLE [OfficeLocation].[Office] ADD CountrySlug [nvarchar](255);

go 

UPDATE [OfficeLocation].[Office]
Set [CountrySlug] = 'netherlands'
where [Country] ='Netherlands';

UPDATE [OfficeLocation].[Office]
Set [CountrySlug] = 'united-states'
where [Country] ='United States';

UPDATE [OfficeLocation].[Office]
Set [CountrySlug] = 'germany'
where [Country] ='Germany';

UPDATE [OfficeLocation].[Office]
Set [CountrySlug] = 'united-kingdom'
where [Country] ='United Kingdom';

UPDATE [OfficeLocation].[Office]
Set [CountrySlug] = 'australia'
where [Country] ='Australia';

UPDATE [OfficeLocation].[Office]
Set [CountrySlug] = 'singapore'
where [Country] ='Singapore';

UPDATE [OfficeLocation].[Office]
Set [CountrySlug] = 'japan'
where [Country] ='Japan';

UPDATE [OfficeLocation].[Office]
Set [CountrySlug] = 'canada'
where [Country] ='Canada';

go

ALTER TABLE [OfficeLocation].[Office] DROP COLUMN [Country];

go