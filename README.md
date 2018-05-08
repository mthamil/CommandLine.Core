# CommandLine.Core
An ASP.NET Core-inspired framework for command line applications.

[![Build status](https://ci.appveyor.com/api/projects/status/wwddaako7cxv7wyj/branch/master?svg=true)](https://ci.appveyor.com/project/mthamil/commandline-core/branch/master)

Getting Started
===============
To get started, install the package CommandLine.Core.Hosting. If you want to integrate with the [McMaster.Extensions.CommandLineUtils](https://github.com/natemcmaster/CommandLineUtils) framework, also install CommandLine.Core.CommandLineUtils.

Packages
========
[![NuGet](https://img.shields.io/nuget/v/CommandLine.Core.Hosting.svg)](https://www.nuget.org/packages/CommandLine.Core.Hosting/) 
**CommandLine.Core.Hosting**: The main library, an ASP.NET Core-inspired framework for creating command line applications.

[![NuGet](https://img.shields.io/nuget/v/CommandLine.Core.Hosting.Abstractions.svg)](https://www.nuget.org/packages/CommandLine.Core.Hosting.Abstractions/) 
**CommandLine.Core.Hosting.Abstractions**: Provides hosting and startup abstractions and does not need to be referenced directly.

[![NuGet](https://img.shields.io/nuget/v/CommandLine.Core.CommandLineUtils.svg)](https://www.nuget.org/packages/CommandLine.Core.CommandLineUtils/) 
**CommandLine.Core.CommandLineUtils**: Provides integration with McMaster.Extensions.CommandLineUtils for CommandLine.Core.

[![NuGet](https://img.shields.io/nuget/v/CommandLineUtils.Extensions.svg)](https://www.nuget.org/packages/CommandLineUtils.Extensions/)
**CommandLineUtils.Extensions**: A dependency of CommandLine.Core.CommandLineUtils, this package can also be used independently of CommandLine.Core.
It provides additional builders, conventions, and more for use with McMaster.Extensions.CommandLineUtils.