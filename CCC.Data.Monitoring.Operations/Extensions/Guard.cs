using System;
using System.Collections.Generic;
using System.Linq;

namespace CCC.Data.Monitoring.Operations.Extensions
{
    public interface IGuardClause { }
    public class Guard : IGuardClause
    {
        public static IGuardClause Against { get; } = new Guard();
        private Guard() { }
    }

    public static class GuardClauseExtensions
    {
        public static void Null(this IGuardClause guardClause, object input, string parameterName)
        {
            if (input == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
        public static bool IsNull(this IGuardClause guardClause, object input)
        {
            return input == null;
        }
        public static void NullOrEmpty(this IGuardClause guardClause, string input, string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (input == string.Empty)
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }
        public static bool IsNullOrEmpty(this IGuardClause guardClause, string input)
        {
            bool isNull = Guard.Against.IsNull(input);
            return isNull && input == string.Empty;

        }
        public static void NullOrEmpty<T>(this IGuardClause guardClause, IEnumerable<T> input, string parameterName)
        {
            Guard.Against.Null(input, parameterName);
            if (!input.Any())
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        }
        public static void NullOrWhiteSpace(this IGuardClause guardClause, string input, string parameterName)
        {
            Guard.Against.NullOrEmpty(input, parameterName);
            if (String.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
            }
        } 
    }
}
