# ARKNET - .NET CLIENT API FOR ARK ECOSYSTEM

Ark.NET is the ARK Ecosystem library for the .NET platform. It implements all most relevant ARK functionalities to help you  develop efficient .NET applications built upon ARK platform. It provides also low level access to ARK so you can easily build your application on top of it. 

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

### Create and post transaction

```c#
Transaction tx = Transaction.CreateTransaction(recepient, amount, description, passphrase);

Peer peer = Network.Mainnet.GetRandomPeer();
string result = peer.PostTransaction(tx);          

```

### Some api calls
For a full list of avaiable api calls please look at the  [ARKNET Test project](hhttps://github.com/kristjank/ark-net/blob/master/ark-netTests/io/ark/core/)

```c#
List<PeerVO> peers = Network.Mainnet.GetRandomPeer().GetPeers();

List<TransactionVO> trans = Network.Mainnet.GetRandomPeer().GetTransactions();

PeerStatusVO peerStat = Network.Mainnet.GetRandomPeer().GetPeerStatus();

TransactionVO trans = Network.Mainnet.GetRandomPeer().GetTransaction("3a9643dcf9631384df6cb8c7aec50d782e8da5dfd4b44c22cd1f10c6434ee00c");

List<DelegateVO> dele = Network.Mainnet.GetRandomPeer().GetDelegates();

DelegateVO dele1 = Network.Mainnet.GetRandomPeer().GetDelegatebyUsername(dele[0].username);

DelegateVO dele22 = Network.Mainnet.GetRandomPeer().GetDelegatebyPubKey(dele[0].publicKey);
DelegateVO dele33 = Network.Mainnet.GetRandomPeer().GetDelegatebyAddress(dele[0].address);

List<DelegateVotersVO> voters = Network.Mainnet.GetRandomPeer().GetDelegateVoters(dele[0].publicKey);

AccountVO accountTest = Network.Mainnet.GetRandomPeer().GetAccountbyAddress(dele[0].address);

```







