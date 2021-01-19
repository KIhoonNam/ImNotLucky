using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{
    bool stop;
    Player Attack;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetInteger("Index") == 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 7 / 28f)
            {
                if (!stop)
                {
                    Attack.Effect("Attack_01", animator.transform.GetChild(1).gameObject, new Vector3(0.3f, 0.3f), -40f);
                    stop = true;
                }
            }
        }
        else if(animator.GetInteger("Index") == 2)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 5 / 28f)
            {
                if (!stop)
                {
                    Attack.Effect("Attack02", animator.transform.GetChild(3).gameObject, new Vector3(0.6f, 0.3f), 5.8f);
                    stop = true;
                }
            }
        }
        else if(animator.GetInteger("Index") == 6)
        {
            if (!stop)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 20f / 25f)
                {
                    
                  
                    Attack.Effect("Block2", animator.transform.GetChild(2).gameObject, new Vector3(0.2f, 0.2f), 0);
                    stop = true;
                }
            }
        }
        else if (animator.GetInteger("Index") == 7)
        {
            if (!stop)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f / 5f)
                {

                    Attack.AttackBox.radius = 100f;
                
                    Attack.Effect("Attack04", animator.transform.GetChild(3).gameObject, new Vector3(1.5f, 1f), 0);
                    stop = true;
                }
            }
        }

        if (Attack.anim.GetBool("IsAttack"))
        {
            if (Attack.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                Attack.anim.SetBool("IsAttack", false);
               
                if(animator.GetInteger("Index") != 6)
                    Attack.StartCoroutine(Attack.Clear());
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("Index", 0);
        Attack.Index = 0;
        stop = false;
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
