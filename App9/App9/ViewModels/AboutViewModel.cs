using System;
using System.Reflection;
using System.Windows.Input;

using Xamarin.Forms;

namespace App9.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            PackageName = "Xamarin.Forms";
            var xamarinFormsAssembly = Assembly.Load($"{PackageName}.Core");

            var fileVersion = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(xamarinFormsAssembly, typeof(AssemblyFileVersionAttribute), false);
            var informationalVersion = (AssemblyInformationalVersionAttribute)Attribute.GetCustomAttribute(xamarinFormsAssembly, typeof(AssemblyInformationalVersionAttribute), false);

            PackageInformationalVersion = informationalVersion.InformationalVersion;
            var info = informationalVersion.InformationalVersion.Split('+');

            if (info.Length > 1)
            {
                PackageVersion = info[0];
                var infos = info[1].Split('-');
                foreach (var item in infos)
                {
                    if (item.Contains("azdo."))
                    {
                        BuildId = item.Replace("azdo.", "");
                        AzdoUrl = $"https://dev.azure.com/devdiv/DevDiv/_build/results?buildId={BuildId}";
                    }
                    if (item.Contains("sha."))
                    {
                        Sha = item.Replace("sha.", "");
                        ShaUrl = $"https://github.com/xamarin/Xamarin.Forms/commits/{Sha}";
                    }
                }
            }
            OpenWebCommand = new Command<string>(p => Device.OpenUri(new Uri(p)));
        }

        public string PackageName { get; set; }

        public string PackageVersion { get; set; }

        public string PackageInformationalVersion { get; set; }

        public string BuildId { get; set; }

        public string AzdoUrl { get; set; }

        public string Sha { get; set; }

        public string ShaUrl { get; set; }


        public ICommand OpenWebCommand { get; }
    }
}