USE [EPVO_test]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STUDENT_CHANGE_LOG]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[STUDENT_CHANGE_LOG] (
        [Id]              BIGINT         IDENTITY(1,1) NOT NULL,
        [IinPlt]          NVARCHAR(12)   NULL,
        [FieldName]       NVARCHAR(100)  NOT NULL,
        [OldValue]        NVARCHAR(1000) NULL,
        [NewValue]        NVARCHAR(1000) NULL,
        [DataSource]      NVARCHAR(20)   NULL,
        [ChangedAt]       DATETIME2      NOT NULL,
        [ChangedBy]       NVARCHAR(256)  NULL,
        [SyncSessionId]   NVARCHAR(50)   NULL,

        CONSTRAINT [PK_STUDENT_CHANGE_LOG] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    CREATE NONCLUSTERED INDEX [IX_STUDENT_CHANGE_LOG_IinPlt]
        ON [dbo].[STUDENT_CHANGE_LOG] ([IinPlt]);

    CREATE NONCLUSTERED INDEX [IX_STUDENT_CHANGE_LOG_ChangedAt]
        ON [dbo].[STUDENT_CHANGE_LOG] ([ChangedAt]);

    CREATE NONCLUSTERED INDEX [IX_STUDENT_CHANGE_LOG_SyncSessionId]
        ON [dbo].[STUDENT_CHANGE_LOG] ([SyncSessionId]);
END
GO
