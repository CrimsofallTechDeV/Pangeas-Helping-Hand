using UnityEngine;

public class WaterCan : MonoBehaviour
{
	public ParticleSystem waterParticle;
	public float pourAngle = 45f;

	private AudioSource source;
	private bool Carrying;

	private void Start()
	{
		source = GetComponent<AudioSource>();

		//stop water particle
		var emission = waterParticle.emission;
		emission.enabled = false;
	}

	private void Update()
	{
		if (!waterParticle || !Carrying) 
			return;

		// Compare can's "up" vector to world up
		float angle = Vector3.Angle(transform.up, Vector3.up);
		bool shouldPour = angle > pourAngle;

		var emission = waterParticle.emission;
		emission.enabled = shouldPour;

		if(shouldPour && !source.isPlaying)
        {
            source.Play();
        }

		if(!shouldPour && source.isPlaying)
        {
            source.Stop();
        }
	}

	public void OnPickedCan()
    {
		Carrying = true;
    }

	public void OnDroppedCan()
    {
		Carrying = false;
        source.Stop();

		//stop water particle
		var emission = waterParticle.emission;
		emission.enabled = false;
    }
}
