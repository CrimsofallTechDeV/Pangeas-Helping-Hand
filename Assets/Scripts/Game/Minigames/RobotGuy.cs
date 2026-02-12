using UnityEngine;

public class RobotGuy : MonoBehaviour
{
	public float animationTime = 10f;
	
	private Animator animator;
	private bool isPlaying = false;
	public AudioSource source;
	private NPC npc;
	private bool progressedDialouge = false;

	private int lastDialougeIndex = 0;
	
	private void Start() 
	{
		animator = GetComponent<Animator>();
		npc = GetComponent<NPC>();
	}
	
	private void OnParticleCollision(GameObject other) 
	{
		if(!isPlaying && other.tag == "Water")
		{
			source.PlayOneShot(source.clip, source.volume);
			animator.SetBool("Sputter", true); //play the stupid animation
			Invoke(nameof(ResetSelf), animationTime + 0.15f);
			isPlaying = true;

			lastDialougeIndex = npc.dialougeIndex;
			if(!progressedDialouge) 
			{
				//the first one is electrocute.
				npc.SetDialougeIndex(0);
				progressedDialouge = true;
			}
		}
	}
	
	private void ResetSelf()
	{
		source.Stop();
		animator.SetBool("Sputter", false);
		isPlaying = false;
		Invoke(nameof(ResetDialouge), 3f); //reset the dialouge!
	}

	private void ResetDialouge() 
	{
		npc.SetDialougeIndex(lastDialougeIndex);
	}
}
