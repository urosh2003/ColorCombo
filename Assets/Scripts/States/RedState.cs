public class RedState : IState
{
    public override IState AddBlue()
    {
        return new PurpleState();
    }

    public override IState AddRed()
    {
        return this;
    }

    public override IState AddYellow()
    {
        return new OrangeState();
    }

    public override void Enter()
    {
        currentColor = WizardColor.RED;
        PlayerManager.instance.SetColor(currentColor);
    }

    public override void Exit()
    {
    }
}
