using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class LoopDisplayer : MonoBehaviour, IDisplayer {
    [SerializeField] private AnimationStateSO state;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int loopIndex;
    private float timeElapsed = 0;
    [SerializeField] private bool isPlaying = false;

    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }
    public AnimationStateSO AnimationDisplaying { get => state; }

    public void Start() {
        loopIndex = 0;
        if (!TryGetComponent<SpriteAnimatorHandler>(out var handler)) {
            Debug.LogError("SpriteAnimatorHandler not found");
            return;
        }

        if (!handler.TryGetComponent<SpriteRenderer>(out spriteRenderer)) {
            Debug.LogError("SpriteRenderer not found");
            return;
        }

        state = handler.GetCurrentAnimationState();
        isPlaying = true;

    }

    public void Update() {
        if (IsPlaying) Display();
    }

    public void Display() {
        timeElapsed += Time.deltaTime;
        if (timeElapsed < 1 / state.framePerSecond) return;
        if (state == null) return;
        if (state.frames.Length == 0) return;

        if (loopIndex >= state.frames.Length) {
            loopIndex = 0;
        }


        spriteRenderer.sprite = state.frames[loopIndex];
        loopIndex++;
        timeElapsed = 0;
    }

    public void DestroySelf() {
        Destroy(this);
    }
}
