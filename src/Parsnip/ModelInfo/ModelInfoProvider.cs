namespace Parsnip.ModelInfo
{
    using System;
    using System.Collections.Generic;


    public interface ModelInfoProvider
    {
        IEnumerable<PropertyModelInfo> GetAllPropertyModelInfo<T>(T model);

        PropertyModelInfo GetPropertyModelInfo<T, TProperty>(T model, Func<T, TProperty> propertyAccessor);


    }
}