using Connect.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Connect.Core
{
    public class GameManager : MonoBehaviour
    {
        #region START_METHODS

        public static GameManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Init();
                DontDestroyOnLoad(gameObject);
                return;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Init()
        {
            CurrentStage = 1;
            CurrentLevel= 1;

            Levels = new Dictionary<string, LevelData>();

           /* foreach (var item in _allLevels.Levels)
            {
                Levels[item.LevelName] = item;
            }*/
        }


        #endregion

        #region GAME_VARIABLES

        
        public int CurrentStage;

        
        public int CurrentLevel;

        
        public string StageName;

        public LevelData[] level;

        public bool IsLevelUnlocked(int level)
        {
            string levelName = "Level" + CurrentStage.ToString() + level.ToString();

            if(level == 1)
            {
                PlayerPrefs.SetInt(levelName, 1);
                return true;
            }

            if(PlayerPrefs.HasKey(levelName)) 
            {
                return PlayerPrefs.GetInt(levelName) == 1;
            }

            PlayerPrefs.SetInt(levelName, 0);
            return false;
        }

        public void UnlockLevel()
        {
            CurrentLevel++;

            if(CurrentLevel == 51)
            {
                CurrentLevel = 1;
                CurrentStage++;

                if(CurrentStage == 8)
                {
                    CurrentStage = 1;
                    GoToMainMenu();
                }
            }

            string levelName = "Level" + CurrentStage.ToString() + CurrentLevel.ToString();
            PlayerPrefs.SetInt(levelName, 1);
        }

        #endregion

        #region LEVEL_DATA

        [SerializeField]
        private LevelData DefaultLevel;

        [SerializeField]
        private LevelList _allLevels;

        private Dictionary<string, LevelData> Levels;

        public LevelData GetLevel()
        {
            
            
            string levelName = "Level" + CurrentStage.ToString() + CurrentLevel.ToString();

            if (CurrentLevel == 1&&CurrentStage==1)
            {
                DefaultLevel = level[0];
            }
            if (CurrentLevel == 2 && CurrentStage == 1)
            {
                DefaultLevel = level[1];
            }
            if (CurrentLevel == 3 && CurrentStage == 1)
            {
                DefaultLevel = level[2];
            }
            if (CurrentLevel == 4 && CurrentStage == 1)
            {
                DefaultLevel = level[3];
            }
            if (CurrentLevel == 5 && CurrentStage == 1)
            {
                DefaultLevel = level[4];
            }
            if (CurrentLevel == 1 && CurrentStage == 2)
            {
                DefaultLevel = level[5];
            }
            if (CurrentLevel == 2 && CurrentStage == 2)
            {
                DefaultLevel = level[6];
            }
            if (CurrentLevel == 3 && CurrentStage == 2)
            {
                DefaultLevel = level[7];
            }
            if (CurrentLevel == 4 && CurrentStage == 2)
            {
                DefaultLevel = level[8];
            }
            if (CurrentLevel == 5 && CurrentStage == 2)
            {
                DefaultLevel = level[9];
            }

            if (CurrentLevel == 1 && CurrentStage == 3)
            {
                DefaultLevel = level[10];
            }
            if (CurrentLevel == 2 && CurrentStage == 3)
            {
                DefaultLevel = level[11];
            }
            if (CurrentLevel == 3 && CurrentStage == 3)
            {
                DefaultLevel = level[12];
            }
            if (CurrentLevel == 4 && CurrentStage == 3)
            {
                DefaultLevel = level[13];
            }
            if (CurrentLevel == 5 && CurrentStage == 3)
            {
                DefaultLevel = level[14];
            }

            if (CurrentLevel == 1 && CurrentStage == 4)
            {
                DefaultLevel = level[15];
            }
            if (CurrentLevel == 2 && CurrentStage == 4)
            {
                DefaultLevel = level[16];
            }
            if (CurrentLevel == 3 && CurrentStage == 4)
            {
                DefaultLevel = level[17];
            }
            if (CurrentLevel == 4 && CurrentStage == 4)
            {
                DefaultLevel = level[18];
            }
            if (CurrentLevel == 5 && CurrentStage == 4)
            {
                DefaultLevel = level[19];
            }


            if (CurrentLevel == 1 && CurrentStage == 5)
            {
                DefaultLevel = level[20];
            }
            if (CurrentLevel == 2 && CurrentStage == 5)
            {
                DefaultLevel = level[21];
            }
            if (CurrentLevel == 3 && CurrentStage == 5)
            {
                DefaultLevel = level[22];
            }
            if (CurrentLevel == 4 && CurrentStage == 5)
            {
                DefaultLevel = level[23];
            }
            if (CurrentLevel == 5 && CurrentStage == 5)
            {
                DefaultLevel = level[24];
            }

            if (CurrentLevel == 1 && CurrentStage == 6)
            {
                DefaultLevel = level[25];
            }
            if (CurrentLevel == 2 && CurrentStage == 6)
            {
                DefaultLevel = level[26];
            }
            if (CurrentLevel == 3 && CurrentStage == 6)
            {
                DefaultLevel = level[27];
            }
            if (CurrentLevel == 4 && CurrentStage == 6)
            {
                DefaultLevel = level[28];
            }
            if (CurrentLevel == 5 && CurrentStage == 6)
            {
                DefaultLevel = level[29];
            }

            if (CurrentLevel == 1 && CurrentStage == 7)
            {
                DefaultLevel = level[30];
            }
            if (CurrentLevel == 2 && CurrentStage == 7)
            {
                DefaultLevel = level[31];
            }
            if (CurrentLevel == 3 && CurrentStage == 7)
            {
                DefaultLevel = level[32];
            }
            if (CurrentLevel == 4 && CurrentStage == 7)
            {
                DefaultLevel = level[33];
            }
            if (CurrentLevel == 5 && CurrentStage == 7)
            {
                DefaultLevel = level[34];
            }

            if (Levels.ContainsKey(levelName))
            {
                return Levels[levelName];
            }

            return DefaultLevel;
        }

        public void Loadlevel()
        {
            
        }

        #endregion

        #region SCENE_LOAD

        private const string MainMenu = "MainMenu";
        private const string Gameplay = "Gameplay";

        public void GoToMainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenu);
        }

        public void GoToGameplay()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(Gameplay);
        }

        #endregion
    } 
}
