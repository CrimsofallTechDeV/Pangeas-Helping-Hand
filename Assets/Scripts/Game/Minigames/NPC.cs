using UnityEngine;
using UnityEngine.UI;
using cherrydev;
using System;

public class NPC : MonoBehaviour
{
    public DialogBehaviour dialogBehaviour;
    public DialogNodeGraph[] nodeGraphs;
	public bool setTalkAnimationWhileTalking = false; //must have a trigger Talk and animation in the animator component!
	public bool DisableTalk = false; //will not talk if this is true
	
	//automatically progress dialouges when player talks with this NPC (will progress until there are no more dialouge objects left)
	public bool autoProgressDiagOnTalk = false;
	
	[Space]
	public Action OnTalkedAction;

    private int dialougeIndex;
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

    public void OnTalked(bool allowRestrictions = true)
	{
        if((allowRestrictions && DisableTalk) || dialogBehaviour._isDialogStarted)
            return;
		
		if(setTalkAnimationWhileTalking && animator != null) {
			animator.SetTrigger("Talk");
		}

        dialogBehaviour.StartDialog(nodeGraphs[dialougeIndex]);
		OnTalkedAction?.Invoke();
        
		if(autoProgressDiagOnTalk)
			ProgressDialouge();
    }

    public void ProgressDialouge()
    {
        dialougeIndex++;

        if(dialougeIndex >= nodeGraphs.Length) 
            dialougeIndex = nodeGraphs.Length - 1;
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
