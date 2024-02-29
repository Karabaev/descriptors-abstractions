using System;
using System.Threading.Tasks;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public interface IDescriptorSourceProvider
  {
    ValueTask<IDescriptorRegistrySource> GetAsync(string key, Type type);
  }
}