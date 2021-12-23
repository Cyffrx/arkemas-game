using Godot;
using System;
using System.Collections.Generic;

public class MainMenu : _GUIState
{
    public override void OnStart(Dictionary<string, object> message)
    {
        base.OnStart(message);
        Owner.GetNode<Control>("Main Menu").Visible = true;
        GetTree().Paused = true;
    }

    public override void OnExit(string nextState)
    {
        base.OnExit(nextState);
        Owner.GetNode<Control>("Main Menu").Visible = false;
    }

    
    public void _on_Continue_pressed() { GSM.ChangeState("HUD"); }

    public void _on_Exit_pressed() { GetTree().Quit(); }
}