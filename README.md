# WindowsServiceHost
Simple Windows Service hosting library. Allows to install service with *installutil.exe* into Service Control Manager (SCM) or run as a Console Application.

[![Build status](https://ci.appveyor.com/api/projects/status/6b43hnwedyp8jrlm?svg=true)](https://ci.appveyor.com/project/iiwaasnet/windowsservicehost)
[![NuGet version](https://badge.fury.io/nu/WIndowsService.Host.svg)](https://badge.fury.io/nu/WIndowsService.Host)

# Why
Although [Topshelf](https://github.com/Topshelf/Topshelf) is really nice, it doesn't support installing services with *installutil*, which might be a show-stopper sometimes...

# Requirements
**.NET 4.6.x**

Usage
----------------
In Console (or Windows) Application Project of your service solution:

 1) Reference WindowsServiceHost NuGet [package](https://www.nuget.org/packages/WindowsService.Host/)
 
 2) Create a class, derived from *WindowsService*
```csharp
public class MyServiceHost : WindowsService
{
///
}
```
 3) In the class, created on the previous step, implement function *GetServiceConfiguration()*
```csharp
protected override ServiceConfiguration GetServiceConfiguration()
  => new ServiceConfiguration
   {
      ServiceName = "MyServiceName",
      DisplayName = "MyService Display Name",
      OnStart = Start,  // method to be executed on service start
      OnStop = Stop,    // method to be executed on service stop
      Account = ServiceAccount.User,    // account, under which the service will be running
      Installers = new[] {new EventLogInstaller()}  // any additional Installer you would like to be executed as well
    };
```
 4) Implement *Start()* and *Stop()* methods, referenced in the *GetServiceConfiguration()*
 
 5) In *Main()* method instantiate an object of the class create at step (1) and call *Run()* method on it:
```csharp
internal static class Program
{
  private static void Main()
    => new MyServiceHost().Run();
}
```
Command-line arguments
-------------------------
The following command-line arguments are supported:

 * multiple installations of a service on the same machine:

*installutil* /serviceName="*service_instance_name*" /displayName="*service_instance_display_name*" *MyService.exe*

*installutil* /serviceName="*service_instance_name*" *MyService.exe* /uninstall


 * unattended installation with specific user account:

*installutil* /userName="*user_name*" /password="*pwd*" *MyService.exe*


 * installation with pre-defined account:

*installutil* /account="*LocalService|NetworkService|LocalSystem*" *MyService.exe*

