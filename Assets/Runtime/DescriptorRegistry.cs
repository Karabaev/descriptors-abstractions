using System;
using System.Collections.Generic;
using System.Linq;
using com.karabaev.descriptors.abstractions.Initialization;

namespace com.karabaev.descriptors.abstractions
{
  public abstract class DescriptorRegistry<TId, TDescriptor>
    : IDescriptorRegistry<TId, TDescriptor>, IMutableDescriptorRegistry
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

    public TDescriptor RequireSingle() => _items.Values.First();

    Type IMutableDescriptorRegistry.DescriptorType => typeof(TDescriptor);

    void IMutableDescriptorRegistry.Add(object id, IDescriptor descriptor) => _items.Add((TId)id, (TDescriptor)descriptor);

    void IMutableDescriptorRegistry.Remove(object id) => _items.Remove((TId)id);

    void IMutableDescriptorRegistry.Replace(object id, IDescriptor descriptor) => _items[(TId)id] = (TDescriptor)descriptor;

    void IMutableDescriptorRegistry.Clear() => _items.Clear();
  }
}