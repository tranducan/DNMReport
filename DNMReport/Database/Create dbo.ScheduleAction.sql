USE DNMReport
GO

CREATE TABLE dbo.ScheduleAction
(
[ActionId] BIGINT IDENTITY(1,1) NOT NULL, 
[ActionName] VARCHAR(50) NOT NULL,
[Description] VARCHAR(300)  NULL,
[CreateTime] DATETIME2(7) NOT NULL,
[CreateTimeUtc] DATETIME2(7) NOT NULL,
[LastModifiedTime] DATETIME2(7) NOT NULL,
[LastModifiedTimeUtc] DATETIME2(7) NOT NULL,
[LastModifiedUser] VARCHAR(50) NOT NULL,
CONSTRAINT [PK_ScheduleAction] PRIMARY KEY CLUSTERED
([ActionId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY]

GO
ALTER TABLE dbo.ScheduleAction ADD CONSTRAINT [DF_ScheduleAction_LastModifiedTime]  DEFAULT (GETDATE()) FOR [LastModifiedTime]
GO

ALTER TABLE dbo.ScheduleAction ADD CONSTRAINT [DF_ScheduleAction_LastModifiedTimeUtc]  DEFAULT (GETUTCDATE()) FOR [LastModifiedTimeUtc]
GO

ALTER TABLE dbo.ScheduleAction ADD CONSTRAINT [DF_ScheduleAction_LastModifiedUser]  DEFAULT (SUSER_SNAME()) FOR [LastModifiedUser]
GO