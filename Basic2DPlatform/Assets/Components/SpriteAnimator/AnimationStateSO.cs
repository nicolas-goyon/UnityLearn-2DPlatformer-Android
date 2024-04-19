using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animation State", menuName = "Sprite Animator/AnimationState")]
public class AnimationStateSO : ScriptableObject
{
    public string animationName;
    public Sprite[] frames;
    public float framePerSecond = 10;
    public AnimationType animationType = AnimationType.Sequence;
    public bool flipX = false;
    public bool flipY = false;
    public bool flipToDirection = true; // If true, the sprite will flip to the direction of the velocity

    public enum AnimationType {
        Sequence,
        Loop
    }
    
}
