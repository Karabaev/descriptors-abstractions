using System;

namespace com.karabaev.descriptors.abstractions.Runtime
{
  public interface IMutableDescriptorRegistry<in TId, in TDescriptor> where TId : IEquatable<TId>
                                                                      where TDescriptor : IDescriptor
  {
    void Add(TId id, TDescriptor descriptor);

    void Remove(TId id);

    void Replace(TId id, TDescriptor descriptor);

    void Clear();
  }
}