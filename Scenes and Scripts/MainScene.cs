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
		SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        player.GlobalPosition = map.GetNode<Position2D>("PlayerSpawn").GlobalPosition;
        player.GetNode<Node>("StateMachine").Call("_Ready");
    }
}
