using Godot;
using System;

[GlobalClass]
public partial class ProcessToInteraction : BaseInteraction
{
    private enum ProcessState { FREE, PROCESSING, FINISHED }

    [Export] public int SmallItemInID;

    [Export] public string SmallItemOutName;
    [Export] public int SmallItemOutID;
    [Export] public Texture2D SmallItemOutTexture;

    [Export] public float ProcessWaitTime;

    private ProcessState _processState = ProcessState.FREE;
    public override void OnInteraction(PlayerCharacter playerTarget)
    {
        switch (_processState)
        {
            case ProcessState.FREE:
                if (playerTarget.GetHoldingItemID() != SmallItemInID)
                    return;

                MyOwner.PlayCrushingTweenAnimation();

                var timerTween = MyOwner.GetTree().CreateTween();
                timerTween.TweenInterval(ProcessWaitTime);
                timerTween.TweenCallback(new Callable(this, "_setStateFinished"));

                playerTarget.DropHoldingItem();
                _processState = ProcessState.PROCESSING;
                break;
            case ProcessState.PROCESSING:
                break;
            case ProcessState.FINISHED:
                if (!playerTarget.GiveItem(SmallItemOutID, SmallItemOutTexture, SmallItemOutName))
                    return;
                MyOwner.PlayCrushingTweenAnimation();
                break;
        }
    }

    private void _setStateFinished()
    {
        _processState = ProcessState.FINISHED;
    }
}
