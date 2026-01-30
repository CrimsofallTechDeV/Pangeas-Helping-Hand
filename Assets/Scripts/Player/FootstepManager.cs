using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;

public class FootstepManager : MonoBehaviour
{
	public XRMovementTracker movementTracker;
	public GravityProvider gravity;
	public AudioClip concreteSteps, grassStep, woodStep, waterStep;
	public float stepDelay = 0.25f;
	
	public float radius;
	public LayerMask mask;

	private float time;
	private string lastFootstepTag;
	private AudioSource source;
	
	private void Start() 
	{
		source = GetComponent<AudioSource>();
		source.clip = concreteSteps;
	}
	
	private void Update()
	{
		//really moving? play footsteps
		if(movementTracker.IsMoving && gravity.isGrounded)
		{
			PlayFootsteps();
		}
		
		//detect footsteps
		if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, radius, mask, QueryTriggerInteraction.Ignore))
		{
			if( hit.collider.tag == "Untagged" ) return;
			
			AudioClip swap_target = null;

			switch( hit.collider.tag )
            {
                case "Concrete":	
					swap_target = concreteSteps; 
					break;
                case "Wood":		
					swap_target = woodStep; 
					break;
                case "Grass":		
					swap_target = grassStep; 
					break;
                case "Water":		
					swap_target = waterStep; 
					break;
                default: return;
            }

			if( swap_target != source.clip )
			{
				if( source.isPlaying )
				{
                    source.Stop();
                }

				source.clip = swap_target;
			}

            lastFootstepTag = hit.collider.tag;
        }
    }

	public void PlayFootsteps()
	{
		if(Time.time >= time)
		{
			source.PlayOneShot(source.clip, source.volume);
			time = Time.time + stepDelay;
		}
	}
	
	// Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected.
	protected void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, radius);
	}
}
