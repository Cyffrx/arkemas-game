using Godot;
using System;
using System.Collections.Generic;

public class AttackState : PlayerControlState
{
    Vector2 Direction;
    public int AttackSpeed = 1;
    
    public override void _Ready()
    {
        // timer = GetNode<Timer>("AttackStateTimer");
    }

    public override void OnStart(Dictionary<string, object> message)
    {
        base.OnStart(message);
        
        // timer.Start();
    }

    // check for collision with thing, if it has injurable tag then damage x pts

    public void _on_AttackStateTimer_timeout()
    {
        PCSM.ChangeState("IdleState");
    }
}