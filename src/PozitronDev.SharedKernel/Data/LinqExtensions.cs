namespace PozitronDev.SharedKernel.Data;

public static class LinqExtensions
{
    public static bool ContainsByValue<T>(this List<IValueEquatable<T>> source, T value) where T : class
    {
        Guard.Against.Null(source, nameof(source));

        for (var i = 0; i < source.Count; i++)
        {
            if (source[i].EqualsByValue(value)) return true;
        }

        return false;
    }

    public static bool ContainsByValue<T>(this IEnumerable<IValueEquatable<T>> source, T value) where T : class
    {
        Guard.Against.Null(source, nameof(source));

        foreach (var element in source)
        {
            if (element.EqualsByValue(value)) return true;
        }

        return false;
    }

    public static List<T> DistinctByValue<T>(this IEnumerable<T> source) where T : class, IValueEquatable<T>
    {
        var distinctList = new List<T>();
        foreach (var element in source)
        {
            if (!distinctList.ContainsByValue(element))
                distinctList.Add(element);
        }
        return distinctList;
    }

}
