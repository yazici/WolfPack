﻿namespace Wolfpack
{
    public enum GameStatus
    {
        Unknown,
        Intro,
        Menu,
        Tutorial,
        Game,
        Lost,
    }
    
    public enum LevelName
    {
        Menu,
        Game,
        Unknown
    }
    
    public enum GameLifecycleAction
    {
        StartGame,
        ExitGame,
        Options,
    }

    public enum FadeDirection
    {
        In, 
        Out
    }
}