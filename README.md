# WindowsServiceHost

Simple Windows Service hosting library. Allows to install service with *installutil.exe* into Service Control Manager (SCM) or run as a Console Application.

# Requirements
.NET 4.6.x

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
      OnStop = Stop,    // mehtod to be executed on service stop
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
