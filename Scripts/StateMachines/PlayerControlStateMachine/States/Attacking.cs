using Godot;
using System;
using System.Collections.Generic;

public class Attacking : PlayerControlState
{
	public override void OnStart(Dictionary<string, object> message)
	{ base.OnStart(message); if (PlayerInformation.Weapons.Count < 1) PCSM.ChangeState("Idling"); }
	
	public void _on_AnimatedSprite_animation_finished() { if (Active) PCSM.ChangeState("Idling");}
}
