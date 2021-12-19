using Godot;
using System;
using System.Collections.Generic;

public class Attacking : PlayerControlState
{
	public override void UpdateState(float _delta)
	{
		// right now this isn't perfect as you can't attack without moving but it'll do for now
		GetOwner<KinematicBody2D>().MoveAndSlide(PCSM.LastDirection.Normalized() * (PlayerInformation.MovementSpeed + PlayerInformation.AttackLungeSpeed));
	}
	
	public void _on_AnimatedSprite_animation_finished() { if (Active) PCSM.ChangeState("Idling");}
}
