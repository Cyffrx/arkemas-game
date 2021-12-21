using Godot;
using System;

public class PulseBody : KinematicBody2D
{
    public Vector2 Velocity;
    public float Speed = 500;
    public float Lifetime = 3;

    private Timer timer;

    public override void _Ready()
    {
        base._Ready();

        timer = GetNode<Timer>("Timer");
        timer.WaitTime = Lifetime;
        timer.Start();
    }
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
		MoveAndSlide(Velocity * Speed);
    }

    public void _on_Timer_timeout() { QueueFree(); }

    public void _on_Area2D_body_entered(Node body) { if (!(body.IsInGroup("player") || body.IsInGroup("pulse_body"))) _on_Timer_timeout(); }
}
