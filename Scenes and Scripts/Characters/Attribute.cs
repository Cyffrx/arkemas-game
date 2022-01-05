using Godot;

public class Attribute : Node
{
	// max value
	private int _defaultMax;
	private int _max;
	// value
	private int _defaultValue;
	private int _value;
	// min value
	private int _defaultMin;
	private int _min;

	public int Max {get { return _max; } set {_max = value;}}
	public int Value {get {return _value;} set {_value = _tryUpdate(value);}}
	public int Min {get { return _min; } set {_min = value;}}

	private int _tryUpdate (int value) { return (value <= _max && value >= _min) ? value : _value; }

	public Attribute(string name, int maxValue, int minValue, int value)
	{
		this.Name = name;
		this._defaultMax = maxValue;
		this._defaultMin = minValue;
		this._defaultValue = value;

		Reset();
	}

	public void Reset()
	{
		this.Max = this._defaultMax;
		this.Min = this._defaultMin;
		this.Value = this._defaultValue;
	}
}