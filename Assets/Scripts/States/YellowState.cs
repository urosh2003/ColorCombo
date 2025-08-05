public class YellowState : IState
{
    public override IState AddBlue()
    {
        return new GreenState();
    }

    public override IState AddRed()
    {
        return new OrangeState();
    }

    public override IState AddYellow()
    {
        return new YellowState();
    }

    public override void Enter()
    {
        PlayerManager.instance.SetYellow();
    }

    public override void Exit()
    {
        
    }
}