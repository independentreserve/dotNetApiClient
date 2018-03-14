using System.Resources;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Independent Reserve API Client")]
[assembly: AssemblyDescription(".NET client for interacting with the Independent Reserve API")]
[assembly: AssemblyCompany("Independent Reserve")]
[assembly: AssemblyProduct("API Client")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("dev build")]
[assembly: InternalsVisibleTo("UnitTest")]

#if DEBUG
    [assembly: AssemblyConfiguration("Debug")]
#else
    [assembly: AssemblyConfiguration("Release")]
#endif

