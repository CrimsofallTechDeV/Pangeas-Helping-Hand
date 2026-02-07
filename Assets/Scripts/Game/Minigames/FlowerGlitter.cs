using Unity.VisualScripting;
using UnityEngine;

public class FlowerGlitter : MonoBehaviour
{
	public ParticleSystem[] glitterParticle;
	public NPC npc; //the npc to progress dialouge for after player watered this flowers
	
	private AudioSource source;
	private bool progressedDialouge = false;

	private void Start()
	{
		source = GetComponent<AudioSource>();
	}
	
	private void OnParticleCollision(GameObject other) 
	{
		if(other.tag == "Water") {
			Glitter();
		}
	}
	
	public void Glitter()
	{
		for (int i = 0; i < glitterParticle.Length; i++) {
			if(!glitterParticle[i].isPlaying) 
				glitterParticle[i].Play();
		}

		if(npc!=null && !progressedDialouge) {
			progressedDialouge = true;
			npc.ProgressDialouge();
		}
		source.PlayOneShot(source.clip, source.volume);

		if(!GameManager.Instance.thingsDone.Contains("WateredFlowers"))
			GameManager.Instance.thingsDone.Add("WateredFlowers"); //add to things done so that it can be checked in other scripts that care about this! (like the journal)
	}
}
