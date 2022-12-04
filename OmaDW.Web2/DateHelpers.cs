namespace OmaDW.Web2;

public static class DateHelpers
{
    //datetime ToInt yyyymmdd
    public static int ToInt(this DateTime date)
    {
        return date.Year * 10000 + date.Month * 100 + date.Day;
    }
    public static List<DateTime> DatesTo(this DateTime from, DateTime thru)
    {
        var list = new List<DateTime>();
        for (var date = from.Date; date <= thru.Date; date = date.AddDays(1))
            list.Add(date);
        return list;
    }
}