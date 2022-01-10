using System.Collections.Generic;
using Godot;

public class LevelState : _DefaultState
{
	public PackedScene Level;
    public LevelStateMachine LSM;

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		LSM.CurrentLevel = (Node) Level.Instance();
		GD.Print("Changing level");
	}
}