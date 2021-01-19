using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterCharacter : StateMachineBehaviour
{
    Player Skill;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Skill = animator.gameObject.GetComponent<Player>();

        Skill.Effect("Block", animator.transform.GetChild(0).gameObject, new Vector3(0.2f, 0.2f), 0);
        Skill.Sound.PlayerVoice(Skill.Audio[2]);
        Skill.Sound.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetInteger("Index") == 6)
        {
            animator.SetBool("IsAttack", true);
        }
        animator.SetBool("IsHit", false);

        if(animator.GetInteger("Index") == 20)
        {
            animator.SetInteger("Index", 0);
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
