using Ardalis.GuardClauses;
using Core.Utils;
using System.Linq.Expressions;

namespace Core.EntityFramework.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// It's like: () => User?.Address?.City ?? "DefaultCity".
        /// Can be compiled to SQL.
        /// </summary>
        /// <param name="selector">Lambda like: () => User.Address.City</param>
        /// <param name="defaultValue">Default value like: "DefaultCity"</param>
        public static IQueryable<TResult> SelectOrDefault<TSource, TResult>(
            this IQueryable<TSource> query,
            Expression<Func<TSource, TResult>> selector,
            TResult defaultValue = default!)
        {
            Defend.Against.Null(query, nameof(query));
            Defend.Against.Null(selector, nameof(selector));
            Defend.Against.InvalidMemberAccessChain(selector, nameof(selector));

            var expression = ExpressionUtils.GetNestedMemberOrDefaultLambda(selector, defaultValue);
            return query.Select(expression);
        }

        /// <summary>
        /// Filter <paramref name="query"/> by values with so members (<paramref name="selector"/>) that contains all the <paramref name="valuesToFind"/>.
        /// Can be compiled to SQL.
        /// </summary>
        /// <param name="selector">>Lambda like: () => User.Address.City</param>
        /// <param name="valuesToFind">Values that must be contained in query element. For example: City1 and City2.</param>
        public static IQueryable<TSource> WhereContains<TSource, TEnumerableProperty, TType>(
            this IQueryable<TSource> query,
            Expression<Func<TSource, TEnumerableProperty>> selector,
            IEnumerable<TType> valuesToFind)
                where TEnumerableProperty : IEnumerable<TType>
                where TType : class
        {
            Defend.Against.Null(query, nameof(query));
            Defend.Against.Null(selector, nameof(selector));
            Defend.Against.Null(valuesToFind, nameof(valuesToFind));
            Defend.Against.LessElementsQuantity(valuesToFind, 1, nameof(valuesToFind));
            Defend.Against.InvalidMemberAccessChain(selector, out var castedMemberAccessExpression, nameof(selector));

            var sourceParam = Expression.Parameter(typeof(TSource));

            LambdaExpression buildLambdaForAnyMethod(int valuesToFindIndex)
            {
                // Build: p => p == valuesToFind[0]
                var propParam = Expression.Parameter(typeof(TType));
                var equalExpression = Expression.Equal(propParam, Expression.Constant(valuesToFind.ElementAt(valuesToFindIndex)));
                return Expression.Lambda(equalExpression, propParam);
            }

            MethodCallExpression buildAnyCall(int valuesToFindIndex)
            {
                // Build: source.Member.Any(p => p == valuesToFind[0])
                var anyMethodLambda = buildLambdaForAnyMethod(valuesToFindIndex);

                var anyMethod = typeof(Enumerable).GetMethods()
                    .First(x => x.Name == nameof(Enumerable.Any) && x.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(TType));

                var memberAccess = ExpressionUtils.ApplyMemberAccessChainToOtherExpression(castedMemberAccessExpression, sourceParam);

                return Expression.Call(anyMethod, memberAccess, anyMethodLambda);
            }

            Expression<Func<TSource, bool>> buildConcatenatedWithAndOperator_AnyCalls_ForEachValueToFind()
            {
                // build: source => source.Member.Any(p => p == valuesToFind[0]) && source.Member.Any(p => p == valuesToFind[1])
                var anyCalls = new List<Expression>();
                for (int i = 0; i < valuesToFind.Count(); i++)
                {
                    anyCalls.Add(buildAnyCall(i));
                }
                var lambdaBody = anyCalls.Aggregate(Expression.AndAlso);
                return Expression.Lambda<Func<TSource, bool>>(lambdaBody, sourceParam);
            }

            var filterLambda = buildConcatenatedWithAndOperator_AnyCalls_ForEachValueToFind();
            return query.Where(filterLambda);
        }

        /// <summary>
        /// <inheritdoc cref="WhereContains{TSource, TEnumerableProperty, TType}(IQueryable{TSource}, Expression{Func{TSource, TEnumerableProperty}}, IEnumerable{TType})"/>
        /// </summary>
        public static IQueryable<TSource> WhereContains<TSource, TEnumerableProperty, TType>(
            this IQueryable<TSource> query,
            Expression<Func<TSource, TEnumerableProperty>> selector,
            params TType[] valuesToFind)
                where TEnumerableProperty : IEnumerable<TType>
                where TType : class
        {
            return WhereContains(query, selector, valuesToFind.ToList());
        }

        /// <summary>
        /// Revert a boolean func expression body.
        /// <br></br>
        /// Example: from (x => x.IsArchive) build (x => !x.IsArchive).
        /// Can be compiled to SQL.
        /// </summary>
        public static Expression<Func<T, bool>> Invert<T>(this Expression<Func<T, bool>> e)
        {
            return ExpressionUtils.Invert(e);
        }

        /// <summary>
        /// If <paramref name="condition"/> is true then apply <paramref name="predicate"/> to <paramref name="query"/> with <see cref="Queryable.Where{TSource}(IQueryable{TSource}, Expression{Func{TSource, bool}})"/>.
        /// </summary>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> query,
            Func<bool> condition,
            Expression<Func<TSource, bool>> predicate)
        {
            Guard.Against.Null(query, nameof(query));
            Guard.Against.Null(predicate, nameof(predicate));
            Guard.Against.Null(condition, nameof(condition));

            if (condition.Invoke())
            {
                query = query.Where(predicate);
            }
            return query;
        }

        /// <summary>
        /// If <paramref name="condition"/> is true then apply <paramref name="predicate"/> to <paramref name="query"/> with <see cref="Queryable.Where{TSource}(IQueryable{TSource}, Expression{Func{TSource, bool}})"/>.
        /// </summary>
        public static IQueryable<TSource> WhereIf<TSource>(
            this IQueryable<TSource> query,
            bool condition,
            Expression<Func<TSource, bool>> predicate)
        {
            Guard.Against.Null(query, nameof(query));
            Guard.Against.Null(predicate, nameof(predicate));
            Guard.Against.Null(condition, nameof(condition));

            if (condition)
            {
                query = query.Where(predicate);
            }
            return query;
        }
    }
}