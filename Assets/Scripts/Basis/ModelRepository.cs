using System;
using System.Collections;
using System.Collections.Generic;

namespace Basis
{
    internal class ModelRepository
    {
        private readonly List<IModel> _models = new List<IModel>();

        public TModel Register<TModel>(TModel model) where TModel : IModel
        {
            _models.Add(model);
            return model;
        }
		
        public TModel Register<TModel>() where TModel : IModel
        {
            return Register(Activator.CreateInstance<TModel>());
        }
        
        public TModel Resolve<TModel>() where TModel : IModel
        {
            IModel model = GetModelByType<TModel>();
            TModel typedModel = (TModel) model;
            return typedModel;
        }
        
        public TModel GetModel<TModel>() where TModel : IModel
        {
            TModel model =  Resolve<TModel>();
            
            if (model == null)
            {
                model = Register<TModel>();
            }
			
            return model;
        }

        private IModel GetModelByType<TModel>()
        {
            Type type = typeof(TModel);

            return _models.Find(model => IsModelOfType(model, type));
        }

        private static bool IsModelOfType(IModel model, Type modelType)
        {
            IList interfaces = model.GetType().GetInterfaces();
            bool a = interfaces.Contains(modelType);
            
            bool b = model.GetType() == modelType;

            return a || b;
        }
    }
}