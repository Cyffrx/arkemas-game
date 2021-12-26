using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MunsterStateMachine : _DefaultStateMachine
{
    public override void _Ready()
    {
        base._Ready();

        List<MunsterState> munsterStates = this.GetChildren().OfType<MunsterState>().ToList();
        for (int m = 0; m < munsterStates.Count; m++) munsterStates[m].MSM = this;
        
        ChangeState(munsterStates[0].Name);
    }

    public void ChangeState(string stateName, Dictionary<string, object> message = null)
    {
        base.ChangeState(stateName, message);

        Owner.GetNode<Label>("StateLabel").Text = stateName;
    }
}
