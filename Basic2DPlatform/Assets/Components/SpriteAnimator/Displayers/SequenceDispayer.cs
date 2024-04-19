using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceDispayer : MonoBehaviour, IDisplayer {

    [SerializeField] private AnimationStateSO state;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteAnimatorHandler spriteAnimatorHandler;
    private int index;
    [SerializeField] private bool isPlaying = false;
    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
    public AnimationStateSO AnimationDisplaying { get => state; }


    public void Start() {
        index = 0;
        if (!TryGetComponent<SpriteAnimatorHandler>(out spriteAnimatorHandler)) {
            Debug.LogError("SpriteAnimatorHandler not found");
            return;
        }

        if (!spriteAnimatorHandler.TryGetComponent<SpriteRenderer>(out spriteRenderer)) {
            Debug.LogError("SpriteRenderer not found");
            return;
        }

        state = spriteAnimatorHandler.GetCurrentAnimationState();
        isPlaying = true;

    }


    public void Update() {
        if (IsPlaying) Display();
    }

    public void Display() {
        if (state == null) return;
        if (state.frames.Length == 0) return;
        if (index >= state.frames.Length) {
            spriteAnimatorHandler.SequenceAnimationEnded();
        }


        spriteRenderer.sprite = state.frames[index];
        index++;
    }

    public void Reset() {
        index = 0;
    }

    public void DestroySelf() {
        Destroy(this);
    }
}
