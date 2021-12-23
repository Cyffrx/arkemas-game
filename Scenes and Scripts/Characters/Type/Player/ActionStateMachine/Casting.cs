using Godot;
using System;

public class Casting : ActionState
{
    public override void UpdateState(float _delta)
    {
        base.UpdateState(_delta);
        ASM.ChangeState("Default");
		
        // if (    Input.IsActionPressed("move_up")
        //     ||  Input.IsActionPressed("move_down")
        //     ||  Input.IsActionPressed("move_left")
        //     ||  Input.IsActionPressed("move_right")
        // ) ASM.ChangeState("Idle");

        // if (Input.IsActionJustPressed("action_dodge")) ASM.ChangeState("Dodging");
    }
}
