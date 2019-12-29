![](https://i.imgur.com/0GJXsIU.png)
# BotFS
A general purpose database wrapper with multiple database providers.

### Status of the NuGet Package : 

[![NuGet-BotFS](https://img.shields.io/nuget/vpre/BotFS.svg)](https://nuget.org/packages/BotFS) 
[![NuGet-BotFS.MongoDB](https://img.shields.io/nuget/vpre/BotFS.MongoDB.svg)](https://nuget.org/packages/BotFS.MongoDB) 
## How to use BotFS
`Install the Nuget Packages with:`
`nuget install BotFS.Provider`

Eg. Connecting to a MongoDB database
```cs
using BotFS.MongoDB;
class Program
{
  //Mongo(connectionString,Name)
  Mongo m = new Mongo("localhost","MongoDB Server");
}
```


Eg. Inserting/Finding/Deleting/Updating
```cs
MongoDatabase local = m.GetDatabase("local");
MongoTable<POCO> table = local.GetTable<POCO>("table");
table.Add(new POCO {Id=1, Value="test"});
var query = table.Find(x => x.Value == test).Value;
var newPOCO = new POCO {Id = query.Id, Value = "Update"};
table.Update(x => x.Id == query.Id, newPOCO);
table.Delete(x => x.Id == query.Id);
````
And thats it! BotFS will handle connection, insertions, updates, deletions and errors.

## Available Providers
- [x] MongoDB
