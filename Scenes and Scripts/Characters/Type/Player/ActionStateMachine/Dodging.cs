// dodging has three substates
// there's a backwards backstep where you move in the opposite vector of your mouse
// and there's forwardstepping, where you step towards your mouse
// and there's sidestepping, where you step perpendicularish to your mouse

using Godot;
using System;
using System.Collections.Generic;

public class Dodging : DefaultDodging
{
	public override void UpdateState(float _delta)
	{
		base.UpdateState(_delta);

		_attackAfterDodge = Input.IsActionJustPressed("action_attack");
	}
}
