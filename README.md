# get-paket

Experiment with downloading files via [F#](https://fsharp.org/) as well as experimenting with consuming applications using [.NET Core Global Tools](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools).

**Under construction.**
:construction:

## Initial Approach

Utilizes [FSharp.Data](https://fsharp.github.io/FSharp.Data/library/Http.html) for both the HTTP request as well as parsing returned JSON.

### Build, Package, Install, and Run

#### Clone

```
git clone https://github.com/curtisalexander/get-paket.git
```

#### Build

```
dotnet build
```

with the following software versions utilized

```
macOS version:
10.14.6

dotnet core version:
3.0.100

Package versions:
Project 'get-paket' has the following package references
   [netcoreapp3.0]:
   Top-level Package      Requested   Resolved
   > FSharp.Core          4.7         4.7.0
   > FSharp.Data          3.1.1       3.1.1
```

#### Package

Update `fsproj` file by adding the following.


```
<PackAsTool>true</PackAsTool>
<ToolCommandName>get-paket</ToolCommandName>
<PackageOutputPath>./nupkg</PackageOutputPath>
<Version>0.0.1</Version>
```

Next, create a NuGet package.

```
dotnet pack
```

#### Install

Install as a global tool.

```
dotnet tool install nupkg/get-paket.0.0.1.nupkg -g
```

#### Run

```
cd root-directory && get-paket
```

## Other Approaches

- Research using [HttpClient](https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient?view=netcore-3.0) in lieu of [HttpWebRequest](https://docs.microsoft.com/en-us/dotnet/api/system.net.httpwebrequest?&view=netcore-3.0).

  - Will be a good opportunity to continue practicing using the [BCL](https://docs.microsoft.com/en-us/dotnet/api/index) within `F#`. In addition, will be an opportunity to practice with [Async](https://docs.microsoft.com/en-us/dotnet/fsharp/tutorials/asynchronous-and-concurrent-programming/async) programming.

- Try to download the file via [streaming](https://github.com/curtisalexander/get-paket/blob/master/Program.fs#L87).

- Try to download the file with [async](https://github.com/curtisalexander/get-paket/blob/master/Program.fs#L88).

- Use a [JSON type provider](https://fsharp.github.io/FSharp.Data/library/JsonProvider.html).
    - Save and utilize local copy to get types for compilation and building
    - During runtime, hit the actual repo

## A Better Way

A better way to manage [may be](https://gist.github.com/baronfel/80f49ecb93ebacb84ca840fca7a12fc2) to manage as a global/local tool.

#### Example Workflow to Use as a Local Tool

Assumes have [.NET Core](https://dotnet.microsoft.com/download) installed.

Create directory

```
mkdir newapp
```

Create a local tool manifest.  This creates the file `.config/dotnet-tools.json`.

```
dotnet new tool-manifest
```

Install paket by running the following.  Note that it adds the latest version of paket to the `.config/dotnet-tools.json` file.

```
dotnet tool install paket
```

Now continue with a typical `paket` workflow.  Note, however, that there is no longer a `.paket` sub-directory within your application directory.

Commands are now issued with `dotnet paket` rather than `paket.exe` or `mono .paket/paket.exe`.

```
dotnet paket init
```

Add a package.

```
dotnet paket add FSharp.Data
```

Install all packages.

```
dotnet paket install
```

Etc, etc, etc...

