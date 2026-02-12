using cherrydev;
using UnityEngine;

namespace CrimsofallTechnologies.VR.Gameplay
{
    //this is for the dark wood shooter minigame, when the player tries to get out while the minigame is active, the shooting guy will say something
    //when player tries to get out while minigame is running say something
    public class DarkWoodShooter : MonoBehaviour
    {
        public NPC darkwoodShooterGuy;
        public DialogNodeGraph dialog;
        public ShootingGame shootingGame;

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player" && shootingGame.Active)
            {
                //make the shooting guy say something lol
                darkwoodShooterGuy.dialogBehaviour.StartDialog(dialog);
            }
        }
    }
}
