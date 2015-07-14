CREATE TABLE [dbo].[Books] (
    [Cover]       VARCHAR (MAX) NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [PublishDate] DATE          NOT NULL,
    [AuthorId]    INT           NOT NULL,
    [BookId]      INT           NOT NULL, 
    CONSTRAINT[PK_Book] PRIMARY KEY ([BookId])
);

