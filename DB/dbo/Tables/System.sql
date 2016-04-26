CREATE TABLE [dbo].[System] (
    [ID]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (50) NULL,
    [StatusId] INT          CONSTRAINT [DF_System_StatusId] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_System] PRIMARY KEY CLUSTERED ([ID] ASC)
);

