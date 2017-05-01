
![alt text](https://github.com/kristjank/ark-net/blob/master/ark-net/res/arknet-new.png)

Ark.NET is the ARK Ecosystem library for the .NET platform. It implements all most relevant ARK functionalities to help you  develop efficient .NET applications built upon ARK platform. It provides also low level access to ARK so you can easily build your application on top of it. 

### Authors
Chris (kristjan.kosic@gmail.com), with a lot of help from FX Thoorens fx@ark.io. Project is a .NET port of [ARK-JAVA](https://github.com/ArkEcosystem/ark-java)

The package supports:
* With full features, Windows Desktop applications, Mono Desktop applications, and platform supported at [.NET Standard Library](https://docs.microsoft.com/en-us/dotnet/articles/standard/library). Should work will all .NET solutions with framework > 4.5.2.
* It's a work in progress so mobile platform testing are still to follow. 

### Note
**This is a work in progress**. As some JAVA libs were "ported" there will internal refactorings and optimizations - aligning the code with .NET. Nevertheless - it is already functional and you can include it in your solutions for ARK Ecosystem.

# How to install?
### From source:
To compile it by yourself, you can git clone, open the project and hit the compile button on visual studio.
In command prompt:
```
git clone https://github.com/kristjank/ark-net
cd ark-net
```
### With nuget:
>**Install-Package ark.net** 

Go on the [nuget website](https://www.nuget.org/packages/ark.net/) for more information.


# How to get started? 

All ark-node services have available reponses have their object representations in the form of ValueObjects. Classes are under [model folder](https://github.com/kristjank/ark-net/tree/master/ark-net/io/ark/model) and have VO appended to their name (e.g. TransactionVO, PeerVO, DelegateVO, AccountVO...).

It's best to let the code do the speaking. For more examples look at the [ARK.NET Tests](https://github.com/kristjank/ark-net/blob/master/ark-netTests/io/ark/core/ModelTests.cs#L22), where all tests are written and you can see the api usage. Some code snippets are below.

### Create and post transaction

```c#
Transaction tx = Transaction.CreateTransaction(recepient, amount, description, passphrase);
Peer peer = Network.Mainnet.GetRandomPeer();
var result = peer.PostTransaction(tx);          
```
### More API calls
For a full list of available api calls please look at the  [ARK.NET Test project](https://github.com/kristjank/ark-net/blob/master/ark-netTests/)

```c#
//PeerService
var peers = PeerService.GetAll();
var peersOK = peers.Where(x => x.Status.Equals("OK"));

//TransactionService
var trans = TransactionService.GetAll();
...
```

## More information about ARK Ecosystem and etc
* [ARK Ecosystem Wiki](https://github.com/kristjank/wiki)
* **Ebook** [Programming The Blockchain in C#](https://www.gitbook.com/book/programmingblockchain/programmingblockchain/details)

Please, use github issues for questions or feedback. For confidential requests or specific demands, contact us on our public channels.


## Useful link for a free IDE :
Visual Studio Community Edition : [https://www.visualstudio.com/products/visual-studio-community-vs](https://www.visualstudio.com/products/visual-studio-community-vs "https://www.visualstudio.com/products/visual-studio-community-vs")

# License
The MIT License (MIT)

Copyright (c) 2017 ARK

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.







