using Godot;
using System;
using System.Collections.Generic;

public class _DefaultState : Node
{
	private bool Initialized = false;
	private bool OnUpdateHasFired = false;
	
	[Signal] public delegate void StateStart();
	[Signal] public delegate void StateUpdated();
	[Signal] public delegate void StateExited();

	public virtual void OnStart(Dictionary<string, object> message)
	{
		EmitSignal(nameof(StateStart));
		Initialized = true;
	}

	public virtual void OnUpdate()
	{
		if (!Initialized)
			return;
		EmitSignal(nameof(StateUpdated));
		OnUpdateHasFired = true;
	}

	public virtual void UpdateState(float _delta)
	{
		if (!OnUpdateHasFired)
			return;
	}

	public virtual void OnExit(string nextState)
	{
		if (!Initialized)
			return;
		EmitSignal(nameof(StateExited));
		Initialized = false;
		OnUpdateHasFired = true;
	}

}
