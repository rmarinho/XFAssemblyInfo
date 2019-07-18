# XFAssemblyInfo
et information about Xamarin.Forms version, package version, build and associated commit:

As of Xamarin.Forms 4.0, all assemblies inside the public nupkg provide information about the package version they reference, the corresponding GitHub commit, and the AzureDevOps build ID that created the package.

We use `Version.targets` and `GitInfo` to get the git commit information, and we use `MSBuilder.GenerateAssemblyInfo` to add this metadata to non-SDK style projects.

You can retrieve this information from the `AssemblyFileVersion` or, for more detailed info, the `AssemblyInformationalVersion` metadata.

`AssemblyInformationalVersion` provides something like `{XamarinFormsVersionWithSuffix}+sha.{commit}-azdo.{buildid}`.

Some example code to extract the values:

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
![alt text](https://i.ibb.co/ZxZStVB/unnamed-2.jpg "4.3")
![alt text](https://i.ibb.co/928TYYg/unnamed-4.jpg "4.1")
![alt text](https://i.ibb.co/YN6xYRF/unnamed-3.jpg "4.0")
