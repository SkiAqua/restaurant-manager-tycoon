using Godot;
using System;

public partial class PropModule : Node2D
{
	[Export] public Vector2 Size = Vector2.Zero;

	[Export] public BaseInteraction Interaction;

	[Export] public Node GraphicNode;

	public override void _Ready()
	{
		if (Interaction == null)
		{
			GD.PrintErr("Interactive Porps must have an interaction resource");
		}

	}
	public void Interact()
	{
		Interaction.OnInteraction();
	}
}
