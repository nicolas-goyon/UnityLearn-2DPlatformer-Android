using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAnimatorHandlerSO : ScriptableObject {
    public abstract AnimationStateSO[] AnimationStateSOArray { get; }

    public AnimationStateSO idleAnimation;
    public AnimationStateSO currentAnimation;


}
