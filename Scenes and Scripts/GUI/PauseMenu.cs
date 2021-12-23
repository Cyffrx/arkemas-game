using Godot;
using System;
using System.Collections.Generic;

public class PauseMenu : _GUIState
{
    public override void OnStart(Dictionary<string, object> message)
    {
        base.OnStart(message);
        Owner.GetNode<Control>("Pause Menu").Visible = true;
        GetTree().Paused = true;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("pause")) GSM.ChangeState("HUD");
    }

    public override void OnExit(string nextState)
    {
        base.OnExit(nextState);
        Owner.GetNode<Control>("Pause Menu").Visible = false;
    }

    public void _on_Resume_pressed() { GD.Print("Resume pressed"); GSM.ChangeState("HUD"); }

    public void _on_Main_Menu_pressed() { GSM.ChangeState("MainMenu"); }

    public void _on_Exit_pressed() { GetTree().Quit(); }
}