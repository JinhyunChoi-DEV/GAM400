namespace BattleZZang
{
    public abstract class StateMachine
    {
        protected IState current;

        public void Change(IState next)
        {
            current?.Exit();

            current = next;

            current.Enter();
        }

        public void HandleInput()
        {
            current?.HandleInput();
        }

        public void Update()
        {
            current?.Update();
        }

        public void FixedUpdate()
        {
            current?.FixedUpdate();
        }
    }
}
