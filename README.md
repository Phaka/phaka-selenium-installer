# Phaka Selenium Installer

This repository contains the source to create Windows Installer packages for a Selenium Grid.  

## Getting Started

This project requires a familiarity with Selenium and a Selenium Grid. This project produces two x86 installers

* selenium-hub.msi
* selenium-node.msi

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

#### Selenium Hub 

* Download the [latest release](https://github.com/Phaka/phaka-selenium-installer/releases) of `selenium-hub.msi`.
* You may want to unlock the msi.
* Run the installer

Once installed, you can view that the Selenium hub is up and running by visiting http://localhost:4444/grid/console. The installer also opened the firewalls to allow traffic to port 4444, so you could access it remotely as http://172.16.247.141/grid/console, if 172.16.247.141 was the IP address of the selenium hub.  Refresh this page after installing each node and confirm that the node registered itself with the Selenium Hub.  It may take some time.

The installer has the following properties:

|Name|Default Value|Description|
|---|---|---|
|`SELENIUM_SERVICE_USERNAME`|`Phaka Selenium Hub`|The username under which the windows service will run|
|`SELENIUM_SERVICE_PASSWORD`|`Ph@k@-S3l3nium-Hub`|The password of the user under which the windows service will run|
|`SELENIUM_PORT`|`4444`|The port that the Selenium hub will listen to|

The installer can be executed from command line as follows. 

```
msiexec /i selenium-hub.msi
```


#### Selenium Node

For each node, you'll need the following applies:

* Download the [latest release](https://github.com/Phaka/phaka-selenium-installer/releases) of `selenium-node.msi`.
* You may want to unlock the msi.
* Run the installer

Once installed, you can view that the Selenium node is up and running by visiting http://localhost:5555/wd/hub/static/resource/hub.html. The installer also opened the firewalls to allow traffic to port 5555, so you could access it remotely as http://172.16.247.142:5555/wd/hub/static/resource/hub.html, if 172.16.247.142 was the IP address of the Selenium node. It takes a few seconds for the Selenium node to register itself with a the hub.

The installer has the following properties:

|Name|Default Value|Description|
|---|---|---|
|`SELENIUM_SERVICE_USERNAME`|`Phaka Selenium Node`|The username under which the windows service will run|
|`SELENIUM_SERVICE_PASSWORD`|`Ph@k@-S3l3nium-Node`|The password of the user under which the windows service will run|
|`SELENIUM_PORT`|`5555`|The port that the Selenium node will listen to|
|`SELENIUM_HUB_BASEURL`|`http://localhost:4444/`|The URL of the Selenium Hub. The default value assumes that the hub and node is installed on the same host|

You could also run the installer from the command line:

```
msiexec /i selenium-node.msi
```

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

