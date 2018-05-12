![ARK.Net](https://user-images.githubusercontent.com/25795454/39834208-83a7b9ca-53cc-11e8-980e-fd7aa281c540.png)

[![Waffle.io - Columns and their card count](https://badge.waffle.io/ArkEcosystem/ark-net.png?columns=all)](https://waffle.io/ArkEcosystem/ark-net?utm_source=badge)
[![GitHub forks](https://img.shields.io/github/forks/kristjank/ark-net.svg)](https://github.com/kristjank/ark-net/network)&nbsp;[![GitHub stars](https://img.shields.io/github/stars/kristjank/ark-net.svg)](https://github.com/kristjank/ark-net/stargazers)&nbsp;[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/kristjank/ark-net/master/LICENSE)

### Why Ark.NET
Ark.NET is the ARK Ecosystem library for the .NET platform. It implements all most relevant ARK functionalities to help you  **develop efficient .NET applications built upon ARK platform**. It provides also low level access to ARK so you can easily build your application on top of it. 

The package supports:
* With full features, Windows Desktop applications, Mono Desktop applications, and platform supported at [.NET Standard Library](https://docs.microsoft.com/en-us/dotnet/articles/standard/library). Works on all .NET solutions with framework >= 4.7.1 & Net Standard 2.0.
* Async/Await with corresponding synchronous methods.

[![Source Browser](https://img.shields.io/badge/Browse-Source-green.svg)](http://sourcebrowser.io/Browse/kristjank)

# How to install?

### With nuget:
[![nuget](https://img.shields.io/badge/nuget-prerelease-yellow.svg)](https://www.nuget.org/packages/ark.net/)
>**Install-Package ark.net** 
Go on the [nuget website](https://www.nuget.org/packages/ark.net/) for more information.

### From source:
To compile it by yourself, you can git clone, open the project and hit the compile button on visual studio.
In command prompt:
```
git clone https://github.com/ArkEcosystem/ark-net
cd ark-net
```
# How to get started? 

All ark-node service responses have object representations. You can use service classes under [service folder](https://github.com/ArkEcosystem/ark-net/tree/master/ark-net/Service). Responses are IEnumerable for a list or an object for a single item.  Every method has a corresponding async method.

It's best to let the code do the speaking. For more examples look at the [ARK.NET Tests](https://github.com/ArkEcosystem/ark-net/tree/master/ark-netTests), where all tests are written and you can see the api usage. Some code snippets are below.


### Ark.Net Client init
**First call needs to be start, so all settings within the library can initialize before going into action.   Multiple instances of ArkNetApi can be used at the same time (DevNet & MainNet in the same application)**

```c#
private ArkNetApi _arkNetApi;
public ArkNetApi ArkNetApi
{
    get { return _arkNetApi ?? (_arkNetApi = new ArkNetApi()); }
}
await ArkNetApi.Start(NetworkType.MainNet); //Other type is DevNet
//or
await ArkNetApi.Start(specificPeerIp, specificPeerPort);
```

### Account/Wallet layer
```c#
//Existing account
var accCtnrl = new AccountController(ArkNetApi, "top secret pass");

//Send ARK
var result = accCtnrl.SendArk(100, "AUgTuukcKeE4XFdzaK6rEHMD5FLmVBSmHk", "Akr.Net test trans from Account");

//Vote 4 Delegate                
var result = accCtnrl.VoteForDelegate( votes);

//Create and send transaction.  Transaction can be saved offine (.ToJson()) and sent later.              
var transaction = accCtnrl.CreateTransaction(100, "AUgTuukcKeE4XFdzaK6rEHMD5FLmVBSmHk", "Akr.Net test trans from Account");
var result = accCtnrl.SendTransaction(transaction);

//Get Account
var account = accCtnrl.GetArkAccount();

//New Account
new AccountController(ArkNetApi, ArkNetApi.AccountService.GeneratePassphrase());
```

### Service layer 
For a full list of available api calls please look at the  [ARK.NET Test project](https://github.com/ArkEcosystem/ark-net/blob/master/ark-netTests/)
```c#
//PeerService
var peers = ArkNetApi.PeerService.GetAll();
var peersOK = peers.Where(x => x.Status.Equals("OK"));

//TransactionService
var trans = ArkNetApi.TransactionService.GetAll();

//BlockService
var blocks = ArkNetApi.BlockService.GetAll();

//AccountService - Generate passphrase
var result = ArkNetApi.AccountService.GeneratePassphrase();

//DelegateService
var delegates = ArkNetApi.DelegateService.GetAll();

//LoaderService
var autoConfigParams = ArkNetApi.LoaderService.GetAutoConfigureParameters();
...
```
### Core Layer 
Layer is used for core Ark blockchain communication (transaction, crypto...). It is wrapped by api libraries that are called from the service and Account layer.
```c#
//Create & send transaction
TransactionApi tx = ArkNetApi.TransactionApi.CreateTransaction(recepient, amount, description, passphrase);
Peer peer = ArkNetApi.NetworkApi.GetRandomPeer();
var result = peer.PostTransaction(tx);

//Connect to a specific peer to perform requests
var peerApi = new PeerApi(ArkNetApi.NetworkApi, ipAddress, Port)
await peerApi.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_STATUS);

//Force specific peer.  All API calls will flow through this peer.  Set back to null to resume decentralized use.  Monitoring a specific is a use case for this functionality.
ArkNetApi.NetworkApi.ForcedPeer = ArkNetApi.PeerService.GetPeer(ip, port);
//or
ArkNetApi.NetworkApi.ForcedPeer = new PeerApi(ArkNetApi.NetworkApi, ipAddress, Port);

// Switch network (Can also create new ArkNetApi instance as alternative solution)
await ArkNetApi.SwitchNetwork(NetworkType.DevNet)

//New network
_arkNetApiDevNet = new ArkNetApi();
await _arkNetApiDevNet.Start(NetworkType.DevNet);      
```
### Logging 
Any logging framework can be used to capture the logs from within ArkNet.  It is up to the user of the library to implement IArkLogger and pass it to ArkNetApi.Start().  Below is an example implementation using Log4Net.
```c#
public class Log4netAdapter : IArkLogger
{
    private readonly ILog _log4NetLog;

    public Log4netAdapter(ILog log4NetLog)
    {
        _log4NetLog = log4NetLog;
    }

    public void Log(ArkLogEntry entry)
    {
        if (entry.LogLevel == ArkLogLevel.Debug)
            _log4NetLog.Debug(entry.Message, entry.Exception);
        else if (entry.LogLevel == ArkLogLevel.Info)
            _log4NetLog.Info(entry.Message, entry.Exception);
        else if (entry.LogLevel == ArkLogLevel.Warn)
            _log4NetLog.Warn(entry.Message, entry.Exception);
        else if (entry.LogLevel == ArkLogLevel.Error)
            _log4NetLog.Error(entry.Message, entry.Exception);
        else
            _log4NetLog.Fatal(entry.Message, entry.Exception);
    }
}

ILog log = LogManager.GetLogger(typeof(LoggingTests));
await ArkNetApi.Start(NetworkType.MainNet, new Log4netAdapter(log);
```

## More information about ARK Ecosystem and etc
* [ARK Ecosystem Wiki](https://github.com/ArkEcosystem/wiki)
* **Ebook** [Programming The Blockchain in C#](https://www.gitbook.com/book/programmingblockchain/programmingblockchain/details)

Please, use github issues for questions or feedback. For confidential requests or specific demands, contact us on our public channels.


## Useful link for a free IDE :
Visual Studio Community Edition : [https://www.visualstudio.com/products/visual-studio-community-vs](https://www.visualstudio.com/products/visual-studio-community-vs "https://www.visualstudio.com/products/visual-studio-community-vs")

## Authors
Chris (kristjan.kosic@gmail.com) & Sharkdev-j, with a lot of help from FX Thoorens fx@ark.io and ARK Community

## Support this project
![alt text](https://github.com/Moustikitos/arky/raw/master/ark-logo.png)
Ark address:``AUgTuukcKeE4XFdzaK6rEHMD5FLmVBSmHk``


# License
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Copyright (c) 2018 ARK




