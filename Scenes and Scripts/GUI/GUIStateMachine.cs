using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class GUIStateMachine : _DefaultStateMachine
{
    List<_GUIState> menuStates;

    public override void _Ready()
    {
        base._Ready();

        menuStates = GetChildren().OfType<_GUIState>().ToList();

        for (int m = 0; m < menuStates.Count; m++)
            menuStates[m].GSM = this;
        
        ChangeState(menuStates[0].Name);
    }
}