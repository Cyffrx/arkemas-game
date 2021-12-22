using Godot;
using System;

public class GlobalUIControls : Node
{
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        #region system
        if (Input.IsActionJustPressed("pause"))
            GetTree().Paused = true;
        if (Input.IsActionJustPressed("console")) {}
        #endregion
    }
}
