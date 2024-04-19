using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private PlayerAnimatorSO animatorHandler;
    [SerializeField] private PlayerMovements playerMovements;
    // Start is called before the first frame update
    void Start() {
        Debug.Log("PlayerVisuals is ready");
    }

    private bool wasMoving = false;

    // Update is called once per frame
    void Update() {
        if (playerMovements.IsMoving && !wasMoving) {
            animatorHandler.currentAnimation = animatorHandler.animationStates[PlayerAnimationsTypes.Run];
            wasMoving = true;
        }
        else if (!playerMovements.IsMoving && wasMoving) {
            animatorHandler.currentAnimation = animatorHandler.animationStates[PlayerAnimationsTypes.Idle];
            wasMoving = false;
        }
    }
}
