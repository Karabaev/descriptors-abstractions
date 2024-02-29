using System;
using System.Collections.Generic;

namespace com.karabaev.descriptors.abstractions.Runtime
{
  public abstract class DescriptorRegistry<TId, TDescriptor>
    : IDescriptorRegistry<TId, TDescriptor>, IMutableDescriptorRegistry<TId, TDescriptor>
    where TId : IEquatable<TId>
    where TDescriptor : IDescriptor
  {
    private readonly Dictionary<TId, TDescriptor> _items = new();

    public IEnumerable<TDescriptor> Items => _items.Values;

    public TDescriptor? Get(TId id)
    {
      _items.TryGetValue(id, out var result);
      return result;
    }

    public TDescriptor Require(TId id) => _items[id];

    public void Add(TId id, TDescriptor descriptor) => _items.Add(id, descriptor);

    public void Remove(TId id) => _items.Remove(id);

    public void Replace(TId id, TDescriptor descriptor) => _items[id] = descriptor;

    public void Clear() => _items.Clear();
  }
}