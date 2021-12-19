using Godot;
using System;

public class Dodging : PlayerControlState
{
    public override void UpdateState(float _delta)
    { 
        GetOwner<KinematicBody2D>().MoveAndSlide(PCSM.LastDirection.Normalized() * (PlayerInformation.MovementSpeed + PlayerInformation.DodgeSpeed)); OnUpdate();
    }
    
    public void _on_AnimatedSprite_animation_finished() { if (Active) PCSM.ChangeState("Idling");}
}