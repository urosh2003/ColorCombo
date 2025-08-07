using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity;

public abstract class IState
{
    public abstract void Enter();
    public abstract void Exit();
    public abstract IState AddRed();
    public abstract IState AddBlue();
    public abstract IState AddYellow();

    public IState ResetColor() { return new WhiteState(); }

}
