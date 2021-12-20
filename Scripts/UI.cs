using Godot;
using System;

public class UI : CanvasLayer
{
    public override void _Ready()
    {
        base._Ready();

        GetNode<Control>("PauseMenu").Visible = false;
        GetNode<Control>("HUD").Visible = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (Input.IsActionJustPressed("pause"))
        GetTree().Paused = !GetTree().Paused;
        GetNode<Control>("PauseMenu").Visible = GetTree().Paused;
        GetNode<Control>("HUD").Visible = !GetTree().Paused;
    }

    public void _on_Unpause_pressed() { GetTree().Paused = !GetTree().Paused; }
    public void _on_Exit_pressed() {GetTree().Quit(); }
}
