using Godot;
using System;

public class PlayerStateReader : Label
{
	public override void _Process(float delta)
	{
		this.Text = (string) Owner.GetNode<Node>("StateMachine").Get("CurrentState");
		//this.Text = (string) Owner.GetNode<AnimationPlayer>("AnimationPlayer").CurrentAnimation;
	}
}
