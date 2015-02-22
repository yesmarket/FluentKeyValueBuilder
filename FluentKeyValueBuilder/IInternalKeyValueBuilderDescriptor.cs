using System.Collections.Generic;

namespace FluentKeyValueBuilder
{
	public interface IInternalKeyValueBuilderDescriptor<T> : IInternalKeyValueBuilder<T>
	{
        IList<KeyValuePair<string, object>> ToList();
	}
}