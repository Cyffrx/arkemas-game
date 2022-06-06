using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class StateMachine : Node
{
	// signal before starting a state
	[Signal] public delegate void PreStart();

	// signal after starting a state
	[Signal] public delegate void PostStart();
	// signal before exiting state
	[Signal] public delegate void PreExit();
	// signal after exiting state
	[Signal] public delegate void PostExit();

	// list of states
	public List<DefaultState> States;

	// current state
	public string CurrentState;
	// last state
	public string LastState;

	protected DefaultState state = null;

	public override void _Ready()
	{
		base._Ready();

		States = GetChildren().OfType<DefaultState>().ToList();
	}

	// set the current state with optional arguments
	private void SetState(DefaultState _state, Dictionary<string, object> message)
	{
		if (_state == null)
			return;
		if (state != null)
		{
			EmitSignal(nameof(PreExit));
			state.OnExit(_state.GetType().ToString());
			EmitSignal(nameof(PostExit));
		}
		
		EmitSignal(nameof(PreStart));

		LastState = CurrentState;
		state = _state;
		CurrentState = _state.GetType().ToString();
		
		state.OnStart(message);
		EmitSignal(nameof(PostStart));

		state.OnUpdate();
	}

	public virtual void ChangeState(string stateName, Dictionary<string, object> message = null)
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