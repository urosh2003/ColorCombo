public class GreenState : IState
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
        currentColor = WizardColor.GREEN;
        PlayerManager.instance.SetColor(currentColor);
    }

    public override void Exit()
    {
    }
}