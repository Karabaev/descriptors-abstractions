using System;
using Cysharp.Threading.Tasks;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public interface IDescriptorSourceProvider
  {
    UniTask<IDescriptorRegistrySource> GetAsync(string key, Type type);
  }
  
  public class DummyDescriptorSourceProvider : IDescriptorSourceProvider
  {
    public UniTask<IDescriptorRegistrySource> GetAsync(string key, Type type)
    {
      return UniTask.FromResult((IDescriptorRegistrySource)Activator.CreateInstance(type));
    }
  }
}