using Godot;
using System;
using System.Collections.Generic;

public class Dead : PlayerState
{
    public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);

        PSM.animationPlayer.Play("die");
	}
    
    public void _on_AnimationPlayer_animation_finished(string animName)
    {
        if (animName.Contains("die"))
        {
            PSM.ChangeState("Idling");
            Owner.GetNode<Node>("../").Call("SpawnPlayer");
        }
    }
}
