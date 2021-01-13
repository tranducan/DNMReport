USE DNMReport 
GO

DECLARE @ActionID INT
DECLARE @TriggerID INT
DECLARE @ActionName Varchar(50)
DECLARE @TriggerName Varchar(50)
SET @ActionName = 'Run JMP'
SET @TriggerName = 'Every'

SET @ActionID = ( select  ActionId from dbo.ScheduleAction where ActionName = @ActionName);

SET @TriggerID = ( select  TriggerId from dbo.ScheduleTrigger where TrigerName = @TriggerName)

BEGIN TRAN
select * from dbo.ReportSchedule
Insert into dbo.ReportSchedule (TriggerId,NAME, Description, ActionId, Minute, Hour, DAY, Date, Month, CreateTime, CreateTimeUtc)
values
( @TriggerID,'Run Every 5 minutes', 'Execute JMP file every 5 minutes', @ActionID, '5','','','','', GETDATE(), GETDATE()) 
select * from dbo.ReportSchedule
COMMIT TRAN