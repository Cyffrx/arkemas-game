using Godot;
using System;

public class Default : ActionState
{
    public override void UpdateState(float _delta)
    {
        base.UpdateState(_delta);
        
        #region actions
        if (Input.IsActionJustPressed("action_interact")) {} // ?
        if (Input.IsActionJustPressed("action_attack")) ASM.ChangeState("Attacking");
        if (Input.IsActionJustPressed("action_dodge")) ASM.ChangeState("Dodging");
        #endregion

        #region casting
        // these state changes should probably carry a reference to what spell they're casting too
        if (Input.IsActionJustPressed("cast_markAndRecall")) ASM.ChangeState("Casting");
        if (Input.IsActionJustPressed("cast_pulse")) ASM.ChangeState("Casting");
        if (Input.IsActionJustPressed("cast_offensive")) ASM.ChangeState("Casting");
        if (Input.IsActionJustPressed("cast_defensive")) ASM.ChangeState("Casting");
        #endregion
    }
       
}
