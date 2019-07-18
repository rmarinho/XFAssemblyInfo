# XFAssemblyInfo
Get information about Xamarin Forms version, package version, build and associated commit

Since version 4.0.0 all Xamarin.Forms assemblies inside the public nupkg provide information of the package version they reference, what commit on github the correspond and what was the build id on AzureDevOps that built the package.

You can get this information by consulting the `AssemblyFileVersion` or for more detail info the `AssemblyInformationalVersion` 

heres some example 

```
   var xamarinFormsAssembly = Assembly.Load($"Xamarin.Forms.Core");
   var fileVersion = (AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(xamarinFormsAssembly, typeof(AssemblyFileVersionAttribute), false);
   var informationalVersion = (AssemblyInformationalVersionAttribute)Attribute.GetCustomAttribute(xamarinFormsAssembly, typeof(AssemblyInformationalVersionAttribute), false);

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
```
 

## Sample code

Run the sample and visit the AboutPage



