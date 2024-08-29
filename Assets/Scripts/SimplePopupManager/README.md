# Valerie's Test Task

Here's the implementation of the requirements. Simple popup window showing the leaderboard or players, loaded from JSON file and avatars, fetched from the web , and cached for future use.

# Overview
- A loading screen was added as a good practice.
- Using popup service by simply calling it and passing params.
- Rank Cards design is set in ScriptableObject so we wouldn't have to tweak prefabs every time height or colours change.
- Using FlowEnt library as a resource efficient animation tween

# PopupManagerServiceService
A brief review of changes to the PopupManagerServiceService and the reasonings for them.
- Renamed to PopupManagerService because the name was confusing.
- Changed to be a singleton. I know that singletons are controversial, but I think that for this app it just makes sense. The definition of a popup is such that it can appear on any screen anytime. And to avoid duplicates and increase readability it's been made a singleton.
- OpenPopup method now has a return type of 'Task'. That was done to be able to track async loading of a popup. In this example I used it to make Leaderboard button uninteractable on the time it takes to load the popup, therefore preventing spam clicking from loading multiple entities.

# What can be improved upon
Some things that I didn't implement because of the reasonable time constraint.
### Pooling
UI elements object pooling. Obviously, loading 100 cards at once is a bad idea for performance reasons. That's why I added a button to load more after the initial 10 has been displayed.
This has a few downfalls.
1. Customer experience. I would expect the app to scroll data from me without pressing the button.
2. You can still load all 100 objects and lag the app (even crash on IOS).
   Therefore I would implement some pooling solutions like that one:
   https://github.com/disas69/Unity-PooledScrollList-Asset
### Unwraped async calls
There are a few rogue async calls in the code. The downfall of that is that it can incorrectly work with Unity systems and won't display errors/debug messages in the log. I would wrap it in async context and run it alongside Unity's lifetime.
### Version upgrade
I upgraded Unity version of that project from the original one to the latest 2021 LTS. The reason is time constraints. Closer to finishing up I used FlowEnt Tween library to do animations. Turns out they don't support that version. To avoid remaking animations and save time I upgraded the project. You can still downgrade it and it should work just fine in the editor. Sorry for the inconvenience.


