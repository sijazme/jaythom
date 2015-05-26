CREATE TABLE [dbo].[Animal]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Type] VARCHAR(50) NOT NULL, 
    [Nickname] VARCHAR(50) NULL, 
    [Age] INT NOT NULL, 
    [Gender] VARCHAR(50) NOT NULL,
	[StaffId] INT NOT NULL,  
    CONSTRAINT [FK_Animal_ToZooKeeper] FOREIGN KEY (StaffId) REFERENCES [Staff]([Id]) 
)
