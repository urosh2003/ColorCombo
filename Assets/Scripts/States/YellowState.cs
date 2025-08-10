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
        return this;
    }

    public override void Enter()
    {
        currentColor = WizardColor.YELLOW;
        PlayerManager.instance.SetColor(currentColor);
    }

    public override void Exit()
    {
        
    }
}