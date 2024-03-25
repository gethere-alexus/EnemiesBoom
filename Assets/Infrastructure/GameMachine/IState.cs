namespace Infrastructure.GameMachine
{
    public interface IState
    {
        /// <summary>
        /// Called when machine is entering the state
        /// </summary>
        void Enter();
        
        /// <summary>
        /// Called when machine is leaving the state
        /// </summary>
        void Exit();
    }
}