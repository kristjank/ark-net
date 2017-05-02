
![alt text](https://github.com/kristjank/ark-net/blob/master/ark-net/res/arknet-new.png)

Copyright 2017 **chris**, Copyright 2017 **ARK**

### Ark.NET
Ark.NET is the ARK Ecosystem library for the .NET platform. It implements all most relevant ARK functionalities to help you  develop efficient .NET applications built upon ARK platform. It provides also low level access to ARK so you can easily build your application on top of it. 

The package supports:
* With full features, Windows Desktop applications, Mono Desktop applications, and platform supported at [.NET Standard Library](https://docs.microsoft.com/en-us/dotnet/articles/standard/library). Should work will all .NET solutions with framework > 4.5.2.
* It's a work in progress so mobile platform testing are still to follow. 

[![Source Browser](https://img.shields.io/badge/Browse-Source-green.svg)](http://sourcebrowser.io/Browse/kristjank

# How to install?

### With nuget:
[![nuget](https://img.shields.io/badge/nuget-prerelease-yellow.svg)](https://www.nuget.org/packages/ark.net/)
>**Install-Package ark.net** 
Go on the [nuget website](https://www.nuget.org/packages/ark.net/) for more information.

### From source:
To compile it by yourself, you can git clone, open the project and hit the compile button on visual studio.
In command prompt:
```
git clone https://github.com/kristjank/ark-net
cd ark-net
```
# How to get started? 

All ark-node services have available reponses have their object representations in the form of ValueObjects. You can user service classes under [service folder](https://github.com/kristjank/ark-net/tree/master/ark-net/service). Responses are IEnumerable or IQueryable (depends if the class and functionality).

It's best to let the code do the speaking. For more examples look at the [ARK.NET Tests](https://github.com/kristjank/ark-net/blob/master/ark-netTests/io/ark/core/ModelTests.cs#L22), where all tests are written and you can see the api usage. Some code snippets are below.


### Service layer 
For a full list of available api calls please look at the  [ARK.NET Test project](https://github.com/kristjank/ark-net/blob/master/ark-netTests/)
```c#
//PeerService
var peers = PeerService.GetAll();
var peersOK = peers.Where(x => x.Status.Equals("OK"));

//TransactionService
var trans = TransactionService.GetAll();
...
```

### Core Layer 
Layer is used for core Ark blockchain communication (transaction, crypto...). It is wrapped by api libraries that are called from the service layer.
```c#
TransactionApi tx = TransactionApi.CreateTransaction(recepient, amount, description, passphrase);
Peer peer = Network.Mainnet.GetRandomPeer();
var result = peer.PostTransaction(tx);          
```

## More information about ARK Ecosystem and etc
* [ARK Ecosystem Wiki](https://github.com/kristjank/wiki)
* **Ebook** [Programming The Blockchain in C#](https://www.gitbook.com/book/programmingblockchain/programmingblockchain/details)

Please, use github issues for questions or feedback. For confidential requests or specific demands, contact us on our public channels.


## Useful link for a free IDE :
Visual Studio Community Edition : [https://www.visualstudio.com/products/visual-studio-community-vs](https://www.visualstudio.com/products/visual-studio-community-vs "https://www.visualstudio.com/products/visual-studio-community-vs")

## Authors
Chris (kristjan.kosic@gmail.com), with a lot of help from FX Thoorens fx@ark.io and ARK Community

## Support this project
![alt text](https://github.com/Moustikitos/arky/raw/master/ark-logo.png)
Ark address:``AUgTuukcKeE4XFdzaK6rEHMD5FLmVBSmHk``


# License
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Copyright (c) 2017 ARK




