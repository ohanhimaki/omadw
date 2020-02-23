USE Prime;
GO

CREATE TABLE transactions(
  Date nvarchar(15),
  saajamaksaja nvarchar(65),
  selite nvarchar(65),
  Viesti nvarchar(100),
  amount nvarchar(10)
);
GO


USE Prime;
GO

CREATE TABLE [dbo].[transactionstmp](
	[date] datetime NULL,
	[saajamaksaja] [nvarchar](65) NULL,
	[selite] [nvarchar](65) NULL,
	[viesti] [nvarchar](100) NULL,
	[amount] [decimal](6, 2) NULL
) ON [PRIMARY]
GO