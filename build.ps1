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


function Pack-Nuget{
    Write-Host "Nuget pack"

    $outputFolder = "$PSScriptRoot\_artifacts"
    
    New-Item -Force -ItemType directory -Path $outputFolder | Out-Null

    if(-not $packageVersion){
        $packageVersion = "1.0.0.0"
    }

    dotnet pack "$PSScriptRoot\src\DotNetClientApi\DotNetClientApi.csproj" --include-symbols -o $outputFolder -c $configuration /p:PackageVersion=$packageVersion
}


function Run-Tests{

    if (-not (Test-Path env:IR_DOTNETCLIENTAPI_TEST_CONFIG))
    {
        Write-Warning "Tests not executed, missing api config"
        return
    }

    Write-Host "Execute Tests"


    $testResultformat = ""
    $nunitConsole = "$PSScriptRoot\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe"
      
    $assembliesToTest = @(
        "$PSScriptRoot\test\UnitTest\bin\Debug\UnitTest.dll"
    )

    $resultArg = "--result=$PSScriptRoot\_artifacts\UnitTest.xml$testResultformat"

    Write-Host "$resultArg $whereArg" 
    & $nunitConsole $assembliesToTest $resultArg

    if ($lastExitCode -ne 0)
    {
        Write-Host "##teamcity[buildProblem description='unit test failure']"
        exit $lastExitCode
    }
}


Find-Nuget
Restore-Packages
Find-MsBuild
Build-Solution
Run-Tests
Pack-Nuget