using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState : StateMachineBehaviour
{
    public static  event SkillStateHandler SkillS;
    bool stop;
     Player Skill;
    MiniGolem Gol;
    Skeleton_Enemy Skell;
    // OnStateE
    //nter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Skill = animator.gameObject.GetComponent<Player>();
        Gol = animator.gameObject.GetComponent<MiniGolem>();
        Skell = animator.gameObject.GetComponent<Skeleton_Enemy>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetParameter(3).name == "Index")
        {
            if (animator.GetInteger("Index") == 5)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 38f / 119f)
                {
                    if (!stop)
                    {
                        Skill.Effect("Idle02", animator.transform.GetChild(2).gameObject, new Vector3(0.3f, 0.3f), 0);
                        stop = true;
                    }
                }
            }
            if (animator.GetInteger("Index") == 8)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f / 4f)
                {
                    if (!stop)
                    {
                        Skill.Effect("Skill02", animator.transform.GetChild(2).gameObject, new Vector3(0.3f, 0.3f), 0);
                        Skill.Effect("Lose", animator.transform.GetChild(2).gameObject, new Vector3(0.3f, 0.3f), 0);
                        stop = true;
                    }
                }
            }
            if (animator.GetInteger("Index") == 9)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f / 5f)
                {
                    if (!stop)
                    {
                        SkillS();
                        //Skill.Effect("Attack03", animator.transform.GetChild(2).gameObject, new Vector3(0.3f, 0.3f), 0);
                        stop = true;
                    }
                }
            }
            if (animator.GetInteger("Index") == 3)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f / 4f)
                {
                    if (!stop)
                    {
                        
                        Skill.Effect("Skill02_Start", animator.transform.GetChild(2).gameObject, new Vector3(0.3f, 0.3f), 0);
                        stop = true;
                    }
                }
            }

            if (animator.GetInteger("Index") == 12)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 41f / 100f)
                {
                    if (!stop)
                    {

                        Skill.Effect("Lose", animator.transform.GetChild(2).gameObject, new Vector3(0.3f, 0.3f), 0);

                        stop = true;
                    }
                }
            }
        }
        if (animator.parameterCount >= 5)
        {
            if (animator.GetParameter(4).name == "IsSkill")
            {
                if (animator.GetBool("IsSkill"))
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Victory"))
                    {
                        if (!stop)
                        {

                            Gol.Effect("Idle02", animator.transform.GetChild(1).gameObject, 1f);

                            stop = true;
                        }
                    }
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
                    {
                        if (!stop)
                        {
                            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f / 4f)
                            {
                                Skell.Effect("Lose1", animator.transform.GetChild(1).gameObject, 0, 0.3f);

                                stop = true;
                            }
                        }
                    }
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetParameter(3).name == "Index")
            animator.SetInteger("Index", 0);

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
