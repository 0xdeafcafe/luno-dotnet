# luno-dotnet
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
