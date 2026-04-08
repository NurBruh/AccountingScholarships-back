USE [EPVO_test]
GO

CREATE TABLE [dbo].[STUDENT_SYNC_LOG] (
    [Id]            INT             IDENTITY(1,1)   NOT NULL,
    [StudentId]     INT             NOT NULL,
    [IinPlt]        NVARCHAR(12)    NULL,
    [SentAt]        DATETIME2       NOT NULL,
    [Status]        NVARCHAR(20)    NOT NULL,   -- Pending / Success / Error
    [RequestBody]   NVARCHAR(MAX)   NULL,
    [ResponseBody]  NVARCHAR(MAX)   NULL,
    [ErrorMessage]  NVARCHAR(MAX)   NULL,
    [EpvoEndpoint]  NVARCHAR(500)   NULL,

    CONSTRAINT [PK_STUDENT_SYNC_LOG] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Индекс для быстрого поиска по студенту
CREATE INDEX [IX_STUDENT_SYNC_LOG_StudentId] ON [dbo].[STUDENT_SYNC_LOG] ([StudentId]);
GO

-- Индекс для фильтрации по статусу
CREATE INDEX [IX_STUDENT_SYNC_LOG_Status] ON [dbo].[STUDENT_SYNC_LOG] ([Status]);
GO
