namespace DistributionCenter.Application.Contexts.Concretes;

using DistributionCenter.Application.Contexts.Bases;

public class Context(IDictionary<Type, object> tables) : BaseContext(tables) { }
