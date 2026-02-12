using System.Collections;
using System.Collections.Generic;
using CrimsofallTechnologies.VR.Music;
using CrimsofallTechnologies.VR.SceneManagement;
using CrimsofallTechnologies.VR.UI;
using CrimsofallTechnologies.VR.DataSaving;
using CrimsofallTechnologies.VR.Inventory;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;

namespace CrimsofallTechnologies.VR
{
    [DefaultExecutionOrder(0)]
    public class GameManager : MonoBehaviour
    {
        #region SINGLETON

        public static GameManager Instance;
        public static UIManager ui;
        public static CustomSceneManager sceneM;
        public BGMPlayer bgmPlayer;
        public static Transform playerObject;
        public static PlayerInventory playerInventory;

        private void Awake()
        {
            if (Instance == null)
            {
                Application.targetFrameRate = maxFramerate;
                DontDestroyOnLoad(gameObject);
                Instance = this;
                CanEnterEntrances = true;
                SceneManager.sceneLoaded += OnSceneLoadComplete;

                //always start in day!
                IsDay = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetCustomSceneManager(CustomSceneManager customSceneManager)
        {
            sceneM = customSceneManager;

            //default music clips:
            defaultMusicDay = sceneM.musicClipDay;
            defaultMusicNight = sceneM.musicClipNight;
        }
        
        private void OnApplicationQuit()
        {
            Instance = null;
        }

        #endregion

        public int maxFramerate = 60;
        public float levelLoadDelay = 2f;

        private AudioClip defaultMusicDay, defaultMusicNight;

        public List<string> thingsDone = new List<string>();
        public int maxThingsToDo = 0;

        [Space]
        public UnityEvent OnGameStartedEvennt;

        public PlayerHandObjectTracker handTracker { get; set; }
        public bool LoadingLevel { get; private set; }
        private string sceneName;
        public bool InteractedRecent { get; private set; } //has recently interacted with something?
        public bool IsDay { get; set; }
        public bool EnteredEntrance { get; private set; }
        public bool CanEnterEntrances { get; set; }

        #region POINTS

        public int points { get; private set; }

        public void AddPoints(int amount)
        {
            points += amount;
            ui.UpdateUI();
        }
        public void RemovePoints(int amount)
        {
            points -= amount;
            ui.UpdateUI();
        }

        #endregion

        public void SetDayNightValue(bool value)
        {
            IsDay = value;

            //set day/night sun light
            SetupSceneLight();
        }

        public void OnEnteredEntrance()
        {
            EnteredEntrance = true;
            Invoke(nameof(ResetEntrance), 0.5f);
        }

        private void ResetEntrance()
        {
            EnteredEntrance = false;
        }

        public void Interacted()
        {
            if (!InteractedRecent)
            {
                InteractedRecent = true;
                Invoke(nameof(ResetInteraction), 1f);
            }
        }

        private void ResetInteraction()
        {
            InteractedRecent = false;
        }

        private void OnSceneLoadComplete(Scene arg0, LoadSceneMode arg1)
        {
            if(Instance != this) return;

            LoadingLevel = false;

            //set day/night sun light
            StartCoroutine(GameStart());
            OnGameStartedEvennt?.Invoke();
        }

        private IEnumerator GameStart()
        {
            yield return new WaitForSeconds(0.5f);
            LoadPlayerData();
            SetupSceneLight();
            yield return new WaitForSeconds(2.5f);
            DisableLoadingUI();
        }

        private void DisableLoadingUI()
        {
            if(LoadingUI.Instance != null) LoadingUI.Instance.DisableScreen();
        }

        private void SetupSceneLight()
        {
            //setup ender game
            if(sceneM != null && sceneM.enderManager != null)
                sceneM.enderManager.hexMan.SetActive(thingsDone.Count == maxThingsToDo);

            if (IsDay)
            {
                if (sceneM != null)
                {
                    if (sceneM.sun)
                        sceneM.sun.intensity = 1f;
                    bgmPlayer.PlayMusic(sceneM.musicClipDay);
                    
                    //enable/disable night/day objects from scene
                    for (int i = 0; i < sceneM.dayObjects.Length; i++)
                        sceneM.dayObjects[i].SetActive(true);
                    for (int i = 0; i < sceneM.nightObjects.Length; i++)
                        sceneM.nightObjects[i].SetActive(false);
                    
                    RenderSettings.skybox = sceneM.skyLight;
                }
            }
            else
            {
                if (sceneM != null)
                {
                    if (sceneM.sun)
                        sceneM.sun.intensity = 0f;
                    if (sceneM.musicClipNight != null)
                        bgmPlayer.PlayMusic(sceneM.musicClipNight);
                    else
                        bgmPlayer.PlayMusic(sceneM.musicClipDay);

                    //enable/disable night/day objects from scene
                    for (int i = 0; i < sceneM.dayObjects.Length; i++)
                        sceneM.dayObjects[i].SetActive(false);
                    for (int i = 0; i < sceneM.nightObjects.Length; i++)
                        sceneM.nightObjects[i].SetActive(true);
                    
                    RenderSettings.skybox = sceneM.skyDark;
                }
            }
        }

        public void SetupMusic(AudioClip dayClip, AudioClip nightClip)
        {
            if(IsDay) 
                bgmPlayer.PlayMusic(dayClip);
            else 
                bgmPlayer.PlayMusic(nightClip);

            //apply to scene manager for future use
            if (sceneM)
            {
                sceneM.musicClipDay = dayClip;
                sceneM.musicClipNight = nightClip;
            }
        }

        public void ResetMusic()
        {
            if(IsDay) bgmPlayer.PlayMusic(defaultMusicDay);
            else bgmPlayer.PlayMusic(defaultMusicNight);

            if(sceneM)
            {
                sceneM.musicClipDay = defaultMusicDay;
                sceneM.musicClipNight = defaultMusicNight;
            }
        }

        public void LoadLevel(string Name)
        {
            //save player data!
            SavePlayerData();

            LoadingLevel = true;
            sceneName = Name;
            sceneM = null;
            if(LoadingUI.Instance != null) LoadingUI.Instance.EnableScreen();
            Invoke(nameof(DelayedLoadLevel), levelLoadDelay);
        }

        private void DelayedLoadLevel()
        {
            SceneManager.LoadScene(sceneName);
        }

        public void SavePlayerData()
        {
            PlayerData data = new PlayerData()
            {
                inventoryData = playerInventory.GetAllItems(), 
                points = points,
                elephantCreated = thingsDone.Contains("ToyCreated"),
            };
            PlayerDataSaver.SaveData(data);
        }

        private void LoadPlayerData()
        {
            PlayerData data = PlayerDataSaver.LoadData();

            if (data != null) {
                playerInventory.ApplyLoadedData(data);
                points = data.points;
            
                if(data.elephantCreated)
                    thingsDone.Add("ToyCreated");
            }


            playerObject.GetComponentInChildren<GravityProvider>().useGravity = true;
            ui.UpdateUI();
        }
    }
}
