using Connect.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Connect.Core
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;
        public Button BackBtn;

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
            CurrentLevel = 1;

            Levels = new Dictionary<string, LevelData>();

            // Uncomment this block if _allLevels.Levels contains the level data you want to load
            /* foreach (var item in _allLevels.Levels)
            {
                Levels[item.LevelName] = item;
            }*/
        }



        public int CurrentStage;
        public int CurrentLevel;
        public Sprite StageName; // StageName here, but not linked to LevelData
        public LevelData[] level; // Level array

        private const string LoginTypeKey = "LoginType";
        private const string FacebookLogin = "Facebook";
        private const string GoogleLogin = "Google";
        private const string GuestLogin = "Guest";
        private string currentLoginKey;

        public bool IsLevelUnlocked(int level)
        {
            string levelKey = $"{currentLoginKey}_Level{CurrentStage}_{level}";

            if (level == 1)
            {
                PlayerPrefs.SetInt(levelKey, 1); // Always unlock level 1
                return true;
            }

            if (PlayerPrefs.HasKey(levelKey))
            {
                return PlayerPrefs.GetInt(levelKey) == 1;
            }

            PlayerPrefs.SetInt(levelKey, 0); // Set locked state by default
            return false;
        }

        public void UnlockLevel()
        {
            CurrentLevel++;

            if (CurrentLevel == 50)
            {
                CurrentLevel = 1;
                CurrentStage++;
                GameplayManager.Instance.nextButton();
                GameplayManager.Instance.RefreshButton();

                if (CurrentStage == 8)
                {
                    CurrentStage = 1;
                    GoToMainMenu();
                }
            }

            string levelKey = $"{currentLoginKey}_Level{CurrentStage}_{CurrentLevel}";
            PlayerPrefs.SetInt(levelKey, 1); // Unlock this level
            SaveProgress();
        }



        public void SetLoginType(string loginType)
        {
            PlayerPrefs.SetString(LoginTypeKey, loginType);
            currentLoginKey = loginType;
            LoadProgress();
        }

        private void LoadLoginType()
        {
            if (PlayerPrefs.HasKey(LoginTypeKey))
            {
                currentLoginKey = PlayerPrefs.GetString(LoginTypeKey);
            }
            else
            {
                currentLoginKey = GuestLogin; // Default to Guest if no login found
                SetLoginType(GuestLogin);
            }
        }



        public void SaveProgress()
        {
            PlayerPrefs.SetInt($"{currentLoginKey}_CurrentStage", CurrentStage);
            PlayerPrefs.SetInt($"{currentLoginKey}_CurrentLevel", CurrentLevel);
            PlayerPrefs.Save();
        }

        public void LoadProgress()
        {
            CurrentStage = PlayerPrefs.GetInt($"{currentLoginKey}_CurrentStage", 1);
            CurrentLevel = PlayerPrefs.GetInt($"{currentLoginKey}_CurrentLevel", 1);
        }



        [SerializeField]
        private LevelData DefaultLevel;

        [SerializeField]
        private LevelList _allLevels;

        private Dictionary<string, LevelData> Levels;




        public LevelData GetLevel()
        {

            string levelKey = "Level" + CurrentStage.ToString() + CurrentLevel.ToString();

            if (CurrentLevel == 1 && CurrentStage == 1)
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
            if (CurrentLevel == 6 && CurrentStage == 1)
            {
                DefaultLevel = level[5];
            }
            if (CurrentLevel == 7 && CurrentStage == 1)
            {
                DefaultLevel = level[6];
            }
            if (CurrentLevel == 8 && CurrentStage == 1)
            {
                DefaultLevel = level[7];
            }
            if (CurrentLevel == 9 && CurrentStage == 1)
            {
                DefaultLevel = level[8];
            }
            if (CurrentLevel == 10 && CurrentStage == 1)
            {
                DefaultLevel = level[9];
            }
            if (CurrentLevel == 11 && CurrentStage == 1)
            {
                DefaultLevel = level[10];
            }
            if (CurrentLevel == 12 && CurrentStage == 1)
            {
                DefaultLevel = level[11];
            }
            if (CurrentLevel == 13 && CurrentStage == 1)
            {
                DefaultLevel = level[12];
            }
            if (CurrentLevel == 14 && CurrentStage == 1)
            {
                DefaultLevel = level[13];
            }
            if (CurrentLevel == 15 && CurrentStage == 1)
            {
                DefaultLevel = level[14];
            }
            if (CurrentLevel == 16 && CurrentStage == 1)
            {
                DefaultLevel = level[15];
            }
            if (CurrentLevel == 17 && CurrentStage == 1)
            {
                DefaultLevel = level[16];
            }
            if (CurrentLevel == 18 && CurrentStage == 1)
            {
                DefaultLevel = level[17];
            }
            if (CurrentLevel == 19 && CurrentStage == 1)
            {
                DefaultLevel = level[18];
            }
            if (CurrentLevel == 20 && CurrentStage == 1)
            {
                DefaultLevel = level[19];
            }
            if (CurrentLevel == 21 && CurrentStage == 1)
            {
                DefaultLevel = level[20];
            }
            if (CurrentLevel == 22 && CurrentStage == 1)
            {
                DefaultLevel = level[21];
            }
            if (CurrentLevel == 23 && CurrentStage == 1)
            {
                DefaultLevel = level[22];
            }
            if (CurrentLevel == 24 && CurrentStage == 1)
            {
                DefaultLevel = level[23];
            }
            if (CurrentLevel == 25 && CurrentStage == 1)
            {
                DefaultLevel = level[24];
            }
            if (CurrentLevel == 26 && CurrentStage == 1)
            {
                DefaultLevel = level[25];
            }
            if (CurrentLevel == 27 && CurrentStage == 1)
            {
                DefaultLevel = level[26];
            }
            if (CurrentLevel == 28 && CurrentStage == 1)
            {
                DefaultLevel = level[27];
            }
            if (CurrentLevel == 29 && CurrentStage == 1)
            {
                DefaultLevel = level[28];
            }
            if (CurrentLevel == 30 && CurrentStage == 1)
            {
                DefaultLevel = level[29];
            }
            if (CurrentLevel == 31 && CurrentStage == 1)
            {
                DefaultLevel = level[30];
            }
            if (CurrentLevel == 32 && CurrentStage == 1)
            {
                DefaultLevel = level[31];
            }
            if (CurrentLevel == 33 && CurrentStage == 1)
            {
                DefaultLevel = level[32];
            }
            if (CurrentLevel == 34 && CurrentStage == 1)
            {
                DefaultLevel = level[33];
            }
            if (CurrentLevel == 35 && CurrentStage == 1)
            {
                DefaultLevel = level[34];
            }
            if (CurrentLevel == 36 && CurrentStage == 1)
            {
                DefaultLevel = level[35];
            }
            if (CurrentLevel == 37 && CurrentStage == 1)
            {
                DefaultLevel = level[36];
            }
            if (CurrentLevel == 38 && CurrentStage == 1)
            {
                DefaultLevel = level[37];
            }
            if (CurrentLevel == 39 && CurrentStage == 1)
            {
                DefaultLevel = level[38];
            }
            if (CurrentLevel == 40 && CurrentStage == 1)
            {
                DefaultLevel = level[39];
            }
            if (CurrentLevel == 41 && CurrentStage == 1)
            {
                DefaultLevel = level[40];
            }
            if (CurrentLevel == 42 && CurrentStage == 1)
            {
                DefaultLevel = level[41];
            }
            if (CurrentLevel == 43 && CurrentStage == 1)
            {
                DefaultLevel = level[42];
            }
            if (CurrentLevel == 44 && CurrentStage == 1)
            {
                DefaultLevel = level[43];
            }
            if (CurrentLevel == 45 && CurrentStage == 1)
            {
                DefaultLevel = level[44];
            }
            if (CurrentLevel == 46 && CurrentStage == 1)
            {
                DefaultLevel = level[45];
            }
            if (CurrentLevel == 47 && CurrentStage == 1)
            {
                DefaultLevel = level[46];
            }
            if (CurrentLevel == 48 && CurrentStage == 1)
            {
                DefaultLevel = level[47];
            }
            if (CurrentLevel == 49 && CurrentStage == 1)
            {
                DefaultLevel = level[48];
            }
            if (CurrentLevel == 50 && CurrentStage == 1)
            {
                DefaultLevel = level[49];
            }


            if (CurrentLevel == 1 && CurrentStage == 2)
            {
                DefaultLevel = level[50];
            }
            if (CurrentLevel == 2 && CurrentStage == 2)
            {
                DefaultLevel = level[51];
            }
            if (CurrentLevel == 3 && CurrentStage == 2)
            {
                DefaultLevel = level[52];
            }
            if (CurrentLevel == 4 && CurrentStage == 2)
            {
                DefaultLevel = level[53];
            }
            if (CurrentLevel == 5 && CurrentStage == 2)
            {
                DefaultLevel = level[54];
            }
            if (CurrentLevel == 6 && CurrentStage == 2)
            {
                DefaultLevel = level[55];
            }
            if (CurrentLevel == 7 && CurrentStage == 2)
            {
                DefaultLevel = level[56];
            }
            if (CurrentLevel == 8 && CurrentStage == 2)
            {
                DefaultLevel = level[57];
            }
            if (CurrentLevel == 9 && CurrentStage == 2)
            {
                DefaultLevel = level[58];
            }
            if (CurrentLevel == 10 && CurrentStage == 2)
            {
                DefaultLevel = level[59];
            }
            if (CurrentLevel == 11 && CurrentStage == 2)
            {
                DefaultLevel = level[60];
            }
            if (CurrentLevel == 12 && CurrentStage == 2)
            {
                DefaultLevel = level[61];
            }
            if (CurrentLevel == 13 && CurrentStage == 2)
            {
                DefaultLevel = level[62];
            }
            if (CurrentLevel == 14 && CurrentStage == 2)
            {
                DefaultLevel = level[63];
            }
            if (CurrentLevel == 15 && CurrentStage == 2)
            {
                DefaultLevel = level[64];
            }
            if (CurrentLevel == 16 && CurrentStage == 2)
            {
                DefaultLevel = level[65];
            }
            if (CurrentLevel == 17 && CurrentStage == 2)
            {
                DefaultLevel = level[66];
            }
            if (CurrentLevel == 18 && CurrentStage == 2)
            {
                DefaultLevel = level[67];
            }
            if (CurrentLevel == 19 && CurrentStage == 2)
            {
                DefaultLevel = level[68];
            }
            if (CurrentLevel == 20 && CurrentStage == 2)
            {
                DefaultLevel = level[69];
            }
            if (CurrentLevel == 21 && CurrentStage == 2)
            {
                DefaultLevel = level[70];
            }
            if (CurrentLevel == 22 && CurrentStage == 2)
            {
                DefaultLevel = level[71];
            }
            if (CurrentLevel == 23 && CurrentStage == 2)
            {
                DefaultLevel = level[72];
            }
            if (CurrentLevel == 24 && CurrentStage == 2)
            {
                DefaultLevel = level[73];
            }
            if (CurrentLevel == 25 && CurrentStage == 2)
            {
                DefaultLevel = level[74];
            }
            if (CurrentLevel == 26 && CurrentStage == 2)
            {
                DefaultLevel = level[75];
            }
            if (CurrentLevel == 27 && CurrentStage == 2)
            {
                DefaultLevel = level[76];
            }
            if (CurrentLevel == 28 && CurrentStage == 2)
            {
                DefaultLevel = level[77];
            }
            if (CurrentLevel == 29 && CurrentStage == 2)
            {
                DefaultLevel = level[78];
            }
            if (CurrentLevel == 30 && CurrentStage == 2)
            {
                DefaultLevel = level[79];
            }
            if (CurrentLevel == 31 && CurrentStage == 2)
            {
                DefaultLevel = level[80];
            }
            if (CurrentLevel == 32 && CurrentStage == 2)
            {
                DefaultLevel = level[81];
            }
            if (CurrentLevel == 33 && CurrentStage == 2)
            {
                DefaultLevel = level[82];
            }
            if (CurrentLevel == 34 && CurrentStage == 2)
            {
                DefaultLevel = level[83];
            }
            if (CurrentLevel == 35 && CurrentStage == 2)
            {
                DefaultLevel = level[84];
            }
            if (CurrentLevel == 36 && CurrentStage == 2)
            {
                DefaultLevel = level[85];
            }
            if (CurrentLevel == 37 && CurrentStage == 2)
            {
                DefaultLevel = level[86];
            }
            if (CurrentLevel == 38 && CurrentStage == 2)
            {
                DefaultLevel = level[87];
            }
            if (CurrentLevel == 39 && CurrentStage == 2)
            {
                DefaultLevel = level[88];
            }
            if (CurrentLevel == 40 && CurrentStage == 2)
            {
                DefaultLevel = level[89];
            }
            if (CurrentLevel == 41 && CurrentStage == 2)
            {
                DefaultLevel = level[90];
            }
            if (CurrentLevel == 42 && CurrentStage == 2)
            {
                DefaultLevel = level[91];
            }
            if (CurrentLevel == 43 && CurrentStage == 2)
            {
                DefaultLevel = level[92];
            }
            if (CurrentLevel == 44 && CurrentStage == 2)
            {
                DefaultLevel = level[93];
            }
            if (CurrentLevel == 45 && CurrentStage == 2)
            {
                DefaultLevel = level[94];
            }
            if (CurrentLevel == 46 && CurrentStage == 2)
            {
                DefaultLevel = level[95];
            }
            if (CurrentLevel == 47 && CurrentStage == 2)
            {
                DefaultLevel = level[96];
            }
            if (CurrentLevel == 48 && CurrentStage == 2)
            {
                DefaultLevel = level[97];
            }
            if (CurrentLevel == 49 && CurrentStage == 2)
            {
                DefaultLevel = level[98];
            }
            if (CurrentLevel == 50 && CurrentStage == 2)
            {
                DefaultLevel = level[99];
            }


            if (CurrentLevel == 1 && CurrentStage == 3)
            {
                DefaultLevel = level[100];
            }
            if (CurrentLevel == 2 && CurrentStage == 3)
            {
                DefaultLevel = level[101];
            }
            if (CurrentLevel == 3 && CurrentStage == 3)
            {
                DefaultLevel = level[102];
            }
            if (CurrentLevel == 4 && CurrentStage == 3)
            {
                DefaultLevel = level[103];
            }
            if (CurrentLevel == 5 && CurrentStage == 3)
            {
                DefaultLevel = level[104];
            }
            if (CurrentLevel == 6 && CurrentStage == 3)
            {
                DefaultLevel = level[105];
            }
            if (CurrentLevel == 7 && CurrentStage == 3)
            {
                DefaultLevel = level[106];
            }
            if (CurrentLevel == 8 && CurrentStage == 3)
            {
                DefaultLevel = level[107];
            }
            if (CurrentLevel == 9 && CurrentStage == 3)
            {
                DefaultLevel = level[108];
            }
            if (CurrentLevel == 10 && CurrentStage == 3)
            {
                DefaultLevel = level[109];
            }
            if (CurrentLevel == 11 && CurrentStage == 3)
            {
                DefaultLevel = level[110];
            }
            if (CurrentLevel == 12 && CurrentStage == 3)
            {
                DefaultLevel = level[111];
            }
            if (CurrentLevel == 13 && CurrentStage == 3)
            {
                DefaultLevel = level[112];
            }
            if (CurrentLevel == 14 && CurrentStage == 3)
            {
                DefaultLevel = level[113];
            }
            if (CurrentLevel == 15 && CurrentStage == 3)
            {
                DefaultLevel = level[114];
            }
            if (CurrentLevel == 16 && CurrentStage == 3)
            {
                DefaultLevel = level[115];
            }
            if (CurrentLevel == 17 && CurrentStage == 3)
            {
                DefaultLevel = level[116];
            }
            if (CurrentLevel == 18 && CurrentStage == 3)
            {
                DefaultLevel = level[117];
            }
            if (CurrentLevel == 19 && CurrentStage == 3)
            {
                DefaultLevel = level[118];
            }
            if (CurrentLevel == 20 && CurrentStage == 3)
            {
                DefaultLevel = level[119];
            }
            if (CurrentLevel == 21 && CurrentStage == 3)
            {
                DefaultLevel = level[120];
            }
            if (CurrentLevel == 22 && CurrentStage == 3)
            {
                DefaultLevel = level[121];
            }
            if (CurrentLevel == 13 && CurrentStage == 3)
            {
                DefaultLevel = level[122];
            }
            if (CurrentLevel == 14 && CurrentStage == 3)
            {
                DefaultLevel = level[123];
            }
            if (CurrentLevel == 15 && CurrentStage == 3)
            {
                DefaultLevel = level[124];
            }
            if (CurrentLevel == 16 && CurrentStage == 3)
            {
                DefaultLevel = level[125];
            }
            if (CurrentLevel == 17 && CurrentStage == 3)
            {
                DefaultLevel = level[126];
            }
            if (CurrentLevel == 18 && CurrentStage == 3)
            {
                DefaultLevel = level[127];
            }
            if (CurrentLevel == 19 && CurrentStage == 3)
            {
                DefaultLevel = level[128];
            }
            if (CurrentLevel == 20 && CurrentStage == 3)
            {
                DefaultLevel = level[129];
            }
            if (CurrentLevel == 21 && CurrentStage == 3)
            {
                DefaultLevel = level[130];
            }
            if (CurrentLevel == 22 && CurrentStage == 3)
            {
                DefaultLevel = level[131];
            }
            if (CurrentLevel == 23 && CurrentStage == 3)
            {
                DefaultLevel = level[132];
            }
            if (CurrentLevel == 24 && CurrentStage == 3)
            {
                DefaultLevel = level[133];
            }
            if (CurrentLevel == 25 && CurrentStage == 3)
            {
                DefaultLevel = level[134];
            }
            if (CurrentLevel == 26 && CurrentStage == 3)
            {
                DefaultLevel = level[135];
            }
            if (CurrentLevel == 27 && CurrentStage == 3)
            {
                DefaultLevel = level[136];
            }
            if (CurrentLevel == 28 && CurrentStage == 3)
            {
                DefaultLevel = level[137];
            }
            if (CurrentLevel == 29 && CurrentStage == 3)
            {
                DefaultLevel = level[138];
            }
            if (CurrentLevel == 30 && CurrentStage == 3)
            {
                DefaultLevel = level[139];
            }
            if (CurrentLevel == 31 && CurrentStage == 3)
            {
                DefaultLevel = level[140];
            }
            if (CurrentLevel == 32 && CurrentStage == 3)
            {
                DefaultLevel = level[141];
            }
            if (CurrentLevel == 33 && CurrentStage == 3)
            {
                DefaultLevel = level[142];
            }
            if (CurrentLevel == 34 && CurrentStage == 3)
            {
                DefaultLevel = level[143];
            }
            if (CurrentLevel == 35 && CurrentStage == 3)
            {
                DefaultLevel = level[144];
            }
            if (CurrentLevel == 36 && CurrentStage == 3)
            {
                DefaultLevel = level[145];
            }
            if (CurrentLevel == 37 && CurrentStage == 3)
            {
                DefaultLevel = level[146];
            }
            if (CurrentLevel == 38 && CurrentStage == 3)
            {
                DefaultLevel = level[147];
            }
            if (CurrentLevel == 39 && CurrentStage == 3)
            {
                DefaultLevel = level[148];
            }
            if (CurrentLevel == 40 && CurrentStage == 3)
            {
                DefaultLevel = level[149];
            }
            if (CurrentLevel == 41 && CurrentStage == 3)
            {
                DefaultLevel = level[150];
            }
            if (CurrentLevel == 42 && CurrentStage == 3)
            {
                DefaultLevel = level[151];
            }
            if (CurrentLevel == 43 && CurrentStage == 3)
            {
                DefaultLevel = level[152];
            }
            if (CurrentLevel == 44 && CurrentStage == 3)
            {
                DefaultLevel = level[153];
            }
            if (CurrentLevel == 45 && CurrentStage == 3)
            {
                DefaultLevel = level[154];
            }
            if (CurrentLevel == 46 && CurrentStage == 3)
            {
                DefaultLevel = level[155];
            }
            if (CurrentLevel == 47 && CurrentStage == 3)
            {
                DefaultLevel = level[156];
            }
            if (CurrentLevel == 48 && CurrentStage == 3)
            {
                DefaultLevel = level[157];
            }
            if (CurrentLevel == 49 && CurrentStage == 3)
            {
                DefaultLevel = level[158];
            }
            if (CurrentLevel == 50 && CurrentStage == 3)
            {
                DefaultLevel = level[159];
            }


            if (CurrentLevel == 1 && CurrentStage == 4)
            {
                DefaultLevel = level[160];
            }
            if (CurrentLevel == 2 && CurrentStage == 4)
            {
                DefaultLevel = level[161];
            }
            if (CurrentLevel == 3 && CurrentStage == 4)
            {
                DefaultLevel = level[162];
            }
            if (CurrentLevel == 4 && CurrentStage == 4)
            {
                DefaultLevel = level[163];
            }
            if (CurrentLevel == 5 && CurrentStage == 4)
            {
                DefaultLevel = level[164];
            }
            if (CurrentLevel == 6 && CurrentStage == 4)
            {
                DefaultLevel = level[165];
            }
            if (CurrentLevel == 7 && CurrentStage == 4)
            {
                DefaultLevel = level[166];
            }
            if (CurrentLevel == 8 && CurrentStage == 4)
            {
                DefaultLevel = level[167];
            }
            if (CurrentLevel == 9 && CurrentStage == 4)
            {
                DefaultLevel = level[168];
            }
            if (CurrentLevel == 10 && CurrentStage == 4)
            {
                DefaultLevel = level[169];
            }
            if (CurrentLevel == 11 && CurrentStage == 4)
            {
                DefaultLevel = level[170];
            }
            if (CurrentLevel == 12 && CurrentStage == 4)
            {
                DefaultLevel = level[171];
            }
            if (CurrentLevel == 13 && CurrentStage == 4)
            {
                DefaultLevel = level[172];
            }
            if (CurrentLevel == 14 && CurrentStage == 4)
            {
                DefaultLevel = level[173];
            }
            if (CurrentLevel == 15 && CurrentStage == 4)
            {
                DefaultLevel = level[174];
            }
            if (CurrentLevel == 16 && CurrentStage == 4)
            {
                DefaultLevel = level[175];
            }
            if (CurrentLevel == 17 && CurrentStage == 4)
            {
                DefaultLevel = level[176];
            }
            if (CurrentLevel == 18 && CurrentStage == 4)
            {
                DefaultLevel = level[177];
            }
            if (CurrentLevel == 19 && CurrentStage == 4)
            {
                DefaultLevel = level[178];
            }
            if (CurrentLevel == 20 && CurrentStage == 4)
            {
                DefaultLevel = level[179];
            }
            if (CurrentLevel == 21 && CurrentStage == 4)
            {
                DefaultLevel = level[180];
            }
            if (CurrentLevel == 22 && CurrentStage == 4)
            {
                DefaultLevel = level[181];
            }
            if (CurrentLevel == 13 && CurrentStage == 4)
            {
                DefaultLevel = level[182];
            }
            if (CurrentLevel == 14 && CurrentStage == 4)
            {
                DefaultLevel = level[183];
            }
            if (CurrentLevel == 15 && CurrentStage == 4)
            {
                DefaultLevel = level[184];
            }
            if (CurrentLevel == 16 && CurrentStage == 4)
            {
                DefaultLevel = level[185];
            }
            if (CurrentLevel == 17 && CurrentStage == 4)
            {
                DefaultLevel = level[186];
            }
            if (CurrentLevel == 18 && CurrentStage == 4)
            {
                DefaultLevel = level[187];
            }
            if (CurrentLevel == 19 && CurrentStage == 4)
            {
                DefaultLevel = level[188];
            }
            if (CurrentLevel == 20 && CurrentStage == 4)
            {
                DefaultLevel = level[189];
            }
            if (CurrentLevel == 21 && CurrentStage == 4)
            {
                DefaultLevel = level[190];
            }
            if (CurrentLevel == 22 && CurrentStage == 4)
            {
                DefaultLevel = level[191];
            }
            if (CurrentLevel == 23 && CurrentStage == 4)
            {
                DefaultLevel = level[192];
            }
            if (CurrentLevel == 24 && CurrentStage == 4)
            {
                DefaultLevel = level[193];
            }
            if (CurrentLevel == 25 && CurrentStage == 4)
            {
                DefaultLevel = level[194];
            }
            if (CurrentLevel == 26 && CurrentStage == 4)
            {
                DefaultLevel = level[195];
            }
            if (CurrentLevel == 27 && CurrentStage == 4)
            {
                DefaultLevel = level[196];
            }
            if (CurrentLevel == 28 && CurrentStage == 4)
            {
                DefaultLevel = level[197];
            }
            if (CurrentLevel == 29 && CurrentStage == 4)
            {
                DefaultLevel = level[198];
            }
            if (CurrentLevel == 30 && CurrentStage == 4)
            {
                DefaultLevel = level[199];
            }
            if (CurrentLevel == 31 && CurrentStage == 4)
            {
                DefaultLevel = level[200];
            }
            if (CurrentLevel == 32 && CurrentStage == 4)
            {
                DefaultLevel = level[201];
            }
            if (CurrentLevel == 33 && CurrentStage == 4)
            {
                DefaultLevel = level[202];
            }
            if (CurrentLevel == 34 && CurrentStage == 4)
            {
                DefaultLevel = level[203];
            }
            if (CurrentLevel == 35 && CurrentStage == 4)
            {
                DefaultLevel = level[204];
            }
            if (CurrentLevel == 36 && CurrentStage == 4)
            {
                DefaultLevel = level[205];
            }
            if (CurrentLevel == 37 && CurrentStage == 4)
            {
                DefaultLevel = level[206];
            }
            if (CurrentLevel == 38 && CurrentStage == 4)
            {
                DefaultLevel = level[207];
            }
            if (CurrentLevel == 39 && CurrentStage == 4)
            {
                DefaultLevel = level[208];
            }
            if (CurrentLevel == 40 && CurrentStage == 4)
            {
                DefaultLevel = level[209];
            }
            if (CurrentLevel == 41 && CurrentStage == 4)
            {
                DefaultLevel = level[210];
            }
            if (CurrentLevel == 42 && CurrentStage == 4)
            {
                DefaultLevel = level[211];
            }
            if (CurrentLevel == 43 && CurrentStage == 4)
            {
                DefaultLevel = level[212];
            }
            if (CurrentLevel == 44 && CurrentStage == 4)
            {
                DefaultLevel = level[213];
            }
            if (CurrentLevel == 45 && CurrentStage == 4)
            {
                DefaultLevel = level[214];
            }
            if (CurrentLevel == 46 && CurrentStage == 4)
            {
                DefaultLevel = level[215];
            }
            if (CurrentLevel == 47 && CurrentStage == 4)
            {
                DefaultLevel = level[216];
            }
            if (CurrentLevel == 48 && CurrentStage == 4)
            {
                DefaultLevel = level[217];
            }
            if (CurrentLevel == 49 && CurrentStage == 4)
            {
                DefaultLevel = level[218];
            }
            if (CurrentLevel == 50 && CurrentStage == 4)
            {
                DefaultLevel = level[219];
            }


            if (CurrentLevel == 1 && CurrentStage == 5)
            {
                DefaultLevel = level[220];
            }
            if (CurrentLevel == 2 && CurrentStage == 5)
            {
                DefaultLevel = level[221];
            }
            if (CurrentLevel == 3 && CurrentStage == 5)
            {
                DefaultLevel = level[222];
            }
            if (CurrentLevel == 4 && CurrentStage == 5)
            {
                DefaultLevel = level[223];
            }
            if (CurrentLevel == 5 && CurrentStage == 5)
            {
                DefaultLevel = level[224];
            }
            if (CurrentLevel == 6 && CurrentStage == 5)
            {
                DefaultLevel = level[225];
            }
            if (CurrentLevel == 7 && CurrentStage == 5)
            {
                DefaultLevel = level[226];
            }
            if (CurrentLevel == 8 && CurrentStage == 5)
            {
                DefaultLevel = level[227];
            }
            if (CurrentLevel == 9 && CurrentStage == 5)
            {
                DefaultLevel = level[228];
            }
            if (CurrentLevel == 10 && CurrentStage == 5)
            {
                DefaultLevel = level[229];
            }
            if (CurrentLevel == 11 && CurrentStage == 5)
            {
                DefaultLevel = level[230];
            }
            if (CurrentLevel == 12 && CurrentStage == 5)
            {
                DefaultLevel = level[231];
            }
            if (CurrentLevel == 13 && CurrentStage == 5)
            {
                DefaultLevel = level[232];
            }
            if (CurrentLevel == 14 && CurrentStage == 5)
            {
                DefaultLevel = level[233];
            }
            if (CurrentLevel == 15 && CurrentStage == 5)
            {
                DefaultLevel = level[234];
            }
            if (CurrentLevel == 16 && CurrentStage == 5)
            {
                DefaultLevel = level[235];
            }
            if (CurrentLevel == 17 && CurrentStage == 5)
            {
                DefaultLevel = level[236];
            }
            if (CurrentLevel == 18 && CurrentStage == 5)
            {
                DefaultLevel = level[237];
            }
            if (CurrentLevel == 19 && CurrentStage == 5)
            {
                DefaultLevel = level[238];
            }
            if (CurrentLevel == 20 && CurrentStage == 5)
            {
                DefaultLevel = level[239];
            }
            if (CurrentLevel == 21 && CurrentStage == 5)
            {
                DefaultLevel = level[240];
            }
            if (CurrentLevel == 22 && CurrentStage == 5)
            {
                DefaultLevel = level[241];
            }
            if (CurrentLevel == 13 && CurrentStage == 5)
            {
                DefaultLevel = level[242];
            }
            if (CurrentLevel == 14 && CurrentStage == 5)
            {
                DefaultLevel = level[243];
            }
            if (CurrentLevel == 15 && CurrentStage == 5)
            {
                DefaultLevel = level[244];
            }
            if (CurrentLevel == 16 && CurrentStage == 5)
            {
                DefaultLevel = level[245];
            }
            if (CurrentLevel == 17 && CurrentStage == 5)
            {
                DefaultLevel = level[246];
            }
            if (CurrentLevel == 18 && CurrentStage == 5)
            {
                DefaultLevel = level[247];
            }
            if (CurrentLevel == 19 && CurrentStage == 5)
            {
                DefaultLevel = level[248];
            }
            if (CurrentLevel == 20 && CurrentStage == 5)
            {
                DefaultLevel = level[249];
            }
            if (CurrentLevel == 21 && CurrentStage == 5)
            {
                DefaultLevel = level[250];
            }
            if (CurrentLevel == 22 && CurrentStage == 5)
            {
                DefaultLevel = level[251];
            }
            if (CurrentLevel == 23 && CurrentStage == 5)
            {
                DefaultLevel = level[252];
            }
            if (CurrentLevel == 24 && CurrentStage == 5)
            {
                DefaultLevel = level[253];
            }
            if (CurrentLevel == 25 && CurrentStage == 5)
            {
                DefaultLevel = level[254];
            }
            if (CurrentLevel == 26 && CurrentStage == 5)
            {
                DefaultLevel = level[255];
            }
            if (CurrentLevel == 27 && CurrentStage == 5)
            {
                DefaultLevel = level[256];
            }
            if (CurrentLevel == 28 && CurrentStage == 5)
            {
                DefaultLevel = level[257];
            }
            if (CurrentLevel == 29 && CurrentStage == 5)
            {
                DefaultLevel = level[258];
            }
            if (CurrentLevel == 30 && CurrentStage == 5)
            {
                DefaultLevel = level[259];
            }
            if (CurrentLevel == 31 && CurrentStage == 5)
            {
                DefaultLevel = level[260];
            }
            if (CurrentLevel == 32 && CurrentStage == 5)
            {
                DefaultLevel = level[261];
            }
            if (CurrentLevel == 33 && CurrentStage == 5)
            {
                DefaultLevel = level[262];
            }
            if (CurrentLevel == 34 && CurrentStage == 5)
            {
                DefaultLevel = level[263];
            }
            if (CurrentLevel == 35 && CurrentStage == 5)
            {
                DefaultLevel = level[264];
            }
            if (CurrentLevel == 36 && CurrentStage == 5)
            {
                DefaultLevel = level[265];
            }
            if (CurrentLevel == 37 && CurrentStage == 5)
            {
                DefaultLevel = level[266];
            }
            if (CurrentLevel == 38 && CurrentStage == 5)
            {
                DefaultLevel = level[267];
            }
            if (CurrentLevel == 39 && CurrentStage == 5)
            {
                DefaultLevel = level[268];
            }
            if (CurrentLevel == 40 && CurrentStage == 5)
            {
                DefaultLevel = level[269];
            }
            if (CurrentLevel == 41 && CurrentStage == 5)
            {
                DefaultLevel = level[260];
            }
            if (CurrentLevel == 42 && CurrentStage == 5)
            {
                DefaultLevel = level[261];
            }
            if (CurrentLevel == 43 && CurrentStage == 5)
            {
                DefaultLevel = level[262];
            }
            if (CurrentLevel == 44 && CurrentStage == 5)
            {
                DefaultLevel = level[263];
            }
            if (CurrentLevel == 45 && CurrentStage == 5)
            {
                DefaultLevel = level[264];
            }
            if (CurrentLevel == 46 && CurrentStage == 5)
            {
                DefaultLevel = level[265];
            }
            if (CurrentLevel == 47 && CurrentStage == 5)
            {
                DefaultLevel = level[266];
            }
            if (CurrentLevel == 48 && CurrentStage == 5)
            {
                DefaultLevel = level[267];
            }
            if (CurrentLevel == 49 && CurrentStage == 5)
            {
                DefaultLevel = level[268];
            }
            if (CurrentLevel == 50 && CurrentStage == 5)
            {
                DefaultLevel = level[269];
            }

            if (CurrentLevel == 1 && CurrentStage == 6)
            {
                DefaultLevel = level[270];
            }
            if (CurrentLevel == 2 && CurrentStage == 6)
            {
                DefaultLevel = level[271];
            }
            if (CurrentLevel == 3 && CurrentStage == 6)
            {
                DefaultLevel = level[272];
            }
            if (CurrentLevel == 4 && CurrentStage == 6)
            {
                DefaultLevel = level[273];
            }
            if (CurrentLevel == 5 && CurrentStage == 6)
            {
                DefaultLevel = level[274];
            }
            if (CurrentLevel == 6 && CurrentStage == 6)
            {
                DefaultLevel = level[275];
            }
            if (CurrentLevel == 7 && CurrentStage == 6)
            {
                DefaultLevel = level[276];
            }
            if (CurrentLevel == 8 && CurrentStage == 6)
            {
                DefaultLevel = level[277];
            }
            if (CurrentLevel == 9 && CurrentStage == 6)
            {
                DefaultLevel = level[278];
            }
            if (CurrentLevel == 10 && CurrentStage == 6)
            {
                DefaultLevel = level[279];
            }
            if (CurrentLevel == 11 && CurrentStage == 6)
            {
                DefaultLevel = level[280];
            }
            if (CurrentLevel == 12 && CurrentStage == 6)
            {
                DefaultLevel = level[281];
            }
            if (CurrentLevel == 13 && CurrentStage == 6)
            {
                DefaultLevel = level[282];
            }
            if (CurrentLevel == 14 && CurrentStage == 6)
            {
                DefaultLevel = level[283];
            }
            if (CurrentLevel == 15 && CurrentStage == 6)
            {
                DefaultLevel = level[284];
            }
            if (CurrentLevel == 16 && CurrentStage == 6)
            {
                DefaultLevel = level[285];
            }
            if (CurrentLevel == 17 && CurrentStage == 6)
            {
                DefaultLevel = level[286];
            }
            if (CurrentLevel == 18 && CurrentStage == 6)
            {
                DefaultLevel = level[287];
            }
            if (CurrentLevel == 19 && CurrentStage == 6)
            {
                DefaultLevel = level[288];
            }
            if (CurrentLevel == 20 && CurrentStage == 6)
            {
                DefaultLevel = level[289];
            }
            if (CurrentLevel == 21 && CurrentStage == 6)
            {
                DefaultLevel = level[290];
            }
            if (CurrentLevel == 22 && CurrentStage == 6)
            {
                DefaultLevel = level[291];
            }
            if (CurrentLevel == 13 && CurrentStage == 6)
            {
                DefaultLevel = level[292];
            }
            if (CurrentLevel == 14 && CurrentStage == 6)
            {
                DefaultLevel = level[293];
            }
            if (CurrentLevel == 15 && CurrentStage == 6)
            {
                DefaultLevel = level[294];
            }
            if (CurrentLevel == 16 && CurrentStage == 6)
            {
                DefaultLevel = level[295];
            }
            if (CurrentLevel == 17 && CurrentStage == 6)
            {
                DefaultLevel = level[296];
            }
            if (CurrentLevel == 18 && CurrentStage == 6)
            {
                DefaultLevel = level[297];
            }
            if (CurrentLevel == 19 && CurrentStage == 6)
            {
                DefaultLevel = level[298];
            }
            if (CurrentLevel == 20 && CurrentStage == 6)
            {
                DefaultLevel = level[299];
            }
            if (CurrentLevel == 21 && CurrentStage == 6)
            {
                DefaultLevel = level[300];
            }
            if (CurrentLevel == 22 && CurrentStage == 6)
            {
                DefaultLevel = level[301];
            }
            if (CurrentLevel == 23 && CurrentStage == 6)
            {
                DefaultLevel = level[302];
            }
            if (CurrentLevel == 24 && CurrentStage == 6)
            {
                DefaultLevel = level[303];
            }
            if (CurrentLevel == 25 && CurrentStage == 6)
            {
                DefaultLevel = level[304];
            }
            if (CurrentLevel == 26 && CurrentStage == 6)
            {
                DefaultLevel = level[305];
            }
            if (CurrentLevel == 27 && CurrentStage == 6)
            {
                DefaultLevel = level[306];
            }
            if (CurrentLevel == 28 && CurrentStage == 6)
            {
                DefaultLevel = level[307];
            }
            if (CurrentLevel == 29 && CurrentStage == 6)
            {
                DefaultLevel = level[308];
            }
            if (CurrentLevel == 30 && CurrentStage == 6)
            {
                DefaultLevel = level[309];
            }
            if (CurrentLevel == 31 && CurrentStage == 6)
            {
                DefaultLevel = level[310];
            }
            if (CurrentLevel == 32 && CurrentStage == 6)
            {
                DefaultLevel = level[311];
            }
            if (CurrentLevel == 33 && CurrentStage == 6)
            {
                DefaultLevel = level[312];
            }
            if (CurrentLevel == 34 && CurrentStage == 6)
            {
                DefaultLevel = level[313];
            }
            if (CurrentLevel == 35 && CurrentStage == 6)
            {
                DefaultLevel = level[314];
            }
            if (CurrentLevel == 36 && CurrentStage == 6)
            {
                DefaultLevel = level[315];
            }
            if (CurrentLevel == 37 && CurrentStage == 6)
            {
                DefaultLevel = level[316];
            }
            if (CurrentLevel == 38 && CurrentStage == 6)
            {
                DefaultLevel = level[317];
            }
            if (CurrentLevel == 39 && CurrentStage == 6)
            {
                DefaultLevel = level[318];
            }
            if (CurrentLevel == 40 && CurrentStage == 6)
            {
                DefaultLevel = level[319];
            }
            if (CurrentLevel == 41 && CurrentStage == 6)
            {
                DefaultLevel = level[320];
            }
            if (CurrentLevel == 42 && CurrentStage == 6)
            {
                DefaultLevel = level[321];
            }
            if (CurrentLevel == 43 && CurrentStage == 6)
            {
                DefaultLevel = level[322];
            }
            if (CurrentLevel == 44 && CurrentStage == 6)
            {
                DefaultLevel = level[323];
            }
            if (CurrentLevel == 45 && CurrentStage == 6)
            {
                DefaultLevel = level[324];
            }
            if (CurrentLevel == 46 && CurrentStage == 6)
            {
                DefaultLevel = level[325];
            }
            if (CurrentLevel == 47 && CurrentStage == 6)
            {
                DefaultLevel = level[326];
            }
            if (CurrentLevel == 48 && CurrentStage == 6)
            {
                DefaultLevel = level[327];
            }
            if (CurrentLevel == 49 && CurrentStage == 6)
            {
                DefaultLevel = level[328];
            }
            if (CurrentLevel == 50 && CurrentStage == 6)
            {
                DefaultLevel = level[329];
            }

            if (Levels.TryGetValue(levelKey, out var levelData))
            {
                return levelData;
            }
            return DefaultLevel;
        }

        public void Loadlevel()
        {

        }



        private const string MainMenu = "MainMenu";
        private const string Gameplay = "Gameplay";

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(MainMenu);
        }

        public void GoToGameplay()
        {
            SceneManager.LoadScene(Gameplay);
        }

        public void OnClickBackBtn()
        {
            SceneManager.LoadScene("LoginScene");
        }
    }
}
