using System;
using System.Collections.Generic;

namespace com.karabaev.descriptors
{
  public interface IDescriptorRegistry<in TId, out TDescriptor> where TId : IEquatable<TId>
                                                                where TDescriptor : IDescriptor
  {
    IEnumerable<TDescriptor> Items { get; }

    TDescriptor? Get(TId id);
    
    TDescriptor Require(TId id);
  }
}