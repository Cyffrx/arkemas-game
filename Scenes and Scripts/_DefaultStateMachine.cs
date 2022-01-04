using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class _DefaultStateMachine : Node
{
	[Signal] public delegate void PreStart();
	[Signal] public delegate void PostStart();
	[Signal] public delegate void PreExit();
	[Signal] public delegate void PostExit();

	public List<_DefaultState> States;

	public string CurrentState;
	public string LastState;

	protected _DefaultState state = null;

	public override void _Ready()
	{
		base._Ready();

		States = GetChildren().OfType<_DefaultState>().ToList();
	}

	private void SetState(_DefaultState _state, Dictionary<string, object> message)
	{
		if (_state == null)
			return;
		if (state != null)
		{
			EmitSignal(nameof(PreExit));
			state.OnExit(_state.GetType().ToString());
			EmitSignal(nameof(PostExit));
		}
		

		LastState = CurrentState;
		CurrentState = _state.GetType().ToString();

		state = _state;
		EmitSignal(nameof(PreStart));
		state.OnStart(message);
		EmitSignal(nameof(PostStart));
		state.OnUpdate();
	}

	public void ChangeState(string stateName, Dictionary<string, object> message = null)
	{
		for (int idx = 0; idx < States.Count; idx++)
			if (stateName == States[idx].GetType().ToString())
			{
				SetState(States[idx], message);
				return;
			}
	}

	public override void _PhysicsProcess(float delta)
	{
		if (state == null) return;
		state.UpdateState(delta);
	}
}
