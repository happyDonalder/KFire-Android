﻿CREATE TABLE [dbo].[Dimension_Operation] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Operation] NVARCHAR (18) NOT NULL,
    CONSTRAINT [PK_Dimension_Operation] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [Dimension_Operation_NCI_Operation]
    ON [dbo].[Dimension_Operation]([Operation] ASC);

