CREATE TABLE [dbo].[Fish] (
    [FishId]        INT             IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX)  NOT NULL,
    [Description]   NVARCHAR (MAX)  NOT NULL,
    [Category]      NVARCHAR (MAX)  NOT NULL,
    [Price]         DECIMAL (18, 2) NOT NULL,
    [ImageData]     VARBINARY (MAX) NULL,
    [ImageMimeType] VARCHAR (50)    NULL,
    CONSTRAINT [PK_dbo.Fish] PRIMARY KEY CLUSTERED ([FishId] ASC)
);

