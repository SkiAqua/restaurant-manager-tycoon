using Godot;
using Microsoft.VisualBasic;
using System;

public partial class PlayerCharacter : CharacterBody2D
{
	private int? _holdingItemID;
	private String? _holdingItemName;

	public const float Speed = 600.0f;


	[Export] Node2D GraphicNode;
	[Export] Sprite2D HoldingItemSprite;
	[Export] Area2D InteractionArea;

	public override void _PhysicsProcess(double delta)
	{
		_movementUpdate(delta);
		_interactionUpdate();
		_throwAwayItemUpdate();
	}

	public void GiveItem(int newItemID, Texture2D newItemTexture, String newItemName)
	{
		if (!IsHoldingItem())
		{
			_holdingItemID = newItemID;
			HoldingItemSprite.Texture = newItemTexture;
			_holdingItemName = newItemName;
		}
	}

	public bool IsHoldingItem()
	{
		if (_holdingItemID.Equals(null) || HoldingItemSprite.Texture == null)
			return false;

		return true;
	}

	private void _movementUpdate(double delta)
	{

		Vector2 direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
		if (direction != Vector2.Zero)
		{
			Velocity = direction * Speed;
		}
		else
		{
			Velocity = Vector2.Zero;
		}

		MoveAndSlide();
	}

	private void _interactionUpdate()
	{
		if (!Input.IsActionJustPressed("action_perform") || !InteractionArea.HasOverlappingAreas())
			return;

		var overlappingAreas = InteractionArea.GetOverlappingAreas();

		Area2D closerArea =overlappingAreas[0];

		if (overlappingAreas.Count > 1)
		{
			foreach (Area2D area in overlappingAreas)
			{
				if (area.Position.DistanceTo(Position) < closerArea.Position.DistanceTo(Position))
				{
					closerArea = area;
				}
			}
		}

		GD.Print(closerArea.GetOwner().Name);
		closerArea.Owner.Call("Interact", this);
	}

	private void _throwAwayItemUpdate()
	{
		if (!IsHoldingItem())
			return;
		if (Input.IsActionJustPressed("action_cancel"))
		{
			_holdingItemID = null;
			HoldingItemSprite.Texture = null;
		}
	}
}
