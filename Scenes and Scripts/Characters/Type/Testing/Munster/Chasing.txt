using Godot;
using System;
using System.Collections.Generic;

public class Chasing : MunsterState
{

    Vector2 targetPositon;
    private KinematicBody2D kb;
    private RayCast2D targetPointer;
    private float Speed;

    public override void _Ready()
    {
        base._Ready();
        Speed = (float) Owner.GetNode<Node>("Stats").Get("WalkSpeed");
        kb = (KinematicBody2D) Owner;
        targetPointer = GetNode<RayCast2D>("../../TargetPointer");
    }
    public override void OnStart(Dictionary<string, object> message)
    {
        base.OnStart(message);

        targetPositon = (Vector2) message["TargetPosition"];   
    }

    public override void UpdateState(float _delta)
    {
        base.UpdateState(_delta);

        // update target position with player position
        targetPositon = GetNode<KinematicBody2D>("/root/MainScene/Player").Position;
        targetPositon = kb.ToLocal(targetPositon);
        targetPointer.CastTo = targetPositon;

        kb.MoveAndSlide(targetPositon.Normalized() * Speed);
    }
}
