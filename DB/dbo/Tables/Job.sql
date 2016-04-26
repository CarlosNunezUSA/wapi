CREATE TABLE [dbo].[Job] (
    [ID]            INT             IDENTITY (1, 1) NOT NULL,
    [SystemID]      INT             NULL,
    [RunOnce]       DATETIME        NULL,
    [RunTime]       VARCHAR (5)     NULL,
    [Monday]        BIT             NULL,
    [Tuesday]       BIT             NULL,
    [Wednesday]     BIT             NULL,
    [Thursday]      BIT             NULL,
    [Friday]        BIT             NULL,
    [Saturday]      BIT             NULL,
    [Sunday]        BIT             NULL,
    [RepeatMin]     INT             NULL,
    [RunDll]        VARCHAR (50)    NULL,
    [IsEnabled]     BIT             CONSTRAINT [DF_SystemTask_IsEnabled] DEFAULT ((0)) NULL,
    [RunParameters] VARBINARY (MAX) NULL,
    CONSTRAINT [PK_SystemTask] PRIMARY KEY CLUSTERED ([ID] ASC)
);

