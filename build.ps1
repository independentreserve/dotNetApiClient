param(
    [string]$packageVersion = $null,
    [string]$configuration = "Release"
)

function Find-Nuget{
    
        $sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/v3.5.0-rc1/NuGet.exe"
        $targetNugetExe = "$PSScriptRoot\packages\nuget.exe"
    
        if (!(Test-Path $targetNugetExe )){
            Write-Host "Downloading nuget to $targetNugetExe"
            Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
        }
    
        Set-Alias nuget $targetNugetExe -Scope Global
}

function Restore-Packages()
{
    nuget restore

}

function Find-MsBuild()
{
    $path = &"$PSScriptRoot\packages\vswhere.2.3.2\tools\vswhere.exe" -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
    if ($path) {
      $path = join-path $path 'MSBuild\15.0\Bin\MSBuild.exe'
      if (test-path $path) {
        Set-Alias msbuild $path -Scope Global
        Write-Host "MSBuild found at '$path'"
      }
      else{
        Write-Error "MSBuild not found"
      }
    }
}

function Build-Solution()
{
    msbuild "$PSScriptRoot\IRDotNetClientApi.sln" /p:Configuration=$configuration /verbosity:minimal
}

Find-Nuget
Restore-Packages
Find-MsBuild
Build-Solution