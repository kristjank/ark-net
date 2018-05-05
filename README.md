![alt text](https://github.com/kristjank/ark-net/blob/master/ark-net/res/arknet-new.png)

[![Waffle.io - Columns and their card count](https://badge.waffle.io/ArkEcosystem/ark-net.png?columns=all)](https://waffle.io/ArkEcosystem/ark-net?utm_source=badge)
[![GitHub issues](https://img.shields.io/github/issues/kristjank/ark-net.svg)](https://github.com/kristjank/ark-net/issues)&nbsp;[![GitHub forks](https://img.shields.io/github/forks/kristjank/ark-net.svg)](https://github.com/kristjank/ark-net/network)&nbsp;[![GitHub stars](https://img.shields.io/github/stars/kristjank/ark-net.svg)](https://github.com/kristjank/ark-net/stargazers)&nbsp;[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/kristjank/ark-net/master/LICENSE)

### Why Ark.NET
Ark.NET is the ARK Ecosystem library for the .NET platform. It implements all most relevant ARK functionalities to help you  **develop efficient .NET applications built upon ARK platform**. It provides also low level access to ARK so you can easily build your application on top of it. 

The package supports:
* With full features, Windows Desktop applications, Mono Desktop applications, and platform supported at [.NET Standard Library](https://docs.microsoft.com/en-us/dotnet/articles/standard/library). Works on all .NET solutions with framework > 4.7.1 & Net Standard 2.0.
* Async/Await with coresponding synchronous methods

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

All ark-node services have available reponses have their object representations in the form of ValueObjects. You can use service classes under [service folder](https://github.com/ArkEcosystem/ark-net/tree/master/ark-net/Service). Responses are IEnumerable.  Every method has a cooresponding async method.

It's best to let the code do the speaking. For more examples look at the [ARK.NET Tests](https://github.com/ArkEcosystem/ark-net/tree/master/ark-netTests), where all tests are written and you can see the api usage. Some code snippets are below.


### Ark.Net Client init
**First call should be network selection, so all settings can initialize before going into action.**

```c#
  await ArkNetApi.Instance.Start(NetworkType.MainNet); //Other type is DevNet
```

### Account/Wallet layer
```c#
var accCtnrl = new AccountController("top secret pass");
//Send ARK
var result = accCtnrl.SendArk(100, "AUgTuukcKeE4XFdzaK6rEHMD5FLmVBSmHk", "Akr.Net test trans from Account");
//Vote 4 Delegate                
var result = accCtnrl.VoteForDelegate( votes, "top secret pass");
//Create and send transaction.  Transaction can be saved offine (.ToJson()) and sent later.              
var transaction = accCtnrl.CreateTransaction(100, "AUgTuukcKeE4XFdzaK6rEHMD5FLmVBSmHk", "Akr.Net test trans from Account");
var result = accCtnrl.SendTransaction(transaction);
//Generate passphrase
var result = AccountService.GeneratePassphrase()
```

### Service layer 
For a full list of available api calls please look at the  [ARK.NET Test project](https://github.com/ArkEcosystem/ark-net/blob/master/ark-netTests/)
```c#
//PeerService
var peers = PeerService.GetAll();
var peersOK = peers.Where(x => x.Status.Equals("OK"));

//TransactionService
var trans = TransactionService.GetAll();
...
```
### Core Layer 
Layer is used for core Ark blockchain communication (transaction, crypto...). It is wrapped by api libraries that are called from the service and Account layer.
```c#
TransactionApi tx = TransactionApi.CreateTransaction(recepient, amount, description, passphrase);
Peer peer = NetworkApi.Instance.GetRandomPeer();
var result = peer.PostTransaction(tx);          
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




