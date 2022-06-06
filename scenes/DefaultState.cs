using Godot;
using System;
using System.Collections.Generic;

public class DefaultState : Node
{
    // Whether or not state is active in its state machine.
	public bool Active = false;

    // Whethor or not state has gone through update at least once
	private bool OnUpdate_HasFired = false;
	
    // Signal that the state has begun
	[Signal] public delegate void StateStart();

    // Signal that the state has begun its update
	[Signal] public delegate void StateUpdated();

    // Signal that the state has has exited.
	[Signal] public delegate void StateExited();

    // Fires whenever the state is first set in its state machien
	public virtual void OnStart(Dictionary<string, object> message)
	{
		EmitSignal(nameof(StateStart));
		Active = true;
	}

	public virtual void OnUpdate()
	{
		if (!Active)
			return;
		EmitSignal(nameof(StateUpdated));
		OnUpdate_HasFired = true;
	}
    
    
	public virtual void UpdateState(float _delta)
	{
		if (!OnUpdate_HasFired)
			return;
	}

    // Fires whenever state ends. Should contain exit / closing code
	public virtual void OnExit(string nextState)
	{
		if (!Active)
			return;
		EmitSignal(nameof(StateExited));
		Active = false;
		OnUpdate_HasFired = true;
	}

}