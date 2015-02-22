using System;
using System.Linq.Expressions;

namespace FluentKeyValueBuilder
{
	public interface IInternalKeyValueBuilder<T>
	{
		IInternalKeyValueBuilderDescriptor<T> AddItem(Expression<Func<T, object>> property, string value);
	}
}