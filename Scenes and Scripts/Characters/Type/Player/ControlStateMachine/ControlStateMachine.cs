using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
public class ControlStateMachine : _DefaultStateMachine
{
    
    public override void _Ready()
    {
        base._Ready();
        
        List<ControlState> controlStates = this.GetChildren().OfType<ControlState>().ToList();

        for (int cs = 0; cs < controlStates.Count; cs++) controlStates[cs].CSM = this;

        ChangeState(controlStates[0].Name);
    }
}
