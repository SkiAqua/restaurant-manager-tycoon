using Godot;
using Microsoft.VisualBasic;
using System;

[GlobalClass]
public partial class DispenserInteraction : BaseInteraction
{
    [Export] public string SmallItemName;
    [Export] public int SmallItemID;
    [Export] public Texture2D SmallItemTexture;

    public override void OnInteraction(PlayerCharacter _playerTarget)
    {
        if (!_playerTarget.GiveItem(SmallItemID, SmallItemTexture, SmallItemName))
            return;
        MyOwner.PlayCrushingTweenAnimation();
    }
}
