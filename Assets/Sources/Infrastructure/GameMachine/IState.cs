namespace Sources.Infrastructure.GameMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}