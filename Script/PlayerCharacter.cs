using Godot;
using Microsoft.VisualBasic;
using System;

public partial class PlayerCharacter : CharacterBody2D
{
	public const float Speed = 600.0f;


	[Export] Node2D GraphicNode;
	[Export] Sprite2D HoldingItemSprite;
	[Export] Area2D InteractionArea;

	private int? _holdingItemID;
	private string _holdingItemName = "";
	private bool _holdingImportantItem = false;
	public override void _PhysicsProcess(double delta)
	{
		_movementUpdate(delta);
		_interactionUpdate();
		_throwAwayItemUpdate();
	}

	public bool GiveItem(int newItemID, Texture2D newItemTexture, string newItemName)
	{
		if (IsHoldingItem())
			return false;
		
		_holdingItemID = newItemID;
		HoldingItemSprite.Texture = newItemTexture;
		_holdingItemName = newItemName;
		return true;
	}
	public void GiveImportantItem(int newItemID, Texture2D newItemTexture, string newItemName)
	{
		GiveItem(newItemID, newItemTexture, newItemName);
		_holdingImportantItem = true;
	}
	public int? GetHoldingItemID()
	{
		return _holdingItemID;
	}
	public bool IsHoldingItem()
	{
		if (_holdingItemID == null)
			return false;

		return true;
	}
	public bool DropHoldingItem()
	{
		if (_holdingImportantItem)
			return false;

		_holdingItemID = null;
		_holdingItemName = "";
		HoldingItemSprite.Texture = null;

		return true;
	}
	public void SwapHoldingItem(int newItemID, Texture2D newItemTexture, string newItemName)
	{
		if (!DropHoldingItem())
			return;

		GiveItem(newItemID, newItemTexture, newItemName);
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

		Area2D closerArea = overlappingAreas[0];

		if (overlappingAreas.Count > 1)
		{
			foreach (Area2D area in overlappingAreas)
			{
				if (area.GlobalPosition.DistanceTo(GlobalPosition) < closerArea.GlobalPosition.DistanceTo(GlobalPosition))
				{
					closerArea = area;
				}
			}
		}

		foreach (Area2D area in overlappingAreas)
		{
			GD.Print(area.GetParent().Name);
		}
		GD.Print("Closer >" + closerArea.GetParent().Name);

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
