using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public class DescriptorInitializer
  {
    private readonly IReadOnlyList<IDescriptorSourceProvider> _descriptorSourceProviders;
    private readonly IReadOnlyList<IMutableDescriptorRegistry> _registries;
    private readonly DescriptorSourceTypes _sourceTypes;

    public async UniTask InitializeAsync()
    {
      var tasks = _sourceTypes.SourceTypes.Select(sourceType =>
      {
        var attribute = sourceType.GetCustomAttribute<DescriptorSourceAttribute>();
        if(attribute == null)
        {
          throw new NullReferenceException(
            $"Could not find {nameof(DescriptorSourceAttribute)} attribute on descriptor source type. DescriptorSourceType={sourceType.Name}");
        }
        var key = attribute.Key;
        var providerType = attribute.ProviderType;
        var provider = _descriptorSourceProviders.FirstOrDefault(p => p.GetType() == providerType);
        if(provider == null)
        {
          throw new NullReferenceException(
            $"Could not find descriptor source provider for source. RequiredProvider={providerType.Name}, SourceType={sourceType.Name}");
        }

        return provider.GetAsync(key, sourceType);
      }).ToList();

      
      var sources = await UniTask.WhenAll(tasks);
      var sourcesDict = sources.ToDictionary(s => s.DescriptorType, s => s);
      
      if(_registries.Count != sources.Length)
        throw new InvalidOperationException("Descriptor registries count is not equal to descriptor sources count");
      
      foreach(var registry in _registries)
      {
        var source = sourcesDict[registry.DescriptorType];
        foreach(var (id, descriptor) in source.Descriptors)
          registry.Add(id, descriptor);
      }
    }

    public DescriptorInitializer(IReadOnlyList<IDescriptorSourceProvider> descriptorSourceProviders, IReadOnlyList<IMutableDescriptorRegistry> registries,
      DescriptorSourceTypes sourceTypes)
    {
      _descriptorSourceProviders = descriptorSourceProviders;
      _registries = registries;
      _sourceTypes = sourceTypes;
    }
  }
}