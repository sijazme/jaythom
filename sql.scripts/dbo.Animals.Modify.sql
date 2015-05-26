CREATE TABLE [dbo].[Animals] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Type]     NVARCHAR (MAX) NOT NULL,
    [Nickname] NVARCHAR (MAX) NULL,
    [Age]      INT            NOT NULL,
    [Gender]   NVARCHAR (MAX) NOT NULL,
    [StaffId]  INT            NULL,
    CONSTRAINT [PK_dbo.Animals] PRIMARY KEY CLUSTERED ([Id] ASC)
);

