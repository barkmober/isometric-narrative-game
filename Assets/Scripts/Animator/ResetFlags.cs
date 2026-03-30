using UnityEngine;

namespace SA
{
    public class ResetFlags : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterManager character = animator.gameObject.GetComponent<CharacterManager>();

            character.isPerformingAction = false;
            character.applyRootMotion = false;

            character.canMove = true;
            character.canRotate = true;
        }
    }
}