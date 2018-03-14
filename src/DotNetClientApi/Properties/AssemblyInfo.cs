using System.Resources;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("DotNetClientApi")]
[assembly: AssemblyDescription("Independent Reserve .NET Client")]
[assembly: AssemblyCompany("Independent Reserve")]
[assembly: AssemblyProduct("IrClientApi")]
[assembly: AssemblyCopyright("Copyright ©  2014")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("dev build")]

[assembly: InternalsVisibleTo("UnitTest")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
    [assembly: AssemblyConfiguration("Release")]
#endif

