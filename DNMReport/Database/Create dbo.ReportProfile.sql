CREATE TABLE dbo.ReportProfile
(
[ReportID] BIGINT IDENTITY(1,1) NOT NULL, 
[ReportName] VARCHAR(50) NOT NULL,
[ReportPathUploaded] VARCHAR(500)  NULL,
[FileExtension] VARCHAR(20)  NULL,
[CreateTime] DATETIME2(7) NOT NULL,
[CreateTimeUtc] DATETIME2(7) NOT NULL,
[LastModifiedTime] [DATETIME2](7) NOT NULL,
[LastModifiedTimeUtc] [DATETIME2](7) NOT NULL,
[LastModifiedUser] [VARCHAR](50) NOT NULL,
CONSTRAINT [PK_ReportProfile] PRIMARY KEY CLUSTERED
([ReportID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)ON [PRIMARY]

GO
ALTER TABLE dbo.ReportProfile ADD CONSTRAINT [DF_ReportProfile_LastModifiedTime]  DEFAULT (GETDATE()) FOR [LastModifiedTime]
GO

ALTER TABLE dbo.ReportProfile ADD CONSTRAINT [DF_ReportProfile_LastModifiedTimeUtc]  DEFAULT (GETUTCDATE()) FOR [LastModifiedTimeUtc]
GO

ALTER TABLE dbo.ReportProfile ADD CONSTRAINT [dbo.ReportProfile_LastModifiedUser]  DEFAULT (SUSER_SNAME()) FOR [LastModifiedUser]
GO