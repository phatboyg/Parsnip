namespace Parsnip.ModelInfo
{
    using System;


    public interface PropertyModelInfo<TModel, TProperty> :
        PropertyModelInfo<TProperty>
    {
    }


    public interface PropertyModelInfo<TProperty> :
        PropertyModelInfo
    {
    }


    /// <summary>
    /// The property model info related to a model
    /// </summary>
    public interface PropertyModelInfo
    {
        Type ModelType { get; }
        Type PropertyType { get; }
    }
}