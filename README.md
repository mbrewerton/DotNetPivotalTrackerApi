# .NET Wrapper for Pivotal Tracker Api

Master Build Status: [![Build status](https://ci.appveyor.com/api/projects/status/j22e5m0yp3j29gmj/branch/master?svg=true)](https://github.com/mbrewerton/DotNetPivotalTrackerApi/tree/master)

Development Build Status: [![Build status](https://ci.appveyor.com/api/projects/status/j22e5m0yp3j29gmj/branch/develop?svg=true)](https://github.com/mbrewerton/DotNetPivotalTrackerApi/tree/develop)


## About
The .NET Wrapper for Pivotal Tracker API is a .NET wrapper to enable easy use of the Pivotal Tracker REST API. It is current compatible with .NET4.5 and UAP10.0 applications.

You can access the nuget package here: [Nuget Package](https://www.nuget.org/packages/Mbrewerton.DotNetPivotalTrackerApi).

## Contribution
Anyone can contribute to the project via Pull Requests (PRs), but all PRs must be created against the `develop` branch. Contribution branches would preferably be created using gitflow - Create a branch called `feature/your_feature_to_add` from `develop` and then create a PR for `feature/your_feature_to_add` -> `develop`. If you are picking off an item in the TODO list, please include the item name in the description section of your PR.

## Basic Guide
To start using the API, create a new PivotalTracker instance `PivotalTracker tracker = new PivotalTracker(YourApiToken)` and you can use all methods in the package from that instance. You must pass a Pivotal Tracker API Key to the PivotalTracker class as it uses this to communicate with the REST API. You can have multiple instances at once with multiple API keys if needed:
``` C#
PivotalTracker personalTracker = new PivotalTracker(PersonalApiKey);
PivotalTracker businessTracker = new PivotalTracker(BusinessApiKey);
```

## Examples

### Types of Authentication
You can authenticate in two ways: API Token or user credentials. Here's an example using an API token:
``` C#
// This tracker is already authenticated and can start calling the REST API immediately
PivotalTracker tracker = new PivotalTracker("MyApiToken");
```

In order to authenticate using credentials, you need to instantiate your tracker instance slightly differently:
``` C#
// Note the use of the default constructor. We don't want to pass a token here!
PivotalTracker tracker = new PivotalTracker();
// If this call succeeds, your `tracker` instance is now authenticated.
tracker.Authorize("myusername", "mypassword");
```
tl;dr on how the new authentication mode works: It authenticates with Pivotal Tracker using the credentials entered, then if successful it will grab the API Token for the user and use that for subsequent requests. The client using the credentials is disposed as it's not needed.

### Get User Info
You can use the API to get your user data as well as specific REST actions:
``` C#
PivotalTracker tracker = new PivotalTracker(_apiKey_);

PivotalUser user = tracker.GetUser();
Console.WriteLine($"User Info: {user.Name} ({user.Initials}) has username {user.Username} and Email {user.Email}");
```
All methods return an object, including any POST or PUT methods. This means you can use previously created objects when making subsequent calls. DELETE requests will return a boolean, returning `true` if it was successful. If there was an error, a `PivotalHttpException` will be thrown allowing you to try/catch for it. The exception message will include the full error message from Pivtotal Tracker.

### Creating a New Story
``` C#
PivotalTracker tracker = new PivotalTracker(_apiKey_);
int projectId = 1357;

// Create a new story with c'tor
PivotalStory savedStory = tracker.CreateNewStory(projectId, "My new Feature", StoryType.feature, null, "My description");

// Create a new story with a pre-made object
PivotalNewStory myBug = new PivotalNewStory
{
  Name = "My new story",
  Description = "My new description",
  StoryType = Enums.StoryType.bug.ToString(),
  Labels = new List<string>
  {
      "My label"
  }
}
PivotalStory savedBug = tracker.CreateNewStory(projectId, myBug);
```

### Adding a Task to a Story
``` C#
int projectId = 1357;
PivotalStory story = tracker.GetProjectStories(projectId).First();
PivotalTask storyTask = tracker.CreateNewStoryTask(projectId, story.Id, "This is a task");
```

## Release Notes

### 1.0.5-Alpha
- Added authorisation for username/password authentication
  - see the [Authentication Section](https://www.nuget.org/packages/Mbrewerton.DotNetPivotalTrackerApi#types-of-authentication)

### 1.0.4-Alpha
- Fixed previously broken build
- Included support for UAP applications in addition to regular .NET 4.5 applications
  - This uses the same namespace (`DotNetPivotalTrackerApi`) unlike the previous "Portable" build

### ~~1.0.3-Alpha1~~
~~- Package is now compatible with UWP/UAP10 projects by referencing the `DotNetPivotalTrackerApi.Portable` namespace~~

### 1.0.1-Alpha2
- Added the ability to persist a Project Id to a PivotalTracker instance by passing as a parameter
- Fixed HttpClient conflicts when using multiple PivotalTracker instances at once

### 1.0.1-Alpha1
- Add GetUserInfo method to Examples
- Add GetProjects method to Examples
- Add CreateNewStory method to Examples

### 1.0.1-Alpha
- Add examples project to the solution

### 1.0.0-Alpha
- Initial development release
- Features are limited to Get, Update, Delete stories, comments and tasks
- Comments handle file attachments

## TODO
This TODO list is not in any prioritised order, it is basically a dumping ground for the next features that I am planning to work on. If you wish to contribute, check out the [Contribution](https://github.com/mbrewerton/DotNetPivotalTrackerApi#contribution) section.

- Make methods truly Async with syncronous counterparts to provide async flexiblity
- Create a custom HttpHandler for the HttpClient to provide Unit Testability
- Provide the ability to chain methods using fluent syntax
- Implement project statistics such as velocity
- Provide GET/POST/PUT/DELETE functionality for Epics
- Eventually cover all PT endpoints... Give a dev a chance!

# Boring Stuff
## MIT License

Copyright (c) 2016

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
