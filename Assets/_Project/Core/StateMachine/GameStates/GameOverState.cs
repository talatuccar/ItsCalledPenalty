using UnityEngine;

public class GameOverState : IGameState
{
    public void Enter()
    {
        Debug.Log("Game Over State Enter");
    }

    public void Exit()
    {
        Debug.Log("Game Over State Exit");
    }
}