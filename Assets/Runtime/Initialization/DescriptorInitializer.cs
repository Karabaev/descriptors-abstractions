using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.karabaev.utilities;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public class DescriptorInitializer
  {
    private readonly IReadOnlyList<IDescriptorSourceProvider> _descriptorSourceProviders;
    private readonly IReadOnlyList<IMutableDescriptorRegistry> _registries;
    private readonly IReadOnlyList<IDescriptorRegistrySource> _sources;

    public async ValueTask InitializeAsync()
    {
      var tasks = _sources.Select(source =>
      {
        var provider = _descriptorSourceProviders.FirstOrDefault(p => p.GetType() == source.ProviderType);
        if(provider == null)
        {
          throw new NullReferenceException(
            $"Could not find descriptor source provider for source. RequiredProvider={source.ProviderType.Name}, SourceType={source.GetType().Namespace}");
        }

        return provider.GetAsync(source.Key, source.DescriptorType);
      }).ToList();

      
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

    public DescriptorInitializer(IReadOnlyList<IDescriptorSourceProvider> descriptorSourceProviders, IReadOnlyList<IMutableDescriptorRegistry> registries,
      IReadOnlyList<IDescriptorRegistrySource> sources)
    {
      _descriptorSourceProviders = descriptorSourceProviders;
      _registries = registries;
      _sources = sources;
    }
  }
}