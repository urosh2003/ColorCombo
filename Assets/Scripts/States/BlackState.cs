public class BlackState : IState
{
    public override IState AddBlue()
    {
        return this;
    }

    public override IState AddRed()
    {
        return this;
    }

    public override IState AddYellow()
    {
        return this;
    }

    public override void Enter()
    {
        PlayerManager.instance.SetBlack();
    }

    public override void Exit()
    {
    }
}