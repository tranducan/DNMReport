USE DNMReport 
GO

select * from dbo.ReportProfile

BEGIN TRAN
select * from dbo.ReportProfile
Insert into dbo.ReportProfile (ReportName, ReportPathUploaded, FileExtension,  CreateTime, CreateTimeUtc)
values
( 'ThroughputScript.JSL','C:\\temp\\Finishing\Throughput.JSL' ,'JSL', GETDATE(), GETUTCDATE()) 
select * from dbo.ReportProfile
Commit TRAN