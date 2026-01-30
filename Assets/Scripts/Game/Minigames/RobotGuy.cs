using UnityEngine;

public class RobotGuy : MonoBehaviour
{
	public float animationTime = 10f;
	public int dialougeIndex = 2; //the dialouge index to set after player drops water on the robot
	
	private Animator animator;
	private bool isPlaying = false;
	private AudioSource source;
	private NPC npc;
	private bool progressedDialouge = false;
	
	private void Start() 
	{
		source = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
		npc = GetComponent<NPC>();
	}
	
	private void OnParticleCollision(GameObject other) 
	{
		if(!isPlaying && other.tag == "Water")
		{
			source.PlayOneShot(source.clip, source.volume);
			animator.SetBool("Sputter", true); //play the stupid animation
			Invoke(nameof(ResetSelf), source.clip.length + 0.15f);
			isPlaying = true;
			if(!progressedDialouge) {
				npc.ProgressDialouge(); //show the electrocute dialouge!
				progressedDialouge = true;
			}
		}
	}
	
	private void ResetSelf()
	{
		animator.SetBool("Sputter", false);
		isPlaying = false;
	}
}
