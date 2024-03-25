namespace Infrastructure.GameMachine.States
{
    /// <summary>
    /// The state goes after LoadGameState, once all the components are prepared
    /// </summary>
    public class GameLoopState : IState 
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
          
        }

        public void Exit()
        {
           
        }
    }
}