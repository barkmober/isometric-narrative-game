using UnityEngine;

namespace SA
{
    public class SetIsPerformingAction : StateMachineBehaviour
    {
        [SerializeField] bool canMove = false;
        [SerializeField] bool canRotate = false;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CharacterManager character = animator.gameObject.GetComponent<CharacterManager>();

            character.isPerformingAction = true;
            character.applyRootMotion = true;

            character.canMove = canMove;
            character.canRotate = canRotate;
        }
    }
}