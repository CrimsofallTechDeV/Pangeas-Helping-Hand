using UnityEngine;

public class WaterCan : MonoBehaviour
{
	public ParticleSystem waterParticle;

	//min, max
	public Vector2 pourAngle = new Vector2(30f, 60f); //the angle range at which the water will pour (set in inspector)

	private AudioSource source;
	private bool Carrying;
	private float angle;

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
		angle = Vector3.SignedAngle(transform.up, Vector3.up, transform.right);
		bool shouldPour = angle >= pourAngle.x && angle <= pourAngle.y;

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
