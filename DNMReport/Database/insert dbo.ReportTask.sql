USE DNMReport 
GO
select * from dbo.Reporttask
select * from dbo.ReportProfile
select * from dbo.ReportSchedule
DECLARE @ReportId INT
DECLARE @ScheduleId INT
DECLARE @ReportName Varchar(50)
DECLARE @ScheduleName Varchar(50)
SET @ReportName = 'ThroughputScript.JSL'
SET @ScheduleName = 'Run Every 5 minutes'


SET @ScheduleId = ( select  ScheduleId from dbo.ReportSchedule where Name = @ScheduleName);

SET @ReportId= ( select  ReportId from dbo.ReportProfile where ReportName = @ReportName)
print @ScheduleId
print @ReportId

BEGIN TRAN
select * from dbo.Reporttask
Insert into dbo.Reporttask (TaskName, Description,ScheduleId, ReportId, CreateTime, CreateTimeUtc)
values
( 'Throughput Report', 'Execute JSL file every 5 minutes',@ScheduleId, @ReportId, GETDATE(), GETDATE()) 
select * from dbo.Reporttask
COMMIT TRAN