using Godot;
using System;

public class Acting : ControlState
{
    private Vector2 lastVelocity;
    private KinematicBody2D kb;
    private Area2D interactArea;
    private float MovementSpeed = 300;

    public override void _Ready()
    {
        base._Ready();

        lastVelocity = Vector2.Down;
        interactArea = Owner.GetNode<Area2D>("InteractArea");
        kb = (KinematicBody2D) Owner;
    }

    public override void UpdateState(float _delta)
    {
        base.UpdateState(_delta);

        Vector2 velocity = Vector2.Zero;
        
        #region movement
        if (Input.IsActionPressed("move_up")) velocity.y -= 1;
        if (Input.IsActionPressed("move_down")) velocity.y += 1;
        if (Input.IsActionPressed("move_left")) velocity.x -= 1;
        if (Input.IsActionPressed("move_right")) velocity.x += 1;
        #endregion

        if (Input.IsActionPressed("action_interact"))
        {
            interactArea.Monitoring = true;
            // need to rotate the interact area to be in front of the player's direction
        }
        else interactArea.Monitoring = false;

        kb.MoveAndSlide(velocity.Normalized() * MovementSpeed);
    }

    public override void _Process(float delta)
    {
        base._Process(delta);

        // handle animations?
    }
}
