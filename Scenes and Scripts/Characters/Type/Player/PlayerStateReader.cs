using Godot;
using System;

public class PlayerStateReader : Label
{
    public override void _Process(float delta)
    {
        // this.Text = (string) Owner.GetNode<Node>("ActionStateMachine").Get("CurrentState");
		this.Text = (string) Owner.GetNode<AnimationPlayer>("AnimationPlayer").CurrentAnimation;
    }
}
