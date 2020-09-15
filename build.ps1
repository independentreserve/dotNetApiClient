param(
    [string]$packageVersion = $null,
    [string]$configuration = "Release"
)

function Find-Nuget{
    
        $sourceNugetExe = "https://dist.nuget.org/win-x86-commandline/v4.9.4/nuget.exe"
        $targetNugetExe = "$PSScriptRoot\packages\nuget.exe"
        $packageFolder = "$PSScriptRoot\packages"
                
        New-Item -ItemType directory -Path $packageFolder -Force  | Out-Null
    
        if (!(Test-Path $targetNugetExe )){
            Write-Host "Downloading nuget to $targetNugetExe"
            Invoke-WebRequest $sourceNugetExe -OutFile $targetNugetExe
        }
    
        Set-Alias nuget $targetNugetExe -Scope Global
        
        return $packageFolder
}

function Restore-Packages()
{
    nuget restore

}

function Find-MsBuild()
{
    $package = "vsWhere"
    $version = "2.6.7"
    $packageFolder = Find-Nuget $package $version
    $vsWherePath = "$packageFolder\$package.$version\tools\vswhere.exe"
   
    $path = &$vsWherePath -latest -products * -requires Microsoft.Component.MSBuild -property installationPath
    $msbuildFound = $false
    
    if ($path) {
      $testPath = join-path $path 'MSBuild\15.0\Bin\msbuild.exe'
      if (test-path $testPath) {
        $msbuildFound = $true 
        $path = $testPath
        
        Write-Host "MSBuild: Visual Studio 2017"
      }

      if (-not $msbuildFound)
      {
            $testPath = join-path $path 'MSBuild\current\Bin\msbuild.exe'

            if (test-path $testPath) {
                Write-Host "MSBuild: Visual Studio 2019"
                $path = $testPath
                $msbuildFound = $true 
            }
      }
    }
    
    if (-not $msbuildFound){
        $path = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"
        Write-Host "Using legacy MSBuild. This won't work! Fix vswhere." -ForegroundColor "Red"
        _WriteConfig "MSBuild" "Visual Studio 2015"
    }
    
    Set-Alias msbuild $path -Scope Global
}

function Build-Solution()
{
    msbuild "$PSScriptRoot\IRDotNetClientApi.sln" /p:Configuration=$configuration /verbosity:minimal
}


function Pack-Nuget{
    Write-Host "Nuget pack"

    $outputFolder = "$PSScriptRoot\_artifacts"

    if(-not $packageVersion){
        $packageVersion = "1.0.0.0"
    }

    dotnet pack "$PSScriptRoot\src\DotNetClientApi\DotNetClientApi.csproj" --include-symbols -o $outputFolder -c $configuration /p:PackageVersion=$packageVersion
}


function Run-Tests{

	if ($env:IR_TESTS_SKIP -eq $true) 
    {
      Write-Host "Tests execution disabled via env:IR_TESTS_SKIP flag"
	  return
    }

    # ensure folder is clean and exists
    $artifactDir = "$PSScriptRoot\_artifacts"
    Get-ChildItem -Path $artifactDir -Recurse | Remove-Item -force -recurse
    New-Item -Force -ItemType directory -Path $artifactDir | Out-Null

    if (-not (Test-Path env:IR_DOTNETCLIENTAPI_TEST_CONFIG))
    {
        Write-Warning "Tests not executed, missing api config"
        return
    }

    Write-Host "Execute Tests"


    $testResultformat = ""
    $nunitConsole = "$PSScriptRoot\packages\NUnit.ConsoleRunner.3.10.0\tools\nunit3-console.exe"
      
    $assembliesToTest = @(
        "$PSScriptRoot\test\UnitTest\bin\$configuration\UnitTest.dll"
    )
    
    $excludeToUse = '"cat!=Brittle"'
    $whereArg ="--where=$excludeToUse"
    $resultArg = "--result=$artifactDir\UnitTest.xml$testResultformat"

    Write-Host "$resultArg $whereArg" 
    & $nunitConsole $assembliesToTest $resultArg $whereArg

    if ($lastExitCode -ne 0)
    {
        Write-Host "##teamcity[buildProblem description='unit test failure']"
        exit $lastExitCode
    }
}


#Find-Nuget
#Restore-Packages
#Find-MsBuild
#Build-Solution
Run-Tests
#Pack-Nuget