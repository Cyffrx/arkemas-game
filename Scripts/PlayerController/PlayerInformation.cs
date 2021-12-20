using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public class PlayerInformation : KinematicBody2D
{
    #region stats
        #region player stats
        public static float MovementSpeed = 400;
        public static float DodgeSpeed = 400;
        public static float AttackLungeSpeed = 200;
        #endregion

        #region spell stats
        private bool RecallSpellSet = false;
        private int PulseResolution = 8;
        #endregion
    #endregion

    #region inventory lists
    public static List<string> Items = new List<string>();
    public static List<string> Weapons = new List<string>();
    public static List<string> Spells = new List<string>();
    #endregion
   

    private PackedScene PS_PulseBody;
    
    public override void _Ready()
    {
        base._Ready();

        PS_PulseBody = (PackedScene) ResourceLoader.Load("res://Scenes/Things/Spells/PulseBody.tscn");
    }

    public void _on_InteractionRadius_area_entered(Area2D area)
    {
        if (area.IsInGroup("player_pickup"))
        {
            if (area.IsInGroup("player_weapon"))
            {
                GD.Print($"Added {area.Name} to weapons.");
                Weapons.Add(area.Name);
            }
            else if (area.IsInGroup("player_spell"))
            {
                GD.Print($"Added {area.Name} to spells.");
                Spells.Add(area.Name);
            }
            else
            {
                GD.Print($"Added {area.Name} to items.");
                Items.Add(area.Name);
            }

            area.QueueFree();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);

        if (Input.IsActionJustPressed("castSpell_recall") && Spells.Contains("RecallSpell"))
        {
            if (!RecallSpellSet) CastSpell_Mark();
            else CastSpell_Recall(); 
        }

        if (Input.IsActionJustPressed("castSpell_pulse") && Spells.Contains("PulseSpell"))
        {
            CastSpell_Pulse();
        }
    }

    #region pulse spell
    public static Dictionary<int, Vector2> pulseDirectionMapper = new Dictionary<int, Vector2>()
	{	
		{0,new Vector2(0,1)},
		{1,new Vector2(1,1)},
		{2,new Vector2(1,0)},
		{3,new Vector2(1,-1)},
		{4,new Vector2(0,-1)},
		{5,new Vector2(-1,-1)},
		{6,new Vector2(-1,0)},
		{7,new Vector2(-1,1)},	
	};

    private void CastSpell_Pulse()
    {
        for (int i = 0; i < PulseResolution; i++)
        {
            PulseBody pulse = (PulseBody) PS_PulseBody.Instance();
            pulse.Visible = true;
            pulse.Position = this.Position + pulseDirectionMapper[i].Normalized() * 50;
            pulse.Velocity = pulseDirectionMapper[i].Normalized();

            GetNode<Node>("Spells/Pulse").AddChild(pulse);
        }
    }
    #endregion

    #region mark and recall spell
    public void _on_RecallTimer_timeout() { CastSpell_Recall(); }

    private void CastSpell_Mark()
    {
        GD.Print("Set Mark at " + this.Position);

        GetNode<Position2D>("Spells/MarkAndRecall/RecallMark").Position = this.Position;
        GetNode<Timer>("Spells/MarkAndRecall/RecallTimer").Start();
        RecallSpellSet = true;
        
    }

    private void CastSpell_Recall()
    {
        GD.Print("Recalled from  " + this.Position);
        
        this.Position = GetNode<Position2D>("Spells/MarkAndRecall/RecallMark").Position;
        GetNode<Timer>("Spells/MarkAndRecall/RecallTimer").Stop();
        RecallSpellSet = false;
    }
    #endregion
}