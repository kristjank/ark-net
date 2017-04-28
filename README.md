# ARKNET - .NET CLIENT API FOR ARK ECOSYSTEM

Ark.NET is the ARK Ecosystem library for the .NET platform. It implements all most relevant ARK functionalities for you to provide and develop efficient .NET applications built upon ARK platform. It provides also low level access to ARK so you can easily build your application on top of it. 

# How to use ?
With nuget :
>**Install-Package ark.net** 

Go on the [nuget website](https://www.nuget.org/packages/ark.net/) for more information.

The packages supports:

* With full features, Windows Desktop applications, Mono Desktop applications, and platform supported at [.NET Standard 1.3](https://docs.microsoft.com/en-us/dotnet/articles/standard/library). Should work will all .NET solutions with framework > 4.5.2.
* It's a work in progress so mobile platform testing are still to follow. 

To compile it by yourself, you can git clone, open the project and hit the compile button on visual studio.

# How to get started? 

All service available reponses have their object representations in the form of ValueObjects. Classes are under model folder and have VO appended to their name (e.g. TransactionVO, PeerVO, DelegateVO, AccountVO...).

It's best to let the code do the speaking. For more examples looke at the [ARKNET Tests](hhttps://github.com/kristjank/ark-net/blob/master/ark-netTests/io/ark/core/ModelTests.cs#L22), where all tests are written and you can see the api usage. Some code snippets are below.

## Create and post transaction

```c#
Transaction tx = Transaction.CreateTransaction(recepient, amount, description, passphrase);

Peer peer = Network.Mainnet.GetRandomPeer();
string result = peer.PostTransaction(tx);          

```

## Get ARK peers

```c#
   List<PeerVO> peers = Network.Mainnet.GetRandomPeer().GetPeers();

   IEnumerable<PeerVO> peersOk = peers.Where(x => x.status.Equals("OK")); //Linq query over the object
```





