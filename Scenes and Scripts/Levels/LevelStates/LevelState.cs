using System.Collections.Generic;
using Godot;

public class LevelState : _DefaultState
{
	public PackedScene Level;
	public LevelStateMachine LSM;
	public string res;

	public override void _Ready()
	{
		base._Ready();
		Level = (PackedScene) ResourceLoader.Load(res);
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		Node2D level = (Node2D) Level.Instance();

		if (LSM.CurrentLevel.GetChildCount() > 0)
			LSM.CurrentLevel.GetChild(0).QueueFree();
		
		LSM.CurrentLevel.AddChild(level);
		
		LSM.Player.GlobalPosition = level.GetNode<Position2D>("PlayerSpawn").GlobalPosition;
	}
}
