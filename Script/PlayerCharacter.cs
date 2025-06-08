using Godot;
using System;

public partial class PlayerCharacter : CharacterBody2D
{
	public const float Speed = 300.0f;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;


		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("walk_left", "walk_right", "walk_up", "walk_down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity = Vector2.Zero;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
