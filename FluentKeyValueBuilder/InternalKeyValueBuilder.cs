using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentKeyValueBuilder
{
    public class InternalKeyValueBuilder<T> : IInternalKeyValueBuilderDescriptor<T>
    {
        public InternalKeyValueBuilder()
        {
            KeyValuePairs = new List<KeyValuePair<string, object>>();
        }

        public IList<Tuple<string, string>> Properties { get; set; }

        private IList<KeyValuePair<string, object>> KeyValuePairs { get; set; }

        public IInternalKeyValueBuilderDescriptor<T> AddItem(Expression<Func<T, object>> property, string value)
        {
            MemberExpression memberExpression = null;

            var unaryExpression = property.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                var operand = unaryExpression.Operand as MemberExpression;
                if (operand != null)
                {
                    memberExpression = operand;
                }
            }
            else
            {
                var body = property.Body as MemberExpression;
                if (body != null)
                {
                    memberExpression = body;
                }
            }

            if (memberExpression == null) return this;

            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null) return this;

            var propertyName =
                Properties
                    .Where(t => t.Item1 == propertyInfo.Name || t.Item2 == propertyInfo.Name)
                    .Select(arg => arg.Item2 ?? arg.Item1)
                    .FirstOrDefault();

            KeyValuePairs.Add(new KeyValuePair<string, object>(propertyName, value));

            return this;
        }

        public IList<KeyValuePair<string, object>> ToList()
        {
            return KeyValuePairs;
        }
    }
}