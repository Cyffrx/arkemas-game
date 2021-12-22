using Godot;
using System;

public class Dodging : ActionState
{
    public override void UpdateState(float _delta)
    {
        base.UpdateState(_delta);

        // below queues attack to player after dodge
        // if (Input.IsActionJustPressed("action_attack")) ASM.ChangeState("Attacking");
    }
}
