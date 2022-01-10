using Godot;

public class MainScene : Node
{
	AudioStreamPlayer musicPlayer;
	public override void _Ready()
    {
    	musicPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}
}
