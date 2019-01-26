﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace AtiehJobCore.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByName<T>(this IQueryable<T> source,
            string propertyName, bool isDescending)
        {
            if (source == null)
            {
                throw new ArgumentException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException(nameof(propertyName));
            }

            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            var propertyInfo = type.GetProperty(propertyName);
            Expression expression = Expression.Property(arg, propertyInfo ?? throw new InvalidOperationException());
            type = propertyInfo.PropertyType;

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expression, arg);

            var methodName = isDescending ? "OrderByDescending" : "OrderBy";
            var result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
            return (IQueryable<T>)result;
        }
    }
}
