using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimatorHandler: MonoBehaviour 
{
    [SerializeField] private IAnimatorHandlerSO animatorHandler;
    private IDisplayer displayer;


    // ****** Mono Behaviour ******

    private void Awake() {
        CleanDisplayer();

        AnimationStateSO[] animationStates = animatorHandler.AnimationStateSOArray;

        if (animationStates == null || animationStates.Length == 0) {
            Debug.LogError("No Animation States found");
            return;
        }

        if (animatorHandler.idleAnimation == null) {
            Debug.LogError("Idle Animation not found");
            return;
        }
    }


    private void Start() {
        CleanDisplayer();
        SetDisplayer();
    }

    private void OnDestroy() {
        CleanDisplayer();
    }

    private void Update() {
        if (animatorHandler.currentAnimation == null) {
            animatorHandler.currentAnimation = animatorHandler.idleAnimation;
            ChangeAnimation(animatorHandler.idleAnimation);
        }
        if (displayer == null) SetDisplayer();

        if (displayer.AnimationDisplaying != animatorHandler.currentAnimation) {
            ChangeAnimation(animatorHandler.currentAnimation);
            Debug.Log("Animation changed");
        }



        // Shoudn't be implemented : if the sequence is finished, displayer already changes the animation
        //if (displayer.AnimationDisplaying.animationType == AnimationStateSO.AnimationType.Sequence) {
        //    if (displayer.IsPlaying) return;
        //    ChangeAnimation(animatorHandler.IdleAnimation);
        //}

        
    }

    // ****** Gizmos ******
    private bool isChanged = false;
    private void OnValidate() {
        isChanged = true;
    }

    private void OnDrawGizmos() {

        if (!isChanged) return;
        if (!TryGetComponent<SpriteRenderer>(out var renderer)) {
            Debug.LogError("SpriteRenderer not found");
            return;
        }

        AnimationStateSO[] animationStates = animatorHandler.AnimationStateSOArray;

        if (animationStates == null || animationStates.Length == 0) return;
        if (renderer.sprite != null && animatorHandler.idleAnimation == null) return;

        renderer.sprite = animatorHandler.idleAnimation.frames[0];

    }


    // ****** Methods ******

    private void ChangeAnimation(AnimationStateSO newAnimation) {
        CleanDisplayer();
        if (newAnimation == null) { 
            Debug.Log("Animation not found");
            return; 
        }
        if (newAnimation == displayer.AnimationDisplaying) {
            Debug.Log("Animation already playing");
            return;
        }
        SetDisplayer();
    }

    public void SequenceAnimationEnded() {
        ChangeAnimation(animatorHandler.idleAnimation);
    }

    private void SetDisplayer() {
        switch (animatorHandler.currentAnimation.animationType) {
            case AnimationStateSO.AnimationType.Loop:
                LoopDisplayer();
                break;
            case AnimationStateSO.AnimationType.Sequence:
                SequenceDisplayer();
                break;
        }
    }


    private void LoopDisplayer() {
        displayer = this.AddComponent<LoopDisplayer>();
    }

    private void SequenceDisplayer() {
        displayer = this.AddComponent<SequenceDispayer>();
    }

    public AnimationStateSO GetCurrentAnimationState() {
        return animatorHandler.currentAnimation;
    }

    private void CleanDisplayer() {
        displayer?.DestroySelf();

        IDisplayer[] displayers = GetComponents<IDisplayer>();
        foreach (IDisplayer displayer in displayers) {
            displayer.DestroySelf();
        }

        LoopDisplayer[] loopDisplayers = GetComponents<LoopDisplayer>();
        SequenceDispayer[] seqDisplayers = GetComponents<SequenceDispayer>();

        foreach (LoopDisplayer displayer in loopDisplayers) {
            displayer.DestroySelf();
        }

        foreach (SequenceDispayer displayer in seqDisplayers) {
            displayer.DestroySelf();
        }

    }




}
