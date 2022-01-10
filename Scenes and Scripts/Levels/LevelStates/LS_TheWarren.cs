using Godot;
using System;
using System.Collections.Generic;

public class LS_TheWarren : LevelState
{
	private string res = "res://Scenes and Scripts/Levels/List/TheWarren/TheWarren.tscn";

	public override void _Ready()
	{
		base._Ready();
		Level = (PackedScene) ResourceLoader.Load(res);
	}
}
