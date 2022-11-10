CREATE TABLE [dbo].[DemoFacility] (
    [FacilityNo] INT          IDENTITY (5, 1) NOT NULL,
    [Name]       VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([FacilityNo] ASC)
);