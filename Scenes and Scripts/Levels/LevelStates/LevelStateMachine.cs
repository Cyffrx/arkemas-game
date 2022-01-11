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

	public void Reload()
	{
		ChangeState(CurrentState);
	}

	public void Reload(Vector2 deathPosition)
	{
		ChangeState(CurrentState, new Dictionary<string, object> {{"PlayerDeathPosition", deathPosition}});
	}
}
