public class BlueState : IState
{
    public override IState AddBlue()
    {
        throw new System.NotImplementedException();
    }

    public override IState AddRed()
    {
        return new PurpleState();
    }

    public override IState AddYellow()
    {
        return new GreenState();
    }

    public override void Enter()
    {
        PlayerManager.instance.SetBlue();
    }

    public override void Exit()
    {
    }
}