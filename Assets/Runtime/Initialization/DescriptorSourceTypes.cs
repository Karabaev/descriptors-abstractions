using System;
using System.Collections.Generic;

namespace com.karabaev.descriptors.abstractions.Initialization
{
  public class DescriptorSourceTypes
  {
    public IReadOnlyList<Type> SourceTypes { get; }

    public DescriptorSourceTypes(IReadOnlyList<Type> sourceTypes) => SourceTypes = sourceTypes;
  }
}