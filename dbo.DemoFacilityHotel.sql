CREATE TABLE [dbo].[DemoFacilityHotel] (
    [FacilityNo] INT NOT NULL,
    [Hotel_No]   INT NOT NULL,
    PRIMARY KEY CLUSTERED ([FacilityNo] ASC, [Hotel_No] ASC),
    FOREIGN KEY ([Hotel_No]) REFERENCES [dbo].[DemoHotel] ([Hotel_No]),
    FOREIGN KEY ([FacilityNo]) REFERENCES [dbo].[DemoFacility] ([FacilityNo])
);

