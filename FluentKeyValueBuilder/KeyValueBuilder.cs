using FluentKeyValueBuilder.Extensions;

namespace FluentKeyValueBuilder
{
	public class KeyValueBuilder : IKeyValueBuilder
	{
		public IInternalKeyValueBuilder<T> For<T>()
		{
			var internalArrangementRuleBuilder =
				new InternalKeyValueBuilder<T>
				{
                    Properties = typeof(T).GetPropertyNiceNames()
				};

			return internalArrangementRuleBuilder;
		}
	}
}