using System;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
  public class DescriptorSourceAttribute : Attribute
  {
    public string Key { get; }
    
    public Type ProviderType { get; }

    public DescriptorSourceAttribute(string key, Type providerType)
    {
      Key = key;
      ProviderType = providerType;
    }
  }
}