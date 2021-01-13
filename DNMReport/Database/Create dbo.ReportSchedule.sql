CREATE TABLE dbo.ReportSchedule
(
[ScheduleID] BIGINT IDENTITY(1,1) NOT NULL, 
[TriggerId] INT NOT NULL,
[NAME] VARCHAR(50) NOT  NULL,
[Description] VARCHAR(300)  NULL,
[ActionId] INT  NULL,
[Minute] varchar(10)  NULL,
[Hour] VARCHAR(10) NULL,
[Day] VARCHAR(10) NULL,
[Date] VARCHAR(10) NULL,
[Month] VARCHAR(10) NULL,
[CreateTime] DATETIME2(7) NOT NULL,
[CreateTimeUtc] DATETIME2(7) NOT NULL,
[LastModifiedTime] [DATETIME2](7) NOT NULL,
[LastModifiedTimeUtc] [DATETIME2](7) NOT NULL,
[LastModifiedUser] [VARCHAR](50) NOT NULL,
CONSTRAINT [PK_ReportSchedule] PRIMARY KEY CLUSTERED
([ScheduleID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY]

GO
ALTER TABLE dbo.ReportSchedule ADD CONSTRAINT [DF_ReportSchedule_LastModifiedTime]  DEFAULT (GETDATE()) FOR [LastModifiedTime]
GO

ALTER TABLE dbo.ReportSchedule ADD CONSTRAINT [DF_ReportSchedule_LastModifiedTimeUtc]  DEFAULT (GETUTCDATE()) FOR [LastModifiedTimeUtc]
GO

ALTER TABLE dbo.ReportSchedule ADD CONSTRAINT [dbo.ReportSchedule_LastModifiedUser]  DEFAULT (SUSER_SNAME()) FOR [LastModifiedUser]
GO