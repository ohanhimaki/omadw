use Prime;
go

create schema dw;
go


DECLARE @StartDate  date = '20180101';

DECLARE @CutoffDate date = DATEADD(DAY, -1, DATEADD(YEAR, 30, @StartDate));

;WITH seq(n) AS
                          (
                              SELECT 0 UNION ALL SELECT n + 1 FROM seq
                              WHERE n < DATEDIFF(DAY, @StartDate, @CutoffDate)
                          ),
      d(d) AS
                          (
                              SELECT DATEADD(DAY, n, @StartDate) FROM seq
                          ),
      src AS
                          (
                              SELECT
                                      TheDate         = CONVERT(date, d),
                                      TheDay          = DATEPART(DAY,       d),
                                      TheDayName      = DATENAME(WEEKDAY,   d),
                                      TheWeek         = DATEPART(WEEK,      d),
                                      TheISOWeek      = DATEPART(ISO_WEEK,  d),
                                      TheDayOfWeek    = DATEPART(WEEKDAY,   d),
                                      TheMonth        = DATEPART(MONTH,     d),
                                      TheMonthName    = DATENAME(MONTH,     d),
                                      TheQuarter      = DATEPART(Quarter,   d),
                                      TheYear         = DATEPART(YEAR,      d),
                                      TheFirstOfMonth = DATEFROMPARTS(YEAR(d), MONTH(d), 1),
                                      TheLastOfYear   = DATEFROMPARTS(YEAR(d), 12, 31),
                                      TheDayOfYear    = DATEPART(DAYOFYEAR, d)
                              FROM d
                          )
 SELECT * 
into dw.DimTime
FROM src
 ORDER BY TheDate
                     OPTION (MAXRECURSION 0);