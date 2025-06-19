using Godot;
using System;

[GlobalClass]
public partial class PropModule : Node2D
{
	private Tween _crushTween;
	private Vector2 _crushTweenInitialPosition;
	private Vector2 _crushTweenInitialScale;

	public enum Direction { UP, DOWN, LEFT, RIGHT }

	[Export] public Vector2 Size = Vector2.Zero;

	[Export] public BaseInteraction Interaction;
	[Export] public Direction StartingDirection;

	[Export] public Node2D UpGraphicNode;
	[Export] public Node2D DownGraphicNode;
	[Export] public Node2D LeftGraphicNode;
	[Export] public Node2D RightGraphicNode;

	public Node2D CurrentGraphicNode;

	public Direction direction;
	public override void _Ready()
	{
		if (Interaction != null)
		{
			Interaction.MyOwner = this;
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

		CurrentGraphicNode = visibleNode;
		CurrentGraphicNode.Visible = true;
		
		_crushTweenInitialPosition = CurrentGraphicNode.Position;
		_crushTweenInitialScale = CurrentGraphicNode.Scale;
	}

	public void PlayCrushingTweenAnimation(float crushForce = .3f, float duration = .25f)
	{
		if (_crushTween != null)
		{
			_crushTween.Kill();
		}

		_crushTween = CreateTween();
		
		CurrentGraphicNode.Scale = _crushTweenInitialScale * new Vector2(1, 1-crushForce);
		CurrentGraphicNode.Position = _crushTweenInitialPosition * new Vector2(1, 1+crushForce*1.8f);

		_crushTween.SetParallel();
		_crushTween.TweenProperty(CurrentGraphicNode, "scale", _crushTweenInitialScale, duration);
		_crushTween.TweenProperty(CurrentGraphicNode, "position", _crushTweenInitialPosition, duration);
	}
}
