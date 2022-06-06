public class CharacterAttribute
{
    private string name;
    public string Name { get {return name;} }
    
    private float max;
    public float Max { get { return max; } }
    
    private float min;
    public float Min { get { return min; } }

    private float val;
    public float Value 
    { 
        get { return val; } 
        set
        {
            if (value > max) val = max;
            else if (value < min) val = min;
            else val = value;
        }
    }

    public float Starting;
    
    public CharacterAttribute(string _name, float _max, float _min, float _starting)
    {
        name = _name;
        max = _max;
        min = _min;
        Starting = val =_starting;
    }
}