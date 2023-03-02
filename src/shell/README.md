# Neo Shell (Preview)

NEO Shell is a cross-platform and extensible, unified command-line interface for managing NEO chain resources on worknet and testnet. It enables users to execute commands through a terminal using interactive command-line prompts or scripts.

With NEO Shell, users can perform various tasks such as transferring funds, deploying contracts, invoking contracts, querying blocks, transactions, addresses and more. NEO Shell support for custom commands and extensions are planned.

## Usage
`neo COMMAND|EXTENSION [sub-commands] [—Global flags]`

## Requirements

[version 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) 
of [the .NET developer platform](https://dot.net) to be installed. 

> Note: worknet is also required.

## Installation (Preview)

The Neo Blockchain Toolkit has a public
[build server](https://dev.azure.com/ngdenterprise/Build/_build) and
[package feed](https://dev.azure.com/ngdenterprise/Build/_artifacts).
The public package feed contains unreleased builds of Neo-Shell.

You can install preview builds of Neo-Shell by using the `--add-source`
option to specify the Neo Blockchain Toolkit package feed.
For example, to update to the latest release branch version of Neo-Express, you would run this command:

``` shell
> dotnet tool update Neo.Shell -g --add-source https://pkgs.dev.azure.com/ngdenterprise/Build/_packaging/public/nuget/v3/index.json
```

You can also install master branch releases of these tools by using the `--version`
and/or `--prerelease` command line options. For more details, please see the
[official dotnet tool documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools#install-a-specific-tool-version).

If you regularly use unreleased versions of these tools in a given project,
you can specify the Neo Blockchain Toolkit package feed in a 
[NuGet.config file](https://docs.microsoft.com/en-us/nuget/consume-packages/configuring-nuget-behavior#changing-config-settings).
Several Neo sample projects like 
[NeoContributorToken](https://github.com/ngdenterprise/neo-contrib-token)
use a NuGet.config file.