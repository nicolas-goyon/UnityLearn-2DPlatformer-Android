using ProjectTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Animator Handler", menuName = "Sprite Animator/AnimatorHandler")]
public abstract class AnimatorHandlerSO<T> : IAnimatorHandlerSO where T : Enum
{

    public SerializableDictionary<T, AnimationStateSO> animationStates;
    
    public override AnimationStateSO[] AnimationStateSOArray {
        get {
            return animationStates.Values.ToArray();
        }
    }

}
