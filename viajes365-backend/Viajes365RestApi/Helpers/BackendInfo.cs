using System.Reflection;
using System.Runtime.Versioning;
using Viajes365RestApi.Properties;

namespace Viajes365RestApi
{
    public class BackendInfo
    {
        public string OsPlatform { get; }
        public string framework { get; }
        public string Company { get; }
        public string Product { get; }
        public string Copyright { get; }
        public string Title { get; }
        public string Description { get; }
        public string Version { get; }
       
        public BackendInfo()
        {
            OsPlatform = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            framework = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
            Company = AssemblyInfo.Company;
            Product = AssemblyInfo.Product;
            Copyright = AssemblyInfo.Copyright;
            Title = AssemblyInfo.Title;
            Description = AssemblyInfo.Description;
            Version = AssemblyInfo.Version.ToString();            
        }
    }
}
