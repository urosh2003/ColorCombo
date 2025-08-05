using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WhiteState : IState
{
    public override IState AddBlue()
    {
        return new BlueState();
    }

    public override IState AddRed()
    {
        return new RedState();
    }

    public override IState AddYellow()
    {
        return new YellowState();
    }

    public override void Enter()
    {
        PlayerManager.instance.SetWhite();
    }

    public override void Exit()
    {
    }
}

