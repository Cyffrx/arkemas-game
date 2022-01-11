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
            if (PSM.Aecarium.Value == PSM.Aecarium.Min)
                Owner.Owner.GetNode<LevelStateMachine>("LevelStateMachine").Call("Reload", PSM.kb.GlobalPosition);
            else 
                Owner.Owner.GetNode<LevelStateMachine>("LevelStateMachine").Call("Reload");

		    GetParent()._Ready();
        }
    }
}
