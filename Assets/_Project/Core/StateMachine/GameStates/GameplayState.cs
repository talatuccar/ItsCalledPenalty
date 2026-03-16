using UnityEngine;

public class GameplayState : IGameState
{
    public void Enter()
    {
        Debug.Log("Gameplay State Enter");
    }

    public void Exit()
    {
        Debug.Log("Gameplay State Exit");
    }
}