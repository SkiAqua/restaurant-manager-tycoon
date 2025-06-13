using Godot;
using System;

[GlobalClass]
public abstract partial class BaseInteraction : Resource
{
	public Area2D InteractionArea { get; set; }

	public abstract void OnInteraction(PlayerCharacter _playerTarget);
}
