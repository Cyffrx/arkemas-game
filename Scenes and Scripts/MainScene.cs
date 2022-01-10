using Godot;

public class MainScene : Node
{
	AudioStreamPlayer musicPlayer;
	KinematicBody2D player;
	public override void _Ready()
    {
    	player = GetNode<KinematicBody2D>("Player");
		musicPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}
}
