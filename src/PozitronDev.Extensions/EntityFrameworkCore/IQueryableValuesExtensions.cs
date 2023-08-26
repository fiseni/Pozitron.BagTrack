using BlazarTech.QueryableValues;
using BlazarTech.QueryableValues.Builders;
using System.Data;

namespace PozitronDev.Extensions.EntityFrameworkCore;

public static class IQueryableValuesExtensions
{
    private static readonly MethodInfo _asQueryableValuesMethodInfo = typeof(QueryableValuesDbContextExtensions)
            .GetTypeInfo()
            .GetDeclaredMethods(nameof(QueryableValuesDbContextExtensions.AsQueryableValues))
            .Single(mi => mi.GetParameters().Length == 3
                && mi.GetGenericArguments().Length == 1
                && mi.GetParameters()[0].ParameterType == typeof(DbContext)
                && mi.GetParameters()[1].ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>));

    private static readonly MethodInfo _asQueryableValuesMethodInfoForString = typeof(QueryableValuesDbContextExtensions)
            .GetTypeInfo()
            .GetDeclaredMethods(nameof(QueryableValuesDbContextExtensions.AsQueryableValues))
            .Single(mi => mi.GetParameters().Length == 3
                && mi.GetParameters()[0].ParameterType == typeof(DbContext)
                && mi.GetParameters()[1].ParameterType == typeof(IEnumerable<string>));

    private static readonly MethodInfo _containsQueryableMethodInfo = typeof(Queryable)
            .GetTypeInfo()
            .GetDeclaredMethods(nameof(Queryable.Contains))
            .Single(mi => mi.GetParameters().Length == 2
                && mi.GetGenericArguments().Length == 1
                && mi.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>)
                && mi.GetParameters()[1].ParameterType.IsGenericParameter);

    private static readonly MethodInfo _containsEnumerableMethodInfo = typeof(Enumerable)
            .GetTypeInfo()
            .GetDeclaredMethods(nameof(Queryable.Contains))
            .Single(mi => mi.GetParameters().Length == 2
                && mi.GetGenericArguments().Length == 1
                && mi.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                && mi.GetParameters()[1].ParameterType.IsGenericParameter);

    public static IQueryable<TEntity> In<TEntity, TKey>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, TKey>> keySelector,
        IEnumerable<TKey> values,
        DbContext dbContext,
        Action<EntityOptionsBuilder<TKey>>? configure = null)
        => dbContext.Database.IsSqlServer()
            ? InSQL(source, keySelector, values, dbContext, configure)
            : InMemory(source, keySelector, values);

    public static IQueryable<TEntity> In<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, string?>> keySelector,
        IEnumerable<string> values,
        DbContext dbContext,
        bool isUnicode = true)
        => dbContext.Database.IsSqlServer()
            ? InSQL(source, keySelector, values, dbContext, isUnicode)
            : InMemory(source, keySelector, values);

    private static IQueryable<TEntity> InSQL<TEntity, TKey>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, TKey>> keySelector,
        IEnumerable<TKey> values,
        DbContext dbContext,
        Action<EntityOptionsBuilder<TKey>>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(keySelector);

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var propertySelector = ParameterReplacerVisitor.Replace(keySelector, keySelector.Parameters[0], parameter) as LambdaExpression;
        _ = propertySelector ?? throw new InvalidExpressionException();

        // Get generic methodInfo for AsQueryableValues.
        var asQueryableValuesMethod = _asQueryableValuesMethodInfo.MakeGenericMethod(typeof(TKey));

        // Create closures so EF can parameterize the query.
        var dbContextAsExpression = ((Expression<Func<DbContext>>)(() => dbContext)).Body;
        var valuesAsExpression = ((Expression<Func<IEnumerable<TKey>>>)(() => values)).Body;
        var configurationAsExpression = ((Expression<Func<Action<EntityOptionsBuilder<TKey>>?>>)(() => configure)).Body;

        // Create an expression for the AsQueryableValues method.
        var asQueryableValuesExpression = Expression.Call(
            null,
            asQueryableValuesMethod,
            dbContextAsExpression,
            valuesAsExpression,
            configurationAsExpression);

        // Get generic methodInfo for Contains.
        var containsMethod = _containsQueryableMethodInfo.MakeGenericMethod(typeof(TKey));

        // Create an expression for the Contains method.
        var containsExpression = Expression.Call(
            null,
            containsMethod,
            asQueryableValuesExpression,
            propertySelector.Body);

        // Create the final lambda expression
        var whereLambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);

        // Use the expression with the Where method
        var result = source.Where(whereLambda);

        return result;
    }

    // I was forced to create a separate overload in case of string type.
    // The generic one does not respect the options properly. The related issue is here:
    // https://github.com/yv989c/BlazarTech.QueryableValues/issues/30
    private static IQueryable<TEntity> InSQL<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, string?>> keySelector,
        IEnumerable<string> values,
        DbContext dbContext,
        bool isUnicode = true)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(keySelector);

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var propertySelector = ParameterReplacerVisitor.Replace(keySelector, keySelector.Parameters[0], parameter) as LambdaExpression;
        _ = propertySelector ?? throw new InvalidExpressionException();

        // Get generic methodInfo for AsQueryableValues.
        var asQueryableValuesMethod = _asQueryableValuesMethodInfoForString;

        // Create closures so EF can parameterize the query.
        var dbContextAsExpression = ((Expression<Func<DbContext>>)(() => dbContext)).Body;
        var valuesAsExpression = ((Expression<Func<IEnumerable<string>>>)(() => values)).Body;
        var isUnicodeAsExpression = ((Expression<Func<bool>>)(() => isUnicode)).Body;

        // Create an expression for the AsQueryableValues method.
        var asQueryableValuesExpression = Expression.Call(
            null,
            asQueryableValuesMethod,
            dbContextAsExpression,
            valuesAsExpression,
            isUnicodeAsExpression);

        // Get generic methodInfo for Contains.
        var containsMethod = _containsQueryableMethodInfo.MakeGenericMethod(typeof(string));

        // Create an expression for the Contains method.
        var containsExpression = Expression.Call(
            null,
            containsMethod,
            asQueryableValuesExpression,
            propertySelector.Body);

        // Create the final lambda expression
        var whereLambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);

        // Use the expression with the Where method
        var result = source.Where(whereLambda);

        return result;
    }

    private static IQueryable<TEntity> InMemory<TEntity, TKey1, TKey2>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, TKey1>> keySelector,
        IEnumerable<TKey2> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        ArgumentNullException.ThrowIfNull(keySelector);

        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var propertySelector = ParameterReplacerVisitor.Replace(keySelector, keySelector.Parameters[0], parameter) as LambdaExpression;
        _ = propertySelector ?? throw new InvalidExpressionException();

        // Create closures so EF can parameterize the query.
        var valuesAsExpression = ((Expression<Func<IEnumerable<TKey2>>>)(() => values)).Body;

        // Get MethodInfo for Contains.
        var containsMethod = _containsEnumerableMethodInfo.MakeGenericMethod(typeof(TKey1));

        // Create an expression for the Contains method.
        var containsExpression = Expression.Call(
            null,
            containsMethod,
            valuesAsExpression,
            propertySelector.Body);

        // Create the final lambda expression
        var whereLambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);

        // Use the expression with the Where method
        var result = source.Where(whereLambda);

        return result;
    }

    public static IQueryable<TEntity> InJoin<TEntity, TKey>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, TKey>> keySelector,
        IEnumerable<TKey> values,
        DbContext dbContext,
        Action<EntityOptionsBuilder<TKey>>? configure = null) where TKey : notnull
        => dbContext.Database.IsSqlServer()
            ? source.Join(dbContext.AsQueryableValues(values, configure), keySelector, v => v, (x, v) => x)
            : InMemory(source, keySelector, values);

    public static IQueryable<TEntity> InJoin<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, Guid?>> keySelector,
        IEnumerable<Guid> values,
        DbContext dbContext)
        => dbContext.Database.IsSqlServer()
            ? source.Join(dbContext.AsQueryableValues(values), keySelector, v => v, (x, v) => x)
            : InMemory(source, keySelector, values);

    public static IQueryable<TEntity> InJoin<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, int?>> keySelector,
        IEnumerable<int> values,
        DbContext dbContext)
        => dbContext.Database.IsSqlServer()
            ? source.Join(dbContext.AsQueryableValues(values), keySelector, v => v, (x, v) => x)
            : InMemory(source, keySelector, values);

    public static IQueryable<TEntity> InJoin<TEntity>(
        this IQueryable<TEntity> source,
        Expression<Func<TEntity, string?>> keySelector,
        IEnumerable<string> values,
        DbContext dbContext,
        bool isUnicode = true)
        => dbContext.Database.IsSqlServer()
            ? source.Join(dbContext.AsQueryableValues(values, isUnicode), keySelector, v => v, (x, v) => x)
            : InMemory(source, keySelector, values);

    public static IQueryable<TEntity> InJoin<TEntity, TCollectionEntity, TRelationshipKey, TKey>(
        this IQueryable<TEntity> source,
        DbContext dbContext,
        IQueryable<TCollectionEntity> collection,
        IEnumerable<TKey> values,
        Expression<Func<TEntity, TRelationshipKey>> outerKeySelector,
        Expression<Func<TCollectionEntity, TRelationshipKey>> innerKeySelector,
        Expression<Func<TCollectionEntity, TKey>> keySelector,
        Expression<Func<TCollectionEntity, bool>>? additionalCondition = null,
        Action<EntityOptionsBuilder<TKey>>? configure = null) where TKey : notnull
    {
        collection = additionalCondition is null ? collection : collection.Where(additionalCondition);

        var collectionQuery = collection
            .InJoin(keySelector, values, dbContext, configure)
            .Select(innerKeySelector)
            .Distinct();

        return source.Join(collectionQuery, outerKeySelector, x => x, (x, y) => x);
    }

    public static IQueryable<TEntity> InJoin<TEntity, TCollectionEntity, TRelationshipKey>(
        this IQueryable<TEntity> source,
        DbContext dbContext,
        IQueryable<TCollectionEntity> collection,
        IEnumerable<Guid> values,
        Expression<Func<TEntity, TRelationshipKey>> outerKeySelector,
        Expression<Func<TCollectionEntity, TRelationshipKey>> innerKeySelector,
        Expression<Func<TCollectionEntity, Guid?>> keySelector,
        Expression<Func<TCollectionEntity, bool>>? additionalCondition = null)
    {
        collection = additionalCondition is null ? collection : collection.Where(additionalCondition);

        var collectionQuery = collection
            .InJoin(keySelector, values, dbContext)
            .Select(innerKeySelector)
            .Distinct();

        return source.Join(collectionQuery, outerKeySelector, x => x, (x, y) => x);
    }

    public static IQueryable<TEntity> InJoin<TEntity, TCollectionEntity, TRelationshipKey>(
        this IQueryable<TEntity> source,
        DbContext dbContext,
        IQueryable<TCollectionEntity> collection,
        IEnumerable<int> values,
        Expression<Func<TEntity, TRelationshipKey>> outerKeySelector,
        Expression<Func<TCollectionEntity, TRelationshipKey>> innerKeySelector,
        Expression<Func<TCollectionEntity, int?>> keySelector,
        Expression<Func<TCollectionEntity, bool>>? additionalCondition = null)
    {
        collection = additionalCondition is null ? collection : collection.Where(additionalCondition);

        var collectionQuery = collection
            .InJoin(keySelector, values, dbContext)
            .Select(innerKeySelector)
            .Distinct();

        return source.Join(collectionQuery, outerKeySelector, x => x, (x, y) => x);
    }

    public static IQueryable<TEntity> InJoin<TEntity, TCollectionEntity, TRelationshipKey>(
        this IQueryable<TEntity> source,
        DbContext dbContext,
        IQueryable<TCollectionEntity> collection,
        IEnumerable<string> values,
        Expression<Func<TEntity, TRelationshipKey>> outerKeySelector,
        Expression<Func<TCollectionEntity, TRelationshipKey>> innerKeySelector,
        Expression<Func<TCollectionEntity, string?>> keySelector,
        Expression<Func<TCollectionEntity, bool>>? additionalCondition = null)
    {
        collection = additionalCondition is null ? collection : collection.Where(additionalCondition);

        var collectionQuery = collection
            .InJoin(keySelector, values, dbContext)
            .Select(innerKeySelector)
            .Distinct();

        return source.Join(collectionQuery, outerKeySelector, x => x, (x, y) => x);
    }
}
