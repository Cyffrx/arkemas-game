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

	// current concern is that i think this will 
	// load each level as soon as the game starts
	// which is NOT what i want. need to figure out
	// how to set nodes to not load until instanced 
	// or something like that to get this approach 
	// to work. elsewise i need to figure something
	// else out.
		// right now the level IS the state. if the state was
		// the manager of the level and created the level onStart
		// then I think that would get around my loading fears.
}
