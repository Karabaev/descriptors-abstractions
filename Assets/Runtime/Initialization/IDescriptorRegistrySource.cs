using System;
using System.Collections.Generic;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public interface IDescriptorRegistrySource
  {
    Type DescriptorType { get; }
    
    IReadOnlyDictionary<object, IDescriptor> Descriptors { get; }
  }
}