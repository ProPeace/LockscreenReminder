using System;
using Autofac;
using LockscreenReminder.Services.Database;

namespace LockscreenReminder.Helpers
{
    public class Locator
    {
		private IContainer _container;
		private ContainerBuilder _containerBuilder;

		public static Locator Instance { get; } = new Locator();

		/// <summary>
		/// Initialise une nouvelle instance de <see cref="Locator"/>.
		/// </summary>
		public Locator()
		{
			_containerBuilder = new ContainerBuilder();

			_containerBuilder.RegisterType<LocalDbService>().As<ILocalDbService>().SingleInstance();
			//_containerBuilder.RegisterType<TimeSynchroService>().As<ITimeSynchroService>().SingleInstance();

			//_containerBuilder.RegisterType<ExtendedSplashViewModel>();
			//_containerBuilder.RegisterType<AboutViewModel>(); ;
		}

		public T Resolve<T>()
		{
			return _container.Resolve<T>();
		}

		public object Resolve(Type type)
		{
			return _container.Resolve(type);
		}

		public void Register<TInterface, TImplementation>() where TImplementation : TInterface
		{
			var newBuilder = new ContainerBuilder();
			newBuilder.RegisterType<TImplementation>().As<TInterface>();
			newBuilder.Update(_container);
		}

		public void RegisterSingleton<TInterface, TImplementation>() where TImplementation : TInterface
		{
			var newBuilder = new ContainerBuilder();
			newBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance();
			newBuilder.Update(_container);
		}

		public void Register<T>() where T : class
		{
			_containerBuilder.RegisterType<T>();
		}

		public void RegisterInstance<T>(T instance) where T : class
		{
			if (_containerBuilder != null && instance != null)
			{
				var newBuilder = new ContainerBuilder();
				newBuilder.RegisterInstance(instance).As<T>();
				newBuilder.Update(_container);
			}
		}

		public void Build()
		{
			_container = _containerBuilder.Build();
		}
    }
}
