using System;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public interface IMutableDescriptorRegistry
  {
    Type DescriptorType { get; }
    
    void Add(object id, IDescriptor descriptor);

    void Remove(object id);

    void Replace(object id, IDescriptor descriptor);

    void Clear();
  }
}