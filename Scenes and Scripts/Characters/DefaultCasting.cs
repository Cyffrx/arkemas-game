using Godot;
using System;

public class Casting : ActorState
{
    public override void UpdateState(float _delta)
    {
        base.UpdateState(_delta);
        ASM.ChangeState("Idling");
    }
}
