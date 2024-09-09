namespace DistributionCenter.Application.Contexts.Concretes;

using System;
using System.Collections.Generic;
using DistributionCenter.Application.Contexts.Bases;

public class Context(IDictionary<Type, object> tables) : BaseContext(tables) { }
