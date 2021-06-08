using Autofac;

namespace voicemodchat.core.repositories.dependencies
{
    public static class DependenciesManager
    {
        public static IContainer Container { get; set; }        
        
        static ContainerBuilder _builder;

        static DependenciesManager()
        {
            _builder = new ContainerBuilder();
        }
        
        public static void RegisterType<TClass, TInterface>() where TClass : class, TInterface where TInterface : class
        {
            _builder.RegisterType<TClass>().As<TInterface>().SingleInstance();            
        }

        public static void RegisterTypeWithParam<TClass, TInterface>(string paramName, string value) where TClass : class, TInterface where TInterface : class
        {
            _builder.RegisterType<TClass>().As<TInterface>().SingleInstance().WithParameter(paramName, value);
        }

        public static void LoadDependencies() {            
            Container = _builder.Build();            
        }

        public static TInterface GetDependency<TInterface>() where TInterface: class
        {
            using(var scope = Container.BeginLifetimeScope())
            {
                return scope.Resolve<TInterface>();
            }
        }
    }
}