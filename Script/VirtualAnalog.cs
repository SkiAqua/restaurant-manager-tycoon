using Godot;
using System;

public partial class VirtualAnalog : TouchScreenButton
{
	private const float DEADZONE = .05f;

	[Export] public Sprite2D AnalogFG;

	private Vector2 AnalogCenter = Vector2.Zero;
	private int _lastTouchIndex;
	public bool AnalogPressing = false;
	public Vector2 Direction = Vector2.Zero;
	public override void _UnhandledInput(InputEvent @event)
	{
		_TestInputEventDrag(@event);
		_TestInputEventTouch(@event);
	}

    public override void _Draw()
    {
		if (IsPressed()) DrawCircle(AnalogCenter, AnalogCenter.X, new Color(255, 0, 0), false, 3);
    }

	public override void _Ready()
	{
		AnalogCenter = TextureNormal.GetSize() / 2;
		AnalogFG.Position = AnalogCenter;
	}
	public override void _PhysicsProcess(double delta)
	{
		if (!AnalogFG.Position.Equals(AnalogCenter) && !AnalogPressing)
		{
			AnalogFG.Position = AnalogFG.Position.MoveToward(AnalogCenter, (float)delta * 2000);
		}

		_PressInputAction();
		QueueRedraw();
	}

	private void _PressInputAction()
	{

		if (Direction.X > DEADZONE)
			Input.ActionPress("walk_right", Direction.X);
		else
			Input.ActionRelease("walk_right");

		if (Direction.X < -DEADZONE)
			Input.ActionPress("walk_left", -Direction.X);
		else
			Input.ActionRelease("walk_left");

		if (Direction.Y > DEADZONE)
			Input.ActionPress("walk_down", Direction.Y);
		else
			Input.ActionRelease("walk_down");

		if (Direction.Y < -DEADZONE)
			Input.ActionPress("walk_up", -Direction.Y);
		else
			Input.ActionRelease("walk_up");
	}
	private void _TestInputEventDrag(InputEvent e)
	{
		if (e is InputEventScreenDrag dragEvent && AnalogPressing && dragEvent.Index == _lastTouchIndex)
		{
			_UpdateAnalogFGPosition(dragEvent.Position);
		}
	}
	private void _TestInputEventTouch(InputEvent e)
	{
		if (e is InputEventScreenTouch touchEvent)
		{
			if (touchEvent.IsReleased() && touchEvent.Index == _lastTouchIndex)
			{
				AnalogPressing = false;
				Direction = Vector2.Zero;
			}
			else if (touchEvent.IsPressed() && new Rect2(Position, TextureNormal.GetSize()).HasPoint(touchEvent.Position))
			{
				_UpdateAnalogFGPosition(touchEvent.Position);
				AnalogPressing = true;
				_lastTouchIndex = touchEvent.Index;
			}
		}
	}

	private void _UpdateAnalogFGPosition(Vector2 newFGPos)
	{
			Vector2 relativeToCenterEventPosition = newFGPos - Position - AnalogCenter;

			Direction = relativeToCenterEventPosition.Normalized();
			float analogLenght = relativeToCenterEventPosition.Length() / (AnalogCenter.X);

			AnalogFG.Position = relativeToCenterEventPosition + AnalogCenter;

			if (analogLenght > 1f)
			{
				AnalogFG.Position = AnalogCenter + Direction * AnalogCenter;
			}

			Direction = Direction * Mathf.Clamp(analogLenght, 0, 1);
	}
	private Vector2 GetCenteredFGPosition()
	{
		return AnalogFG.Position - TextureNormal.GetSize() / 2;
	}
}
