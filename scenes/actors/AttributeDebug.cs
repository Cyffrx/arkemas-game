using Godot;
using System.Collections.Generic;
using System.Linq;

public class AttributeDebug : Control
{
	PlayerStateMachine PSM;
	GridContainer gc;
	
	// easiest way to set this up is making our own dictonary to access items in GC by name, or something akin
	public override void _Ready()
	{
		gc = GetNode<GridContainer>("Panel/ScrollContainer/GridContainer");
		PSM = GetParent().GetParent().GetChild(0) as PlayerStateMachine;
		
		List<CharacterAttribute> Attributes = PSM.CharacterAttributes.Values.ToList();

		foreach (CharacterAttribute att in Attributes)
		{
			Label attLabel = new Label
			{
				Text = att.Name,
			};
			Label attCurrent = new Label
			{
				Text = att.Value.ToString(),
			};
			
			LineEdit attSet = new LineEdit();
			attSet.PlaceholderText = att.Name;
			attSet.Connect("text_changed", this, 
				nameof(_debugAttribute_text_changed), new Godot.Collections.Array(new object[] {attSet}));
			
			gc.AddChild(attLabel);
			gc.AddChild(attCurrent);
			gc.AddChild(attSet);
		}
	}
	
	public override void _Process(float _delta)
	{
	}

	// the reason i had so much trouble with this was because a signal is looking for its default argumnets (string text)
	// before it's looking for the additional ones i provided!
	public void _debugAttribute_text_changed(string text, LineEdit attSet)
	{
		if ( float.TryParse(text, out float val))
		PSM.CharacterAttributes[attSet.PlaceholderText].Value = val;
	}
}
