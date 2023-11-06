using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Runtime.Loader;

namespace Repair.Core.Assemblys
{
    public static class AssemblyExtension
    {

        public static List<Assembly> _allAssemblies;

        /// <summary> 
        /// 根据名称获取程序集
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        public static Assembly GetAssemblyByName(string assemblyName)
        {
            return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
        }
        /// <summary>
        /// 获取当前项目所有的程序集
        /// </summary>
        public static List<Assembly> AllAssemblies
        {
            get
            {
                if (_allAssemblies != null) return _allAssemblies;
                var list = new List<Assembly>();
                var deps = DependencyContext.Default;
                var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package" && !lib.Name.Contains("Microsoft"));
                foreach (var lib in libs)
                {
                    list.Add(GetAssemblyByName(lib.Name));
                }
                _allAssemblies = list;
                return list;
            }
        }

        public static bool IsNullableType(this Type theType)
        {
            return theType.IsGenericType && theType.
            GetGenericTypeDefinition().Equals
            (typeof(Nullable<>));
        }
    }
}
