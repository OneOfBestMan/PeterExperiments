using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace LC.SDK.Core.Framework
{
	public static class ServiceCollectionAutoRegExtentions
	{
		public static IServiceCollection RegisterAssembly(this IServiceCollection services)
		{
			var assemblys = RuntimeHelper.GetAllAssemblies();
			foreach (var item in assemblys)
			{
				RegisterAssembly(services, item);
			}

			return services;
		}
		/// <summary>
		/// 用DI批量注入接口程序集中对应的实现类。
		/// </summary>
		/// <param name="service"></param>
		/// <param name="interfaceAssemblyName"></param>
		/// <returns></returns>
		public static IServiceCollection RegisterAssembly(IServiceCollection service, Assembly assembly)
		{
			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (assembly == null)
			{
				throw new DllNotFoundException($"the dll  not be found");
			}

			//过滤掉非接口及泛型接口
			var types = assembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

			foreach (var type in types)
			{
				var implementTypeName = type.Name.Substring(1);
				var implementType = RuntimeHelper.GetImplementType(implementTypeName, type);
				if (implementType != null)
				{
					service.AddScoped(type, implementType);
				}
			}

			//增加dto验证类的注入
			var validtorTypes = assembly.GetTypes().Where(a => a.GetTypeInfo().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>)));
			if (validtorTypes != null && validtorTypes.Count() > 0)
			{
				foreach (var type in validtorTypes)
				{
					Type validInterface = typeof(IValidator<>);
					Type constructed = validInterface.MakeGenericType(type);
					service.AddTransient(constructed, type);
				}

			}

			return service;
		}


		/// <summary>
		/// 用DI批量注入接口程序集中对应的实现类。
		/// </summary>
		/// <param name="service"></param>
		/// <param name="interfaceAssemblyName">接口程序集的名称（不包含文件扩展名）</param>
		/// <param name="implementAssemblyName">实现程序集的名称（不包含文件扩展名）</param>
		/// <returns></returns>
		public static IServiceCollection RegisterAssembly(IServiceCollection service, string interfaceAssemblyName, string implementAssemblyName)
		{
			if (service == null)
				throw new ArgumentNullException(nameof(service));
			if (string.IsNullOrEmpty(interfaceAssemblyName))
				throw new ArgumentNullException(nameof(interfaceAssemblyName));
			if (string.IsNullOrEmpty(implementAssemblyName))
				throw new ArgumentNullException(nameof(implementAssemblyName));

			var interfaceAssembly = RuntimeHelper.GetAssembly(interfaceAssemblyName);
			if (interfaceAssembly == null)
			{
				throw new DllNotFoundException($"the dll \"{interfaceAssemblyName}\" not be found");
			}

			var implementAssembly = RuntimeHelper.GetAssembly(implementAssemblyName);
			if (implementAssembly == null)
			{
				throw new DllNotFoundException($"the dll \"{implementAssemblyName}\" not be found");
			}

			//过滤掉非接口及泛型接口
			var types = interfaceAssembly.GetTypes().Where(t => t.GetTypeInfo().IsInterface && !t.GetTypeInfo().IsGenericType);

			foreach (var type in types)
			{
				//过滤掉抽象类、泛型类以及非class
				var implementType = implementAssembly.DefinedTypes
					.FirstOrDefault(t => t.IsClass && !t.IsAbstract && !t.IsGenericType &&
										 t.GetInterfaces().Any(b => b.Name == type.Name));
				if (implementType != null)
				{
					service.AddScoped(type, implementType.AsType());
				}
			}

			return service;
		}

	}

	class RuntimeHelper
	{
		public static List<Assembly> AllAssembly = new List<Assembly>();
		public static List<Type> AllTypes = new List<Type>();

		/// <summary>
		/// 获取项目程序集 
		/// </summary>
		/// <returns></returns>
		public static IList<Assembly> GetAllAssemblies()
		{
			if (AllAssembly != null && AllAssembly.Count > 0)
			{
				return AllAssembly;
			}
			else
			{
				var list = new List<Assembly>();
				var deps = DependencyContext.Default;
				var libs = deps.CompileLibraries.Where(lib => lib.Name.Contains("LC"));
				foreach (var lib in libs)
				{
					try
					{
						var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
						list.Add(assembly);
					}
					catch (Exception)
					{
						// ignored
					}
				}
				AllAssembly = list;
				return list;
			}
		}

		public static Assembly GetAssembly(string assemblyName)
		{
			return GetAllAssemblies().FirstOrDefault(assembly => assembly.FullName.Contains(assemblyName));
		}

		public static IList<Type> GetAllTypes()
		{
			if (AllTypes != null && AllTypes.Count > 0)
			{
				return AllTypes;
			}
			var list = new List<Type>();
			foreach (var assembly in GetAllAssemblies())
			{
				var typeInfos = assembly.DefinedTypes;
				foreach (var typeInfo in typeInfos)
				{
					list.Add(typeInfo.AsType());
				}
			}
			AllTypes = list;
			return list;
		}

		public static IList<Type> GetTypesByAssembly(string assemblyName)
		{
			var list = new List<Type>();
			var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
			var typeInfos = assembly.DefinedTypes;
			foreach (var typeInfo in typeInfos)
			{
				list.Add(typeInfo.AsType());
			}
			return list;
		}

		public static Type GetImplementType(string typeName, Type baseInterfaceType)
		{
			return GetAllTypes().FirstOrDefault(t =>
			{
				if (t.Name == typeName &&
					t.GetTypeInfo().GetInterfaces().Any(b => b.Name == baseInterfaceType.Name))
				{
					var typeInfo = t.GetTypeInfo();
					return typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType;
				}
				return false;
			});
		}
	}
}
