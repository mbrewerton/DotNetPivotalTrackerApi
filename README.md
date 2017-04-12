# DotNetPivotalTrackerApi
## About
The DotNet Pivotal Tracker API is a C# wrapper to enable easy use of the Pivotal Tracker REST API in C#.

## Basic Guide
To start using the API, create a new PivotalTracker instance `PivotalTracker tracker = new PivotalTracker(YourApiToken)` and you can use all methods in the package from that instance. You must pass a Pivotal Tracker API Key to the PivotalTracker class as it uses this to communicate with the REST API. You can have multiple instances at once with multiple API keys if needed:
``` C#
PivotalTracker personalTracker = new PivotalTracker(PersonalApiKey);
PivotalTracker businessTracker = new PivotalTracker(BusinessApiKey);
```

## Examples
You can use the API to get your user data as well as specific REST actions:
``` C#
PivotalTracker tracker = new PivotalTracker(_apiKey_);

PivotalUser user = tracker.GetUser();
```
All methods return an object, including any POST or PUT methods. This means you can use previously created objects when making subsequent calls.

Not only can you use the API to call GET methods on the Pivotal Tracker REST API, but you can also use to it POST.
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

# Boring Stuff
##MIT License

Copyright (c) 2016

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
