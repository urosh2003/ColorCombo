public class BlackState : IState
{
    public override IState AddBlue()
    {
        return new BlackState();
    }

    public override IState AddRed()
    {
        return new BlackState();
    }

    public override IState AddYellow()
    {
        return new BlackState();
    }

    public override void Enter()
    {
        PlayerManager.instance.SetBlack();
    }

    public override void Exit()
    {
    }
}