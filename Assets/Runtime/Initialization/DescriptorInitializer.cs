using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.karabaev.utilities;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public class DescriptorInitializer
  {
    private readonly ReflectionUtils.AssembliesCollection _assembliesCollection;
    private readonly IDescriptorSourceProvider _descriptorSourceProvider;
    private readonly IReadOnlyList<IMutableDescriptorRegistry> _registries;

    public async ValueTask InitializeAsync()
    {
      var descriptorSourceTypes = ReflectionUtils
       .FindAllTypesWithAttributeAndInterface<DescriptorSourceAttribute, IDescriptorRegistrySource>(_assembliesCollection)
       .ToList();

      var tasks = descriptorSourceTypes.Select(t => _descriptorSourceProvider.GetAsync(t.Item2.Key, t.Item1)).ToList();

      var sources = await CommonUtils.WhenAll(tasks);
      var sourcesDict = sources.ToDictionary(s => s.DescriptorType, s => s);
      
      if(_registries.Count != sources.Count)
        throw new InvalidOperationException("Descriptor registries count is not equal to descriptor sources count");
      
      foreach(var registry in _registries)
      {
        var source = sourcesDict[registry.DescriptorType];
        foreach(var (id, descriptor) in source.Descriptors)
          registry.Add(id, descriptor);
      }
    }

    public DescriptorInitializer(IDescriptorSourceProvider descriptorSourceProvider, IReadOnlyList<IMutableDescriptorRegistry> registries,
      ReflectionUtils.AssembliesCollection assembliesCollection)
    {
      _descriptorSourceProvider = descriptorSourceProvider;
      _registries = registries;
      _assembliesCollection = assembliesCollection;
    }
  }
}