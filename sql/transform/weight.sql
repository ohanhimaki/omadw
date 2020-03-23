INSERT INTO weighttmp
SELECT  CAST([Date] as datetime) Date
      ,cast(REPLACE([Weight] , '"' , '') as decimal(5,2)) Weight
      ,cast(REPLACE([BMI] , '"' , '') as decimal(5,2)) BMI
      ,cast(REPLACE([BodyFat] , '"' , '') as decimal(5,2)) BodyFat
      ,cast(REPLACE([Water] , '"' , '') as decimal(5,2)) Water
      ,cast(REPLACE([Muscle] , '"' , '') as decimal(5,2)) Muscle
      ,cast(REPLACE([Bone] , '"' , '') as decimal(5,2)) Bone
  FROM [Prime].[dbo].[weight]