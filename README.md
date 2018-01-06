# Phaka Selenium Installer

This repository contains the source to create Windows Installer packages for a Selenium Grid.  

## Getting Started

See the [documentation](docs/index.md) for instructions.


### Prerequisites

The Selenium hub and node installers were require the following:

* Windows
    * Windows Server 2016
    * Windows Server 2012 R2
    * Windows Server 2012
    * Windows Server 2008 R2
    * Windows 7
    * Windows 8
    * Windows 8.1
    * Windows 10
* JRE or JDK
    * 1.9
    * 1.8
* administrative access to the computers

In order for the Selenium Node to function, you'll also need to install one of the following Browsers

* Firefox
* Google Chrome
* Opera
* Internet Explorer

In a typicall configuration, the hub is installed on a variant of Windows Server due to technical limits on concurrent connections, and the hubs are installed on a number of Windows client variants.  For example, this is typical:

* Selenium Hub
    * Windows Server 2016
* Selenium Node
    * Windows 7
        * Firefox
        * Chrome
        * Internet Explorer
        * Opera
    * Windows 8.1
        * Firefox
        * Chrome
        * Internet Explorer
        * Opera
    * Windows 10
        * Firefox
        * Chrome
        * Internet Explorer
        * Microsoft Edge (not installed by the installer)
        * Opera

### Installing



## Building

* [Visual Studio 2017](https://www.visualstudio.com/downloads/)
* [Windows 10](https://www.microsoft.com/en-us/software-download/windows10) 

In order to build the codebase you'll need the following

- nuget
- Visual Studio Community edition or higher
- WiX Toolset

Before you can build the packages, you'll need to restore packages.  Typically this is only done once, but you may need to do it whenever the packages folder was deleted. 

```
nuget restore SeInstaller.sln
```

In order to build the release package, you can use `msbuild`. 

```
msbuild /t:Build /p:Configuration=Release /p:Platform=x86 SeInstaller.sln
```

## Contributing

Please read [CONTRIBUTING.md](.github/CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/Phaka/phaka-selenium-installer/tags). 

## Authors

* [WernerStrydom](https://github.com/WernerStrydom) 

## License

- This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* [WinSW](https://github.com/kohsuke/winsw/)

