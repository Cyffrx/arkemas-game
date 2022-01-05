using Godot;
using System;

public class MainScene : Node
{
	KinematicBody2D player;
	Node2D map;

    public override void _Ready()
    {
        map = GetNode<Node2D>("TheWarren");
		player = GetNode<KinematicBody2D>("Player");
		player.GlobalPosition = map.GetNode<Position2D>("PlayerSpawn").GlobalPosition;
    }
}
