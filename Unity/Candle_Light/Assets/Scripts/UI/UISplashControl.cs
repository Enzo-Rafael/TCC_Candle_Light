using UnityEngine;

public enum SplashState
{
    START,
    EXIT,
    END
}

public class UISplashControl : Singleton<UISplashControl>
{
    public SplashState currentState;

    public void SetState(bool isEnd = false)
    {
        if(isEnd)
        {
            currentState = SplashState.END;
        }
        else
        {
            currentState = SplashState.EXIT;
        }
    }
}
