# WallRunner

A twin stick shooter made in one week for the Code for a Cause game jam.

![Gameplay demo](Media/WallRunner(1).gif)
> Not sure why the gif looks so bad on github, the weird grid isn't actually there.

## About

WallRunner is a fast-paced dungeon shooter where you must fight enemies and outpace a wall that will kill you if you lag behind.

The jam's theme was "Keep Moving Forward", which inspired both the kill-wall mechanic and the progression system for each run.

This game was built in one week (with minor post jam updates). You may play the most recent version of the game and download a build [here](http://bit.ly/42UFdnV).

This game placed 16th in the judge rankings and 9th in the community ranking against ~250 other entries.

## Gameplay Features
- Twin stick shooter with keyboard and mouse inputs
- Randomized dungeon structure on each run
- Constant forward pressure from a kill wall

## Difficulty System
The most interesting system to implement was based on a mix of Transistor and an April Fool's Minecraft snapshot.
When a player levels up, they may choose between one of two different boons. However, each boon will also come with a drawback that is applied to either their enemies or the wall pursuing them.
When a player dies, they keep their boons, yet are allowed the choice to remove one of two drawbacks.
The intent behind this system was to make death an indirect way to "Keep Moving Forward".

## Tech Stack
- Engine: Unity 2022
- Language: C#
- Audio: jsfxr

## Post-Jam Improvements
- Tweaked game balance to be more fair and less grindy
- Added a public leaderboard system
