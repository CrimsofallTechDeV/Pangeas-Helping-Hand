using cherrydev;
using UnityEngine;

public class AxotolClimbing : MonoBehaviour
{
    public Animator parentAnimator;
    public GameObject realObject;
    public GameObject gfx;
    public NPC npc;
    public Animator gfxAnimator;
    public DialogNodeGraph winGraph, loseGraph;

    [Range(0.1f, 1f)]
    public float animatorSpeed = 1.0f;

    public bool PlayerWon { get; set; }
    private bool IsClimbing;

    private void Start()
    {
        //when player presses StartClimbing in dialog start climbing!
        npc.dialogBehaviour.BindExternalFunction("StartClimbing", () => StartClimbing());
        gfx.SetActive(false);
    }

    public void StartClimbing()
    {
        parentAnimator.speed = animatorSpeed;
        IsClimbing = true;
        gfx.SetActive(true);
        realObject.SetActive(false);

        PlayerWon = false;
        gfxAnimator.SetBool("Climb", true);
        parentAnimator.SetTrigger("Climb");
    }

    public void OnReachedTop()
    {
        gfxAnimator.SetBool("Climb", false);
        if(PlayerWon)
        {
            gfxAnimator.SetBool("Lose", true);
            npc.nodeGraphs[1] = loseGraph;
            npc.ProgressDialouge();
        }
        else
        {
            npc.ProgressDialouge();
            npc.nodeGraphs[1] = winGraph;
            gfxAnimator.SetBool("Win", true);
        }
        Invoke(nameof(JumpDown), 5f);
    }

    public void JumpDown()
    {
        parentAnimator.SetTrigger("Land");
        gfxAnimator.SetBool("Climb", false);
        IsClimbing = false;
    }

    public void OnReachedGround()
    {
        if (!IsClimbing)
        {    
            gfx.SetActive(false);
            realObject.SetActive(true);
            IsClimbing = false;
        }

        gfxAnimator.SetTrigger("Land");
    }
}
