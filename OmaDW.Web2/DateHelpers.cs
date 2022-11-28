namespace OmaDW.Web2;

public static class DateHelpers
{
    //datetime ToInt yyyymmdd
    public static int ToInt(this DateTime date)
    {
        return date.Year * 10000 + date.Month * 100 + date.Day;
    }
}