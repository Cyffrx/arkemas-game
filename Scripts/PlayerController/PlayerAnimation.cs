using Godot;
using System;

public class PlayerAnimation : AnimatedSprite
{
	CommonActorThings.FacingDirection currentDirection = 0;
	CommonActorThings.FacingDirection lastDirection;

    public void _on_IdleState_StateStart()
	{
		// play idle based on currentDirection
		Play($"idle_{(int)currentDirection}");
		SpeedScale = 2;
	}

	public void _on_MovementState_StateStart()
	{
		// play run based on currentDirection
		Play($"run_{(int)currentDirection}");
		SpeedScale = 6;
	}

	public void _on_MovementState_StateUpdated()
	{
		//	only run if direction changes
		// if ()
		// {
		// 	Play("run_0");
		// 	SpeedScale = 6;
		// }

		if (lastDirection != currentDirection)
		{
			// set direction
		}
	}

	// planning to implement an acceleration thing and this will handle it
	public void _on_MovementState_StateExited()
	{
		// Play($"idle_{currentDirection}");
		// SpeedScale = 2;
	}
}
