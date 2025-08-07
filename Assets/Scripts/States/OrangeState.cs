public class OrangeState : IState
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
        PlayerManager.instance.SetOrange();
    }

    public override void Exit()
    {
    }
}
