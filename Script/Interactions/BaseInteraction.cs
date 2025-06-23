using Godot;
using System;

[GlobalClass]
public abstract partial class BaseInteraction : Resource
{
	public PropModule MyOwner;
	public abstract void OnInteraction(PlayerCharacter _playerTarget);
}
