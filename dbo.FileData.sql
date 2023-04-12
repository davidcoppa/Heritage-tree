CREATE TABLE [dbo].[FileData] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [DateUploaded]   DATETIME2 (7)  NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [DocumentTypeId] INT            NOT NULL,
    [Size]           INT            NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [Url]            NVARCHAR (MAX) NOT NULL,
    [WebUrl]         NVARCHAR (MAX) NOT NULL,
    [MediaId]        INT            NULL,
    [UrlPreview] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_FileData] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FileData_Media_MediaId] FOREIGN KEY ([MediaId]) REFERENCES [dbo].[Media] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FileData_DocumentTypeId]
    ON [dbo].[FileData]([DocumentTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FileData_MediaId]
    ON [dbo].[FileData]([MediaId] ASC);

