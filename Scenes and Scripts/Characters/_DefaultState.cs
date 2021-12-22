using Godot;
using System;
using System.Collections.Generic;

public class _DefaultState : Node
{
	public bool Active = false;
	private bool OnUpdateHasFired = false;
	
	[Signal] public delegate void StateStart();
	[Signal] public delegate void StateUpdated();
	[Signal] public delegate void StateExited();

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
		OnUpdateHasFired = true;
	}

	public virtual void UpdateState(float _delta)
	{
		if (!OnUpdateHasFired)
			return;
	}

	public virtual void OnExit(string nextState)
	{
		if (!Active)
			return;
		EmitSignal(nameof(StateExited));
		Active = false;
		OnUpdateHasFired = true;
	}

}
