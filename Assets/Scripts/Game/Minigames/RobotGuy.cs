using UnityEngine;

public class RobotGuy : MonoBehaviour
{
	public float animationTime = 10f;
	
	private Animator animator;
	private bool isPlaying = false;
	private AudioSource source;
	
	private void Start() 
	{
		source = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
	}
	
	private void OnParticleCollision(GameObject other) 
	{
		if(!isPlaying && other.tag == "Water")
		{
			source.PlayOneShot(source.clip, source.volume);
			animator.SetTrigger("Sputter"); //play the stupid animation
			isPlaying = true;
		}
	}
	
	private void ResetSelf()
	{
		isPlaying = false;
	}
}
