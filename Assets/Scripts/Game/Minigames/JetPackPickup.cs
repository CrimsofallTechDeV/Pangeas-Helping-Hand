using CrimsofallTechnologies.XR;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Jump;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

namespace CrimsofallTechnologies.XR.Gameplay 
{
	//player applies the jetpack and floats around, this disappears upon interaction and may activate some component on player that controls jetpack style movements!
    public class JetPackPickup : MonoBehaviour
    {
	    public JumpProvider jump;
	    
	    public void OnPick()
	    {
	    	gameObject.SetActive(false); //means player picked the jetpack
	    	
	    	//enable infinite jumps
	    	jump.unlimitedInAirJumps = true;
	    }
    }
}
