namespace Parsnip.ModelInfo
{
    using System;


    public class PropertyModelInfoImpl<TModel, TProperty> : 
        PropertyModelInfo<TModel, TProperty>
    {
        readonly Type _modelType;
        readonly Type _propertyType;

        public PropertyModelInfoImpl()
        {
            _modelType = typeof(TModel);
            _propertyType = typeof(TProperty);
        }

        public Type ModelType
        {
            get { return _modelType; }
        }

        public Type PropertyType
        {
            get { return _propertyType; }
        }
    }
}