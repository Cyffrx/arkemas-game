using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class LevelStateMachine : _DefaultStateMachine
{
	public Node CurrentLevel;
	public KinematicBody2D Player;

	public override void _Ready()
	{
		base._Ready();

		Player = Owner.GetNode<KinematicBody2D>("Player");
		CurrentLevel = Owner.GetNode<Node>("CurrentLevel");
		
	   	List<LevelState> levelStates = GetChildren().OfType<LevelState>().ToList();

	   	for (int i = 0; i < levelStates.Count; i++)
			levelStates[i].LSM = this;

		ChangeState(levelStates[0].Name);
	}


	public override void ChangeState(string stateName, Dictionary<string, object> message = null)
	{
		base.ChangeState(stateName, message);

		// message is going to have [Level] == [resource path]
		// PackedScene nextLevel_packed = message["NextLevel"] as PackedScene;
		// Node nextLevel = nextLevel_packed.Instance();
		// Owner.AddChild(nextLevel);
		
		// CurrentLevel.QueueFree();
		// CurrentLevel = nextLevel;
	}
}
