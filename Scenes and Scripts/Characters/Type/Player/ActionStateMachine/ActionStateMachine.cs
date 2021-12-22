using Godot;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ActionStateMachine : _DefaultStateMachine
{
	public AnimationPlayer animPlayer;
    public override void _Ready()
    {
        base._Ready();
        
		animPlayer = Owner.GetNode<AnimationPlayer>("AnimationPlayer");
        List<ActionState> actionStates = this.GetChildren().OfType<ActionState>().ToList();

        for (int i = 0; i < actionStates.Count; i++) actionStates[i].ASM = this;

        ChangeState(actionStates[0].Name);
    }
}
