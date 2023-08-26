using PozitronDev.SharedKernel.Data;

namespace PozitronDev.Extensions.Data;

public static class PaginationExtensions
{
    public static async Task<(IQueryable<T> Query, Pagination Pagination)> ApplyPaging<T>(
        this IQueryable<T> source, BaseFilter filter)
    {
        var count = await source.CountAsync();
        var pagination = new Pagination(count, filter);

        return (source.ApplyPaging(pagination), pagination);
    }

    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> source, Pagination pagination)
    {
        if (pagination.Skip != 0)
        {
            source = source.Skip(pagination.Skip);
        }

        source = source.Take(pagination.Take);

        return source;
    }

    public static IQueryable<T> ApplyGenericOrdering<T>(this IQueryable<T> source, BaseFilter filter)
    {
        return source.ApplyGenericOrdering(filter.SortBy, filter.OrderBy);
    }

    public static IQueryable<T> ApplyGenericOrdering<T>(this IQueryable<T> source, string? sortBy, string? orderBy)
    {
        var hasId = typeof(T).IsSubclassOf(typeof(BaseEntity));

        // If the input is null and the type is not BaseEntity, do not apply ordering.
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            if (!hasId) return source;

            sortBy = nameof(BaseEntity.Id);
        }

        var propertyName = GetPropertyName(typeof(T), hasId, sortBy);

        // If not found do not apply ordering at all. Initially I threw exception, but ordering is not so critical task to break the request.
        if (propertyName is null) return source;

        var keySelector = CreateExpression2<T>(propertyName);

        if (orderBy is not null && orderBy.Equals("desc", StringComparison.OrdinalIgnoreCase))
        {
            source = source.OrderByDescending(keySelector);
        }
        else
        {
            source = source.OrderBy(keySelector);
        }

        return source;
    }

    private static Expression<Func<T, object>> CreateExpression2<T>(string propertyName)
    {
        var paramExpr = Expression.Parameter(typeof(T), "x");
        Expression body = paramExpr;

        foreach (var member in propertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, member);
        }

        var convertedExpr = Expression.Convert(body, typeof(object));

        return Expression.Lambda<Func<T, object>>(convertedExpr, paramExpr);
    }

    private static string? GetPropertyName(Type type, bool hasId, string sortBy)
    {
        var matchedProperty =
            type.GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (matchedProperty is not null) return matchedProperty.Name;

        var valueObjectProperties = type.GetProperties().Where(x => x.PropertyType.BaseType == typeof(ValueObject));

        foreach (var property in valueObjectProperties)
        {
            var innerProperty = property.PropertyType.GetProperty(sortBy,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (innerProperty is not null)
            {
                return $"{property.Name}.{innerProperty.Name}";
            }
        }

        return hasId ? nameof(BaseEntity.Id) : null;
    }
}
