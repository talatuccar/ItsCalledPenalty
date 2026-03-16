using Project.Infrastructure.DependencyInjection;

public class GameStateMachine : IService
{
    private IGameState currentState;

    public void ChangeState(IGameState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState.Enter();
    }
}