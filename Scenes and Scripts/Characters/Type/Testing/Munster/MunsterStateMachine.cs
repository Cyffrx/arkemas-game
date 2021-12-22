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
        GD.Print(munsterStates.Count);
        // for (int m = 0; 0 < munsterStates.Count; m++)
        //     munsterStates[m].MSM = this;
        
        ChangeState(munsterStates[0].Name);
    }
}
