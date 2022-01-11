using Godot;
using System;
using System.Collections.Generic;

public class BunsterDead : BunsterState
{
    Timer bodyDecayTimer;

    public override void _Ready()
    {
        base._Ready();

        bodyDecayTimer = GetChild(0) as Timer;
    }
    public override void OnStart(Dictionary<string, object> message)
    {
        base.OnStart(message);
        
        if (BSM.LastState != Name)
        {
            BSM.sprite.FlipV = true;
            bodyDecayTimer.Start();
        }
    }

    public void _on_BodyDecayTimer_timeout() { Owner.QueueFree(); }
}
