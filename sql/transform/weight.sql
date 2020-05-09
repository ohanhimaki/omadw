INSERT INTO Prime.dbo.weighttmp
SELECT  CAST(w.[Date] as datetime) Date
      ,cast(REPLACE(w.[Weight] , '"' , '') as decimal(5,2)) Weight
      ,cast(REPLACE(w.[BMI] , '"' , '') as decimal(5,2)) BMI
      ,cast(REPLACE(w.[BodyFat] , '"' , '') as decimal(5,2)) BodyFat
      ,cast(REPLACE(w.[Water] , '"' , '') as decimal(5,2)) Water
      ,cast(REPLACE(w.[Muscle] , '"' , '') as decimal(5,2)) Muscle
      ,cast(REPLACE(w.[Bone] , '"' , '') as decimal(5,2)) Bone
  FROM [Prime].[dbo].[weight] w
  left join prime.dbo.weighttmp wt on CAST(w.[Date] as datetime) = wt.[Date]
  where wt.Date is null