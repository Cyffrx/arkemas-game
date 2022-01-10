using Godot;
using System;
using System.Collections.Generic;

public class LS_TitleScreen : LevelState
{
	// serves to display the main menu ui
	public void _on_Continue_pressed()
	{ LSM.ChangeState("TheWarren"); }
}
