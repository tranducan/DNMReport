USE DNMReport

BEGIN TRAN
select 'before', * from dbo.ScheduleAction
Insert into dbo.ScheduleAction (ActionName, Description,CreateTime, CreateTimeUtc)
values
('Run JMP','Execute JMP file', GETDATE(), GETDATE()),
('Run JSL','Execute JSL file', GETDATE(), GETDATE()),
('Run EXE','Execute EXE file', GETDATE(), GETDATE()),
('Send SMS','Send SMS', GETDATE(), GETDATE())
select 'after', * from dbo.ScheduleAction
COMMIT TRAN