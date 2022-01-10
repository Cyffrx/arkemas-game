using Godot;
using System;
using System.Collections.Generic;

public class _DefaultLevel : Node
{
	private KinematicBody2D Player;
	private Position2D playerSpawn;
	public Dictionary<string, object> WorldState;
	
    public override void _Ready()
    {
        CanvasModulate canvas = GetChild<CanvasModulate>(0);
        canvas.Visible = true;

		Player = Owner.GetNode("Player") as KinematicBody2D;
		playerSpawn.GlobalPosition = playerSpawn.Position;
    }
	
}
