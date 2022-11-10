CREATE TABLE [dbo].[DemoRoom] (
    [Room_No]  INT        NOT NULL,
    [Hotel_No] INT        NOT NULL,
    [Types]    CHAR (1)   DEFAULT ('S') NULL,
    [Price]    FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([Room_No] ASC, [Hotel_No] ASC),
    FOREIGN KEY ([Hotel_No]) REFERENCES [dbo].[DemoHotel] ([Hotel_No]) ON UPDATE CASCADE,
    CONSTRAINT [checkType] CHECK ([Types]='S' OR [Types]='F' OR [Types]='D' OR [Types] IS NULL),
    CONSTRAINT [checkPrice] CHECK ([price]>=(0) AND [price]<=(9999))
);