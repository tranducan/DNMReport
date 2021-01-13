USE DNMReport

BEGIN TRAN
select 'before', * from dbo.ScheduleTrigger
Insert into dbo.ScheduleTrigger (TrigerName, Description,CreateTime, CreateTimeUtc)
values
('Every','Run task every period', GETDATE(), GETDATE()),
('Daily','Run Task every day at the time frame', GETDATE(), GETDATE()),
('Weekly','Run Task every Week at the time frame', GETDATE(), GETDATE()),
('Monthly','Run Task every Month at the time frame', GETDATE(), GETDATE())
select 'after', * from dbo.ScheduleTrigger
COMMIT TRAN