
USE Prime;
GO

CREATE TABLE weighttmp(
  Date Datetime,
  Weight decimal(6,2),
  BMI decimal(6,2),
  BodyFat decimal(6,2),
  Water decimal(6,2),
  Muscle decimal(6,2),
  Bone decimal(6,2)
  
);
GO



USE Prime;
GO

CREATE TABLE weight(
  Date Datetime,
  Time nvarchar(max),
  Weight nvarchar(max),
  BMI nvarchar(max),
  BodyFat nvarchar(max),
  Water nvarchar(max),
  Muscle nvarchar(max),
  Bone nvarchar(max),
  Comment nvarchar(max),
  Medication nvarchar(max)
);
GO