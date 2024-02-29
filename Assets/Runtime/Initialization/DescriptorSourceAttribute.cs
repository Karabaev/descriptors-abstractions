using System;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
  public class DescriptorSourceAttribute : Attribute
  {
    public string Key { get; }

    public DescriptorSourceAttribute(string key) => Key = key;
  }
}