# AKR API for .NET

Ark.NET is the ARK Ecosystem library for the .NET platform. It implements all most relevant ARK functionalities for you to provide and develop efficient .NET applications built upon ARK platform. It provides also low level access to ARK so you can easily build your application on top of it. 

# How to use ?
With nuget :
>**Install-Package ark-net** 

Go on the [nuget website](https://www.nuget.org/packages/ark.net/) for more information.

The packages supports:

* With full features, Windows Desktop applications, Mono Desktop applications, and platform supported at [.NET Standard 1.3](https://docs.microsoft.com/en-us/dotnet/articles/standard/library). Should work will all .NET solutions with framework > 4.5.2.
* With limited features, plateform supported at [.NET Standard 1.1](https://docs.microsoft.com/en-us/dotnet/articles/standard/library) (Windows Phone, Windows 8.0 apps).

To compile it by yourself, you can git clone, open the project and hit the compile button on visual studio.

#How to get started? 
##Create transaction

```c#
  Transaction tx = Transaction.CreateTransaction("AXoXnFi4z1Z6aFvjEYkDVCtBGW2PaRiM25",                                                              133380000000,
                                                            "This is first transaction from ARK-NET",
                                                              "this is a top secret passphrase");

            Peer peer = Network.Mainnet.GetRandomPeer();
            string result = peer.PostTransaction(tx);
            

```




