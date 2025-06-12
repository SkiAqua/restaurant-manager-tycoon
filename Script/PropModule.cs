using Godot;
using System;

[GlobalClass]
public partial class PropModule : Node2D
{
	public enum Direction { UP, DOWN, LEFT, RIGHT }

	[Export] public Vector2 Size = Vector2.Zero;

	[Export] public BaseInteraction Interaction;
	[Export] public Direction StartingDirection;

	[Export] public Node2D UpGraphicNode;
	[Export] public Node2D DownGraphicNode;
	[Export] public Node2D LeftGraphicNode;
	[Export] public Node2D RightGraphicNode;

	public Direction direction;
	public override void _Ready()
	{
		if (Interaction == null)
		{
			GD.PrintErr("Interactive Props must have an interaction resource");
		}

		SetModuleRotation(StartingDirection);
	}
	public void Interact(PlayerCharacter player)
	{
		Interaction.OnInteraction(player);
	}

	public void SetModuleRotation(Direction newDir)
	{
		Node2D[] dirNodes = [UpGraphicNode, DownGraphicNode, LeftGraphicNode, RightGraphicNode];
		Node2D visibleNode = dirNodes[0];
		for (int i = 0; i < 4; i++)
		{
			dirNodes[i].Visible = false;

			if (i == (int)newDir)
				visibleNode = dirNodes[i];
		}

		visibleNode.Visible = true;
	}
}
