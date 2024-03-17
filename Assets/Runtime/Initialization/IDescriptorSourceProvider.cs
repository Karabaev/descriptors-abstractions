using System;
using System.Threading.Tasks;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public interface IDescriptorSourceProvider
  {
    ValueTask<IDescriptorRegistrySource> GetAsync(string key, Type type);
  }
  
  public class DummyDescriptorSourceProvider : IDescriptorSourceProvider
  {
#pragma warning disable CS1998
    public async ValueTask<IDescriptorRegistrySource> GetAsync(string key, Type type) => (IDescriptorRegistrySource)Activator.CreateInstance(type);
#pragma warning restore CS1998
  }
}