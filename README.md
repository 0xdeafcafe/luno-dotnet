# luno-dotnet
[![Build status](https://img.shields.io/appveyor/ci/0xdeafcafe/luno-dotnet.svg?style=flat-square&label=windows%20build)](https://ci.appveyor.com/project/0xdeafcafe/luno-dotnet) [![NuGet](https://img.shields.io/nuget/v/LunoClient.svg?style=flat-square)](https://www.nuget.org/packages/LunoClient/) [![Github Issues](https://img.shields.io/github/issues/0xdeafcafe/luno-dotnet.svg?style=flat-square)](https://github.com/0xdeafcafe/luno-dotnet/issues) [![Github Forks](https://img.shields.io/github/forks/0xdeafcafe/luno-dotnet.svg?style=flat-square)](https://github.com/0xdeafcafe/luno-dotnet/network) [![Github Stars](https://img.shields.io/github/stars/0xdeafcafe/luno-dotnet.svg?style=flat-square)](https://github.com/0xdeafcafe/luno-dotnet/stargazers) [![Github License](https://img.shields.io/github/license/0xdeafcafe/luno-dotnet.svg?style=flat-square)](https://github.com/0xdeafcafe/luno-dotnet/blob/master/LICENSE.md)

A dotnet wrapper for [luno.io](http://luno.io) - *also supports vnext* 💃

> breaking changes, alot to do. yada yada yada

### Getting Started
To simply get a list of users, you can do the following:
```csharp
var key = "my_luno_key";
var secret = "my_luno_secret";
var connection = new ApiKeyConnection(key, secret);
var client = new Luno.LunoClient(connection);
var allUsers = await client.User.GetAllAsync();
```
