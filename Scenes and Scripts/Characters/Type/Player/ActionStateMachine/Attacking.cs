using Godot;
using System;
using System.Collections.Generic;

public class Attacking : ActionState
{
    private Area2D damageArea;

    public override void _Ready()
    {
        base._Ready();

        damageArea = Owner.GetNode<Area2D>("DamageArea");
    }

    public override void OnStart(Dictionary<string, object> message)
    {
        base.OnStart(message);

        damageArea.Monitoring = true;
    }

    public override void UpdateState(float _delta)
    {
        base.UpdateState(_delta);

        if (Input.IsActionJustPressed("action_dodge")) ASM.ChangeState("Dodging");
    }

    public override void OnExit(string nextState)
    {
        base.OnExit(nextState);

        damageArea.Monitoring = false;
    }
}
