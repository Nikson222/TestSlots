using System;
using UnityEngine;

public class AnimationStateBehaviour : StateMachineBehaviour
{
    public event Action OnStateExitEvent;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OnStateExitEvent?.Invoke();
    }
}