using System;
using System.Collections.Generic;
using com.karabaev.descriptors.abstractions.Initialization;

namespace com.karabaev.descriptors.abstractions.example
{
  public class ExampleDescriptor : IDescriptor
  {
    public readonly string Label;
    public readonly string Description;

    public ExampleDescriptor(string label, string description)
    {
      Label = label;
      Description = description;
    }
  }
  
  public class ExampleRegistry : DescriptorRegistry<string, ExampleDescriptor> { }
  
  public class ExampleRegistrySource : IDescriptorRegistrySource
  {
    public string Key => "Example";

    public Type DescriptorType => typeof(ExampleDescriptor);

    public Type ProviderType => typeof(DummyDescriptorSourceProvider);

    public IReadOnlyDictionary<object, IDescriptor> Descriptors { get; } = new Dictionary<object, IDescriptor>
    {
      { "firstId", new ExampleDescriptor("firstLabel", "firstDescription") },
      { "secondId", new ExampleDescriptor("secondLabel", "secondDescription") },
      { "thirdId", new ExampleDescriptor("thirdLabel", "thirdDescription") }
    };
  }
}