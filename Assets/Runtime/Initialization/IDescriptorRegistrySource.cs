using System;
using System.Collections.Generic;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public interface IDescriptorRegistrySource
  {
    string Key { get; }
    
    Type DescriptorType { get; }
    
    Type ProviderType { get; }
    
    IReadOnlyDictionary<object, IDescriptor> Descriptors { get; }
  }
}