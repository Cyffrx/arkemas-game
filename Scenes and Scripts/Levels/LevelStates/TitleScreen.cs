using Godot;
using System;
using System.Collections.Generic;

public class TitleScreen : LevelState
{
	public override void _Ready()
	{
		res = "res://Scenes and Scripts/Levels/List/TitleScreen/TitleScreen.tscn";
		base._Ready();
	}

	public void _on_Continue_pressed()
	{ LSM.ChangeState("TheWarren"); }
}
