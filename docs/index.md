# Setting up a Selenium Grid

## Prerequisites

- Selenium-Server
    - 2vCPU
    - 4GB Memory
    - 40GB HDD
    - Windows Server 2016 Standard (Desktop Experience) or Windows Server 2016 Datacenter (Desktop Experience)
    - Windows Management Framework 5.1

- Selenium-Node-Windows-10-Pro 
    - 2vCPU
    - 4GB Memory
    - 40GB HDD
    - Windows 10 Pro
    - Windows Management Framework 5.1

- Selenium-Node-Windows-7-Pro 
    - 2vCPU
    - 4GB Memory
    - 40GB HDD
    - Windows 7
    - Windows Management Framework 5.1

- Selenium-Node-Windows-8.1-Pro 
    - 2vCPU
    - 4GB Memory
    - 40GB HDD
    - Windows 8.1
    - Windows Management Framework 5.1

It is expected that all hosts are patched to the latest version.

## Provisioning

In order to create a Selenium Grid, we'll need to provision some hosts. We could use the same host for both the hub and the nodes, but this doesn't allow you to certify that your application works against browsers on different versions of Windows.

### Installing Windows Server 2016

1. Install the operating system as usuall
2. If needed, install VMware Tools (if running on VMware), or software needed for the operating system to work
3. (Optional) Install endpoint protection (e.g. Sophos Endpoint)
4. (Optional) Enable Remote Desktop to ease remote management
5. (Optional) Join the computer to a domain, if that is required by your organization.
6. Activate the operating system
7. Rename computer to something sensible, for example SEH01SFO01 (Selenium Hub 01 at Site 01 close to San Francisco Airport)
8. Make sure Windows Updates are installed.

Additional Tasks:

1. Create a snapshot "baseline" of the virtual machine
2. Run sysprep 
3. Create another snapshot
4. Create a template from the snapshot
5. Revert to the "baseline" snaphot

If needed I can create a new clone of the Windows Server 2016 and use it as a Selenium Node.

### Installing Windows 10 Pro (64 bit)

1. Install the operating system as usuall
2. If needed, install VMware Tools (if running on VMware), or software needed for the operating system to work
3. (Optional) Install endpoint protection (e.g. Sophos Endpoint)
4. (Optional) Enable Remote Desktop to ease remote management
5. (Optional) Join the computer to a domain, if that is required by your organization.
6. Activate the operating system
7. Rename computer to something sensible, for example SEN01SFO01 (Selenium Node 01 at Site 01 close to San Francisco Airport)
8. Make sure Windows Updates are installed

Additional Tasks:

1. Create a snapshot "baseline" of the virtual machine
2. Run sysprep 
3. Create another snapshot
4. Create a template from the snapshot
5. Revert to the "baseline" snaphot

### Installing Windows 8.1 Pro (64 bit)

1. Install the operating system as usuall
2. If needed, install VMware Tools (if running on VMware), or software needed for the operating system to work
3. (Optional) Install endpoint protection (e.g. Sophos Endpoint)
4. (Optional) Enable Remote Desktop to ease remote management
5. (Optional) Join the computer to a domain, if that is required by your organization.
6. Activate the operating system
7. Rename computer to something sensible, for example SEN02SFO01 (Selenium Node 02 at Site 01 close to San Francisco Airport)
8. Make sure Windows Updates are installed
9. Install [WMF 5.1](https://go.microsoft.com/fwlink/?linkid=839516) (Windows Management Framework). See [documentation](https://docs.microsoft.com/en-us/powershell/wmf/5.1/install-configure).

Additional Tasks:

1. Create a snapshot "baseline" of the virtual machine
2. Run sysprep 
3. Create another snapshot
4. Create a template from the snapshot
5. Revert to the "baseline" snaphot

### Installing Windows 7 Pro (64 bit)

1. Install the operating system as usuall
2. If needed, install VMware Tools (if running on VMware), or software needed for the operating system to work
3. (Optional) Install endpoint protection (e.g. Sophos Endpoint)
4. (Optional) Enable Remote Desktop to ease remote management
5. (Optional) Join the computer to a domain, if that is required by your organization.
6. Activate the operating system
7. Rename computer to something sensible, for example SEN03SFO01 (Selenium Node 03 at Site 01 close to San Francisco Airport)
8. Make sure Windows Updates are installed
9. Install [WMF 5.1](https://go.microsoft.com/fwlink/?linkid=839523) (Windows Management Framework). See [documentation](https://docs.microsoft.com/en-us/powershell/wmf/5.1/install-configure).

Additional Tasks:

1. Create a snapshot "baseline" of the virtual machine
2. Run sysprep 
3. Create another snapshot
4. Create a template from the snapshot
5. Revert to the "baseline" snaphot

### Configure DNS Server

You may want to make sure that you register an easy to remember name, `selenium.example.net` that points to your Selenium Hub (SEH01SFO01). The primary reason for this is that clients and nodes will connect to this machine and should you choose to reprovision it and the IP address change, you'll need to make changes to a million different places.

## Deployment

We will not always be reprovisioning machines to deploy the grid. 

### Selenium Hub

This assumes that you'll be installing  