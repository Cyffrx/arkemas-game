using Godot;
using System;

public class PlayerScript3D : KinematicBody
{
    private const int SPEED = 10;
    
    Spatial body;
    AnimationPlayer body_ap;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        body = this.GetNode<Spatial>("body");
        body_ap = body.GetChild(4) as AnimationPlayer;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(float delta)
    {
        var rotate = Vector3.Zero;
        if (Input.IsKeyPressed((int) KeyList.Q))
            this.Rotate(Vector3.Up, .1f);
        if (Input.IsKeyPressed((int) KeyList.E))
            this.Rotate(Vector3.Up, -.1f);

        var velocity = Vector3.Zero;
        
        if (Input.IsActionPressed("move_right"))
            velocity.x -= 1f;
        if (Input.IsActionPressed("move_left"))
            velocity.x += 1f;
        if (Input.IsActionPressed("move_up"))
            velocity.z += 1f;
        if (Input.IsActionPressed("move_down"))
            velocity.z -= 1f;

        if (velocity != Vector3.Zero)
        {
            velocity = velocity.Normalized();
            body_ap.Play("PlaceholderRun");
        }
        else body_ap.Stop();

        velocity.x *= SPEED;
        velocity.z *= SPEED;

        velocity = MoveAndSlide(velocity);
    }
}
