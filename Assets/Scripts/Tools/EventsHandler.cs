using System;

public static class EventsHandler
{
    public static Action OnGameFinished;

    public static void InvokeOnGameFinished()
    {
        OnGameFinished?.Invoke();
    }   
}