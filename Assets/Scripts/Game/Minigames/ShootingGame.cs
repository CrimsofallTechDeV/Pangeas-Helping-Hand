using System.Collections;
using cherrydev;
using CrimsofallTechnologies.VR.Inventory;
using UnityEngine;
using TimeSpan = System.TimeSpan;

namespace CrimsofallTechnologies.VR.Gameplay 
{
    public class ShootingGame : MonoBehaviour
    {
        public bool Active = true;
        public float time = 120f;
        public TextMesh scoresText, timeText;
        public Animator tylerAnimator;
        public DialogBehaviour behaviour;
        public GameObject[] toActives;
        public Pickup rayGunPickup;
        public AudioClip houseClip, shootingClip;
        public TriggerObjectEnabler triggerObjectEnabler;

        public DialogNodeGraph cuteOnly, notMoveHere, win, lose;

        public MovingBoards[] movingBoards;
        private int timeLeft;

        public int oranges { get; set; }
        public int livings { get; set; }

        private void Start()
        {
            rayGunPickup.canCarry = false;
            behaviour.BindExternalFunction("ShootYes",() => ActivateGame());
            triggerObjectEnabler.enabled = false;

            if(!Active)
            {
                for (int i = 0; i < movingBoards.Length; i++)
                {
                    movingBoards[i].Active = false;
                }

                for (int i = 0; i < toActives.Length; i++)
                {
                    toActives[i].SetActive(false);
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

            for (int i = 0; i < toActives.Length; i++)
            {
                toActives[i].SetActive(true);
            }

            triggerObjectEnabler.enabled = true;
            GameManager.Instance.SetupMusic(shootingClip, shootingClip);

            //reset after 2 minutes!
            timeLeft = (int)time;
            scoresText.text = "Scores: " + GameManager.Instance.points.ToString();
            timeText.text = "Time: " + TimeSpan.FromSeconds(timeLeft).ToString(@"mm\:ss");
            StartCoroutine(DelayedResetGame());
        }

        private IEnumerator DelayedResetGame()
        {
            while (Active)
            {
                yield return new WaitForSeconds(1f);
                timeLeft--;
                timeText.text = "Time: " + TimeSpan.FromSeconds(timeLeft).ToString(@"mm\:ss");

                if (timeLeft <= 0)
                {
                    if(GameManager.Instance.points <= 3000)
                    {
                        behaviour.StartDialog(lose);
                        tylerAnimator.SetTrigger("Lose"+ Random.Range(0, 2));
                    }
                    else 
                    {
                        rayGunPickup.canCarry = true;
                        behaviour.StartDialog(win);
                        tylerAnimator.SetTrigger("Win" + Random.Range(0, 3));
                    }

                    ResetGame();
                }
            }
        }

        public void ResetGame()
        {
            triggerObjectEnabler.enabled = false;
            Active = false;
            for (int i = 0; i < movingBoards.Length; i++)
            {
                movingBoards[i].Active = false;
            }

            for (int i = 0; i < toActives.Length; i++)
            {
                toActives[i].SetActive(false);
            }

            //reset music
            GameManager.Instance.SetupMusic(houseClip, houseClip);
        }
    }
}
