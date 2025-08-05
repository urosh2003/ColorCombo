public class RedState : IState
{
    public override IState AddBlue()
    {
        return new PurpleState();
    }

    public override IState AddRed()
    {
        return new RedState();
    }

    public override IState AddYellow()
    {
        return new OrangeState();
    }

    public override void Enter()
    {
        PlayerManager.instance.SetRed();
    }

    public override void Exit()
    {
    }
}
