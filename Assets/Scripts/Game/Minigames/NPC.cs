using UnityEngine;
using UnityEngine.UI;
using cherrydev;
using System;

public class NPC : MonoBehaviour
{
    public DialogBehaviour dialogBehaviour;
    public DialogNodeGraph[] nodeGraphs;
	public int startingDialougeIndex = 0; //the dialouge index to start at (set in inspector)
	public bool setTalkAnimationWhileTalking = false; //must have a trigger Talk and animation in the animator component!
	public bool DisableTalk = false; //will not talk if this is true
	
	//automatically progress dialouges when player talks with this NPC (will progress until there are no more dialouge objects left)
	public bool autoProgressDiagOnTalk = false;
	public bool stopAfterOneProgress = false; //will stop after progressing dialouge by 1
	
	[Space]
	public Action OnTalkedAction;

    public int dialougeIndex { get; private set; }
	private bool hasProgressedOnce = false;
	private Animator animator;

	private void Start()
	{
		dialougeIndex = startingDialougeIndex;
		animator = GetComponent<Animator>();
		if(animator == null) animator = GetComponentInChildren<Animator>();
	}

    public void OnTalked(bool allowRestrictions = true)
	{
        if((allowRestrictions && DisableTalk) || dialogBehaviour._isDialogStarted)
            return;

		if(nodeGraphs.Length == 0)
			return;
		
		if(setTalkAnimationWhileTalking && animator != null) {
			animator.SetTrigger("Talk");
		}

        dialogBehaviour.StartDialog(nodeGraphs[dialougeIndex]);
		OnTalkedAction?.Invoke();
        
		if(autoProgressDiagOnTalk) {
			if(stopAfterOneProgress && hasProgressedOnce)
				return;

			ProgressDialouge();
			hasProgressedOnce = true;
    	}
	}

    public void ProgressDialouge()
    {
        dialougeIndex++;

        if(dialougeIndex >= nodeGraphs.Length) 
            dialougeIndex = nodeGraphs.Length - 1;
    }

	public void SetDialougeIndex(int index)
	{
		dialougeIndex = index;
	}
    
	public void SetAnimatorBool_On(string Name)
	{
		animator.SetBool(Name, true);
	}
	
	public void SetAnimatorBool_Off(string Name)
	{
		animator.SetBool(Name, false);
	}
}
