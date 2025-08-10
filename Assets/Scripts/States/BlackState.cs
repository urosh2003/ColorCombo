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
        currentColor = WizardColor.BLACK;
        PlayerManager.instance.SetColor(currentColor);
    }

    public override void Exit()
    {
    }
}