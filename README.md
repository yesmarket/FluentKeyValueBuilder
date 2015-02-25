# FluentKeyValueBuilder
Fluent key-value builder is a utility that makes it easy to create a list of key-value pairs based on the strongly-typed property names of an interface or class. 

To elaborate consider the simple class below:

```C#
public class Client
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Project { get; set; }
}
```

With fluent key-value builder we can easily create a list of key-value pairs for Name, Email, and Age by using the library as shown below:

```C#
private readonly KeyValueBuilder _keyValueBuilder;

public Test(KeyValueBuilder keyValueBuilder)
{
    _keyValueBuilder = keyValueBuilder;
}

public IList<KeyValuePair<string, object>> GetList()
{
    return
        _keyValueBuilder.For<Client>()
            .AddItem(test => test.Name, "Ryan Bartsch")
            .AddItem(test => test.Age, 33.ToString(CultureInfo.InvariantCulture))
            .AddItem(test => test.Project, "FluentKeyValueBuilder")
            .ToList();
}
```

The call to GetList() would produce the list shown below:
```JSON
[
  {
    "Key": "Name",
    "Value": "Ryan Bartsch"
  },
  {
    "Key": "Age",
    "Value": "33"
  }
  {
    "Key": "Project",
    "Value": "FluentKeyValueBuilder"
  }
]
```

If we want to tweak the key for one of the items in the list, we can decorate the interface or class with a NiceName attribute as shown below:
```C#
public class Client
{
    [NiceName("What is my name")]
    public string Name { get; set; }
    [NiceName("How old am I")]
    public int Age { get; set; }
    [NiceName("what is the project I'm working on")]
    public string Project { get; set; }
}
```

This would produce the following list:
```JSON
[
  {
    "Key": "What is my name",
    "Value": "Ryan Bartsch"
  },
  {
    "Key": "How old am I",
    "Value": "33"
  }
  {
    "Key": "what is the project I'm working on",
    "Value": "FluentKeyValueBuilder"
  }
]
```

So there you have it. A simple library to fluently create lists based on the properties of an interface or class in a simple strongly-typed way.

This library has been published to nuget.
