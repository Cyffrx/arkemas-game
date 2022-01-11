using System.Collections.Generic;
using Godot;

public class LevelState : _DefaultState
{
	private PackedScene _playerHusk;
	private string _playerHusk_resource = "res://Scenes and Scripts/Characters/Type/Testing/Bunster/Bunster.tscn";
	public PackedScene Level;
	public LevelStateMachine LSM;
	public string res;

	public override void _Ready()
	{
		base._Ready();
		Level = (PackedScene) ResourceLoader.Load(res);
		_playerHusk = (PackedScene) ResourceLoader.Load(_playerHusk_resource);
	}

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

		Node2D level = (Node2D) Level.Instance();

		if (LSM.CurrentLevel.GetChildCount() > 0)
			LSM.CurrentLevel.GetChild(0).QueueFree();
		
		LSM.CurrentLevel.AddChild(level);
		
		LSM.Player.GlobalPosition = level.GetNode<Position2D>("PlayerSpawn").GlobalPosition;

		// if (message != null) 
		// 	if (message.ContainsKey("PlayerDeathPosition"))
		// 	{
		// 		GD.Print(LSM.CurrentLevel.GetChild(0).GetNode<Node>("Enemies").Name);
		// 		LSM.CurrentLevel.GetChild(0).GetNode<Node>("Enemies/Dynamic").AddChild(_playerHusk.Instance());

		// 		PackedScene newScene = new PackedScene();
		// 		newScene.Pack(LSM.CurrentLevel);
		// 		ResourceSaver.Save(newScene.ResourcePath, newScene);
		// 		Level = newScene;
		// 	}
	}
}
