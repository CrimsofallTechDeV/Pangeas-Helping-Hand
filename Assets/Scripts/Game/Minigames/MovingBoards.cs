using Unity.VisualScripting;
using UnityEngine;

namespace CrimsofallTechnologies.VR.Gameplay 
{
    public class MovingBoards : MonoBehaviour
    {
        public bool Active = true;
        public int points = 100;
        public bool removePoints = false; //false to add points

        [Space]
        public bool orange_blue;
        public bool livings;

        [Space]
        public float speed = 5f;
        public float minX = -10f;
        public float maxX = 10f;
        public bool startMovingRight = true;
        public ShootingGame shootingGame;

        private int direction;
        public Animator animator { get; private set; }

        private void Start()
        {
            direction = startMovingRight ? 1 : -1;
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(!Active)
                return;

            // Move the object
            transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);

            // Clamp position and reverse direction if limits are exceeded
            float x = transform.position.x;

            if (x >= maxX)
            {
                x = maxX;
                direction = -1;

                //reset animation
                animator.SetBool("Hit", false);
            }
            else if (x <= minX)
            {
                x = minX;
                direction = 1;

                //reset animation
                animator.SetBool("Hit", false);
            }

            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        public void OnShot()
        {
            //add/remove points!
            if(removePoints)
                GameManager.Instance.RemovePoints(points);
            else
                GameManager.Instance.AddPoints(points);

            animator.SetBool("Hit", true);
            shootingGame.scoresText.text = "Scores: " + GameManager.Instance.points.ToString();
            
            if(orange_blue)
            {
                shootingGame.oranges++;

                if(shootingGame.oranges >= 3) {
                    shootingGame.behaviour.StartDialog(shootingGame.cuteOnly);
                    shootingGame.oranges = 0;
                    shootingGame.tylerAnimator.SetTrigger("Pissed" + Random.Range(0, 2));
                }
            }

            if(livings)
            {
                shootingGame.livings++;

                if(shootingGame.livings >= 3) {
                    shootingGame.behaviour.StartDialog(shootingGame.notMoveHere);
                    shootingGame.livings = 0;
                    shootingGame.tylerAnimator.SetTrigger("Pissed" + Random.Range(0, 2));
                }
            }
        }
    }
}
