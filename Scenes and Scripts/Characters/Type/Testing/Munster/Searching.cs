using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class Searching : MunsterState
{
    [Export] private float WanderSpeed = 50;
    private Vector2 lastDirection = Vector2.Down;
    private KinematicBody2D kb;
    private RandomNumberGenerator rng = new RandomNumberGenerator();
    private List<RayCast2D> radar;
    private RayCast2D targetPointer;

    public override void _Ready()
    {
        base._Ready();        
        kb = (KinematicBody2D) Owner;
        rng.Randomize();
        RandomRotateVector(ref lastDirection);
        radar = GetNode<Node>("../../Radar").GetChildren().OfType<RayCast2D>().ToList();
        targetPointer = GetNode<RayCast2D>("../../TargetPointer");
    }

    public override void OnStart(Dictionary<string, object> message)
    {
        base.OnStart(message);
    }

    public override void UpdateState(float _delta)
    {
        base.UpdateState(_delta);
        OnUpdate();
        RadarBounce();
        kb.MoveAndSlide(WanderSpeed * lastDirection.Normalized());
        TargetCheck();
    }
    private void RandomRotateVector(ref Vector2 vector) { vector = vector.Rotated(rng.RandfRange(-2.0f, 2.0f)); }
    public void _on_Timer_timeout() { RandomRotateVector(ref lastDirection); }

    private void RadarBounce()
    {
        for (int r = 0; r < radar.Count; r++)
        {
            if (radar[r].IsColliding()) lastDirection = -lastDirection;
            radar[r].Rotate(0.1f);
        }
    }

    private void TargetCheck()
    {
        targetPointer.CastTo = lastDirection * 200;
        if (targetPointer.IsColliding())
        {
            Node2D potentialTarget = (Node2D) targetPointer.GetCollider();
            if (potentialTarget.IsInGroup("player") || potentialTarget.IsInGroup("aecarium"))
            MSM.ChangeState("Chasing", new Dictionary<string, object>() 
                {
                    {"TargetPositon", kb.ToLocal(potentialTarget.Position)}
                }
            );
        }
    }
}