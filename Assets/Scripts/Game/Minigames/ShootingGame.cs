using cherrydev;
using UnityEngine;

public class ShootingGame : MonoBehaviour
{
    public bool Active = true;
    public float time = 120f;
    public DialogBehaviour behaviour;
    public AudioClip houseClip, shootingClip;

    public MovingBoards[] movingBoards;

    private void Start()
    {
        behaviour.BindExternalFunction("ShootYes",() => ActivateGame());

        if(!Active)
        {
            for (int i = 0; i < movingBoards.Length; i++)
            {
                movingBoards[i].Active = false;
            }
        }
    }

    public void ActivateGame()
    {
        Active = true;
        for (int i = 0; i < movingBoards.Length; i++)
        {
            movingBoards[i].Active = true;
        }

        GameManager.Instance.SetupMusic(shootingClip, shootingClip);

        //reset after 2 minutes!
        Invoke(nameof(ResetGame), time);
    }

    public void ResetGame()
    {
        Active = false;
        for (int i = 0; i < movingBoards.Length; i++)
        {
            movingBoards[i].Active = false;
        }

        //reset music
        GameManager.Instance.SetupMusic(houseClip, houseClip);
    }
}
