﻿
using Ninject;
using Ninject.Parameters;
using WindowsPhone.Contracts.Core;

namespace WindowsPhone.DI
{
    public class StandardContainer : IDIContainer
    {
        private IKernel _kernel;

        public StandardContainer()
        {
            _kernel = new StandardKernel(
                  new Modules.LoggingModule()
                , new Modules.DomainModelsModule()
                , new Modules.CommonModule()
                , new Modules.StorageModule()
                );

        }

        public T Get<T>(params IInjectionParameter[] parameters)
        {
            var parameterList = Container.DomainToNinjectParameters(parameters);
            if (parameterList != null) return _kernel.Get<T>(parameterList.ToArray());
            return _kernel.Get<T>();
        }
        public T Get<T>(string name, params IInjectionParameter[] parameters)
        {
            var parameterList = Container.DomainToNinjectParameters(parameters);
            if (parameterList != null) return _kernel.Get<T>(name, parameterList.ToArray());
            return _kernel.Get<T>(name);
        }
    }
}