using Godot;
using System;
using System.Collections.Generic;

public class HUD : _GUIState
{
    public override void OnStart(Dictionary<string, object> message)
    {
        base.OnStart(message);
        Owner.GetNode<Control>("HUD").Visible = true;
        GetTree().Paused = false;
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        if (Input.IsActionJustPressed("pause")) GSM.ChangeState("PauseMenu");
    }

    public override void OnExit(string nextState)
    {
        base.OnExit(nextState);
        Owner.GetNode<Control>("HUD").Visible = false;
    }
}