using UnityEngine;
using System.Collections;


// Require these components when using this script
[RequireComponent(typeof(Animator))]

public class AnimationViewer : MonoBehaviour
{

	private Animator anim;						
	private AnimatorStateInfo currentState;		
	private AnimatorStateInfo previousState;	


	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		currentState = anim.GetCurrentAnimatorStateInfo (0);
		previousState = currentState;

	}



	void OnGUI()
	{	
		GUI.Box(new Rect(Screen.width - 280 , 45 ,120 , 410), "");
		if(GUI.Button(new Rect(Screen.width - 270 , 60 ,100, 20), "Attack01"))
			anim.SetBool ("attack01", true);
		if(GUI.Button(new Rect(Screen.width - 270, 90 ,100, 20), "Attack02"))
			anim.SetBool ("attack02", true);
		if(GUI.Button(new Rect(Screen.width - 270, 120 ,100, 20), "Attack03"))
			anim.SetBool ("attack03", true);
		if(GUI.Button(new Rect(Screen.width - 270, 150 ,100, 20), "Attack04"))
			anim.SetBool ("attack04", true);
		if(GUI.Button(new Rect(Screen.width - 270, 180 ,100, 20), "Skill01"))
			anim.SetBool ("skill01", true);
		if(GUI.Button(new Rect(Screen.width - 270, 210 ,100, 20), "Skill02"))
			anim.SetBool ("skill02", true);
		if(GUI.Button(new Rect(Screen.width - 270, 240 ,100, 20), "Skill02_Start"))
			anim.SetBool ("skill02_start", true);
        if (GUI.Button(new Rect(Screen.width - 270, 270, 100, 20), "Skill02_Loop"))
            anim.SetBool("skill02_loop", true);
        if (GUI.Button(new Rect(Screen.width - 270, 300, 100, 20), "Skill02_End"))
            anim.SetBool("skill02_end", true);
        if (GUI.Button(new Rect(Screen.width - 270, 330, 100, 20), "Damaged"))
            anim.SetBool("damaged", true);
        if (GUI.Button(new Rect(Screen.width - 270, 360, 100, 20), "Idle02"))
            anim.SetBool("idle02", true);
        if (GUI.Button(new Rect(Screen.width - 270, 390, 100, 20), "Block"))
            anim.SetBool("block", true);
        if (GUI.Button(new Rect(Screen.width - 270, 420, 100, 20), "Dead"))
            anim.SetBool("dead", true);

        GUI.Box(new Rect(Screen.width - 150, 45, 120, 410), "");
        if (GUI.Button(new Rect(Screen.width - 140, 60, 100, 20), "Jump"))
            anim.SetBool("jump", true);
        if (GUI.Button(new Rect(Screen.width - 140, 90, 100, 20), "Jump_start"))
            anim.SetBool("jump_start", true);
        if (GUI.Button(new Rect(Screen.width - 140, 120, 100, 20), "Jump_fall"))
            anim.SetBool("jump_fall", true);
        if (GUI.Button(new Rect(Screen.width - 140, 150, 100, 20), "Jump_landing"))
            anim.SetBool("jump_landing", true);
        if (GUI.Button(new Rect(Screen.width - 140, 180, 100, 20), "Walk"))
            anim.SetBool("walk_forward", true);
        if (GUI.Button(new Rect(Screen.width - 140, 210, 100, 20), "Walk_back"))
            anim.SetBool("walk_backward", true);
        if (GUI.Button(new Rect(Screen.width - 140, 240, 100, 20), "Run"))
            anim.SetBool("run", true);
        if (GUI.Button(new Rect(Screen.width - 140, 270, 100, 20), "Laugh"))
            anim.SetBool("laugh", true);
        if (GUI.Button(new Rect(Screen.width - 140, 300, 100, 20), "Lose"))
            anim.SetBool("lose", true);
        if (GUI.Button(new Rect(Screen.width - 140, 330, 100, 20), "Stun"))
            anim.SetBool("stun", true);
        if (GUI.Button(new Rect(Screen.width - 140, 360, 100, 20), "Victory"))
            anim.SetBool("victory", true);



        ;
	}
}
