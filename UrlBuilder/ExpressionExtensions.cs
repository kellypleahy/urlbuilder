using System;
using System.Linq.Expressions;
using System.Reflection;

namespace UrlBuilder
{
    public static class ExpressionExtensions
    {
        public static void ForEachParameter(this LambdaExpression methodCallExpression, Action<ParameterInfo, object> handler)
        {
            var methodCall = methodCallExpression.Body as MethodCallExpression;
            if (methodCall == null)
                return;

            var parameterInfos = methodCall.Method.GetParameters();
            for (var index = 0; index < methodCall.Arguments.Count; index++)
            {
                var argument = methodCall.Arguments[index];
                var paramInfo = parameterInfos[index];
                var argumentValue = Expression.Lambda(argument).Compile().DynamicInvoke();
                handler(paramInfo, argumentValue);
            }
        }

        public static string GetMethodName(this LambdaExpression methodCallExpression)
        {
            var methodCall = methodCallExpression.Body as MethodCallExpression;
            if(methodCall == null)
                throw new InvalidOperationException("Expected a method call expression");

            return methodCall.Method.Name;
        }
    }
}