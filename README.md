# luno-dotnet
[![NuGet](https://img.shields.io/nuget/v/LunoClient.svg?style=flat-square)](https://www.nuget.org/packages/LunoClient/)
[![Github Issues](https://img.shields.io/github/issues/0xdeafcafe/luno-dotnet.svg?style=flat-square)](https://github.com/0xdeafcafe/luno-dotnet/issues)
[![Github Forks](https://img.shields.io/github/forks/0xdeafcafe/luno-dotnet.svg?style=flat-square)](https://github.com/0xdeafcafe/luno-dotnet/network)
[![Github Stars](https://img.shields.io/github/stars/0xdeafcafe/luno-dotnet.svg?style=flat-square)](https://github.com/0xdeafcafe/luno-dotnet/stargazers)
[![Github License](https://img.shields.io/github/license/0xdeafcafe/luno-dotnet.svg?style=flat-square)](https://github.com/0xdeafcafe/luno-dotnet/blob/master/LICENSE)

A dotnet wrapper for [luno.io](http://luno.io) - *also supports vnext* 💃

> breaking changes, alot to do. yada yada yada

### Build Status

|Windows | Linux & OSX |
|:------:|:-----------:|
|[![Master Branch Build status](https://img.shields.io/appveyor/ci/0xdeafcafe/luno-dotnet/master.svg?style=flat-square&label=master%20branch%20build)](https://ci.appveyor.com/project/0xdeafcafe/luno-dotnet)| [![Master Branch Build status](https://img.shields.io/travis/0xdeafcafe/luno-dotnet/master.svg?style=flat-square&label=master%20branch%20build)](https://travis-ci.org/0xdeafcafe/luno-dotnet/branches)|
|[![Dev Branch Build status](https://img.shields.io/appveyor/ci/0xdeafcafe/luno-dotnet/dev.svg?style=flat-square&label=dev%20branch%20build)](https://ci.appveyor.com/project/0xdeafcafe/luno-dotnet)| [![Dev Branch Build status](https://img.shields.io/travis/0xdeafcafe/luno-dotnet/dev.svg?style=flat-square&label=dev%20branch%20build)](https://travis-ci.org/0xdeafcafe/luno-dotnet/branches)|

### Getting Started
To simply get a list of users, you can do the following:
```csharp
var key = "my_luno_key";
var secret = "my_luno_secret";
var connection = new ApiKeyConnection(key, secret);
var client = new LunoClient(connection);
var allUsers = await client.User.GetAllAsync();
```

### Library Examples
I have made an examples [repository](https://github.com/0xdeafcafe/luno-dotnet-examples). Check it out!