using cherrydev;
using UnityEngine;

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
