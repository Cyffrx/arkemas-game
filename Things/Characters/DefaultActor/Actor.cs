using Godot;
using System;

public class Actor : Node
{
	private enum ActionState
	{
		IDLE
	}

    // the actor doesn't care about this only the animation state machine
    // the actor can read its velocity
	private enum DirectionStates
	{
		UP = 0,
		UP_RIGHT = 1,
		RIGHT = 2,
		DOWN_RIGHT = 3,
		DOWN = 4,
		DOWN_LEFT = 5,
		LEFT = 6,
		UP_LEFT = 7
	}

	private ActionState _actionState;
	private Vector2 _velocity;

	//  todo:   figure out how to assign these such that the 
	//          player's stats / equipment persist throughout scenes
	private ActorStat Speed = new ActorStat("Speed", 0, 0, 800);
	private ActorStat Acceleration = new ActorStat("Acceleration", 0, 0, 100);
	private ActorStat iFrames = new ActorStat("Evasion Time", 0, 0, 0);
	private ActorStat Health = new ActorStat("Health", 0, 0, 0);
	private ActorStat Stamina = new ActorStat("Stamina", 0, 0, 0);
	private ActorStat Aecarium = new ActorStat("Aecarium", 0, 0, 0);
    private ActorStat AttackSpeed = new ActorStat("Aecarium", 0, 0, 0);


	public override void _Ready() {}

	public override void _PhysicsProcess(float delta) {}

	public override void _Process(float delta) {}


	private class ActorStat {
		// backers
		private string _Name;
		private double _Maximum;
		private double _Minimum;
		private double _Value;

		// never change
		private readonly double _DefaultMaximum;
		private readonly double _DefaultMinimum;
		private readonly double _DefaultValue;
		
		// properties
		public string Name { get { return _Name; } }
		public double Maximum { get { return _Maximum; } set { this._Maximum = value;} }
		public double Minimum { get { return _Minimum; } set { this._Minimum = value; } }
		public double Value { get { return _Value; } set { this.ChangeValue(value); } }

		private void ChangeValue(double value)
		{
			if (value < this.Minimum)
				this.Value = this.Minimum;
			else if ( value > this.Maximum)
				this.Value = this.Maximum;
			else
				this.Value = value;
		}

		public void Reset() 
		{ 
			this.Value = this._DefaultValue;
			this._Maximum = this._DefaultMaximum;
			this._Minimum = this._DefaultMinimum;
		}

		public ActorStat (string name, double defaultValue, double minimum, double maximum)
		{
			this._Name = name;
			this._DefaultValue = defaultValue;
			this._DefaultMinimum = minimum;
			this._DefaultMaximum = maximum;
		}

	}
}
