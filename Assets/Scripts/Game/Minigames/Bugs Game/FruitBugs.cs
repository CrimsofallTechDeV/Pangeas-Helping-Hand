using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace CrimsofallTechnologies.VR.Gameplay.BugGame
{
	public class FruitBugs : MonoBehaviour
	{
		public bool Active = false;
		public GameObject bugPrefab, fruitPrefab;
		//bug spawn areas
		public Transform[] spawnAreas;
		public Transform[] fruitSpawnAreas;
		public List<Transform> fruits;
		private Transform[] allFruits_backup;
		public AudioClip music;

		public float minSpawnDelay = 2f, maxSpawnDelay = 4f;

		private List<GameObject> bugs = new List<GameObject>();
		private float lastTime;

		private void Start() { allFruits_backup = fruits.ToArray(); }
		
		private void Update()
		{
			if(!Active)
				return;

			if(Time.time >= lastTime)
			{
				SpawnRandomBug();
				
				lastTime = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
			}
		}
		
		private void SpawnRandomBug()
		{
			//spawn no more if there are no fruits!
			if(fruits.Count == 0) {
				return;
			}

			BugAI ai = Instantiate(bugPrefab, spawnAreas[Random.Range(0, spawnAreas.Length)].position, Quaternion.identity).GetComponent<BugAI>();
			bugs.Add(ai.gameObject);
			ai.SetTarget(fruits[Random.Range(0, fruits.Count)]);
		}

		public void RemoveFruit(GameObject fruit)
		{
			fruits.Remove(fruit.transform);

			if(fruits.Count == 0)
			{
				//remove all bugs too!
				for (int i = 0; i > bugs.Count ; i--)
				{
					Destroy(bugs[i]);
				}
				bugs.Clear();
				DeactivateGame();

				//means all fruits are gone, player wins!
				if(!GameManager.Instance.thingsDone.Contains("FruitBugsDone"))
					GameManager.Instance.thingsDone.Add("FruitBugsDone");
			}
		}

		public void RemoveBug(GameObject bug)
		{
			bugs.Remove(bug);
		}

		//activated when player picks up the fly swatter!
		public void ActivateGame()
		{
			Active = true;
			GameManager.Instance.SetupMusic(music, music);
		}

		public void DeactivateGame()
		{
			Active = false;
			GameManager.Instance.ResetMusic();

			//repsawn all fruits for next time!
			for (int i = 0; i < allFruits_backup.Length; i++)
			{
				allFruits_backup[i].gameObject.SetActive(true);
				fruits.Add(allFruits_backup[i]);
			}
		}

		public void PickedFruit(Transform area)
		{
			//spawn another there after some delay!
			StartCoroutine(SpawnFruit(area));
		}

		private IEnumerator SpawnFruit(Transform area)
		{
			yield return new WaitForSeconds(3f);
			GameObject fruit = Instantiate(fruitPrefab, area.position, Quaternion.Euler(-90, 0, 0), area.parent);
			fruits.Add(fruit.transform);
			fruit.GetComponent<EatableFruit>().fb = this;
		}
	}
}
