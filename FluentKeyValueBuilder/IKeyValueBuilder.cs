namespace FluentKeyValueBuilder
{
	public interface IKeyValueBuilder
	{
		IInternalKeyValueBuilder<T> For<T>();
	}
}