using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class GuestLogin : MonoBehaviour
{
    private const string GuestDataFileName = "guestData.json"; // JSON file name to store guest data
    public GameObject LoginPanel;
    public GameObject OpenLoginPanel;
    public bool guestlogin = false;
    public static GuestLogin instance;
    public GameObject hide;
    public GameObject guestNameImage;
    public TextMeshProUGUI statusText; // UI Text element to display login status

    // Serializable class for saving guest data as JSON
    [System.Serializable]
    public class GuestData
    {
        public string guestId;
        public int gameProgress;
        public string settings;
    }

    private string localDataPath;

    //private const string GuestUserNameKey = "FBUserName";
    private const string GuestUserIdKey = "GuestUserId";
    //private const string FBUserDpKey = "FBUserDp"; // Save the profile picture URL

    private void Awake()
    {
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }*/
        //GlobalManager.Instance.guestLogin = this;
        //DontDestroyOnLoad(gameObject);
        localDataPath = Application.persistentDataPath + "/" + GuestDataFileName;
        LoadGuestData(); // Load data on startup if it exists
    }

    private void Start()
    {
        GlobalManager.Instance.InitializeGuestLogin();

        guestlogin = PlayerPrefs.GetInt("guestloginbool", 0) == 1;
        Debug.Log("Elan Guestlogin manager Start ==>" + guestlogin);
        if (guestlogin)
        {
            Debug.Log("Elan Guestlogin manager Start 1111 ==>" + guestlogin);
            LoadGuestData();
            LoginPanel.SetActive(true);
            guestNameImage.gameObject.SetActive(true);
            hide.gameObject.SetActive(false);
            guestlogin = true;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnGuestLoginButtonClick()
    {
        if (!File.Exists(localDataPath))
        {
            // Generate a new guest ID, game progress, and settings
            GuestData guestData = new GuestData
            {
                guestId = System.Guid.NewGuid().ToString(),
                gameProgress = 0, // Initial progress
                settings = "DefaultSettings" // Default settings
            };

            SaveGuestData(guestData); // Save the new guest data to the JSON file
            statusText.text = "Guest. ID: " + guestData.guestId;
            Debug.Log("Guest ID created: " + guestData.guestId);
        }
        else
        {
            GuestData guestData = LoadGuestDataFromFile();
            statusText.text = "Already logged in as Guest. ID: " + guestData.guestId;
            Debug.Log("Guest already logged in with ID: " + guestData.guestId);
        }

        LoginPanel.SetActive(true);
        guestNameImage.gameObject.SetActive(true);
        hide.gameObject.SetActive(false);
        guestlogin = true;

        PlayerPrefs.SetInt("guestloginbool", guestlogin ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnLogoutButtonClick()
    {
        

        // OpenLoginPanel.gameObject.SetActive(true);

        PlayerPrefs.DeleteKey("guestloginbool");
        PlayerPrefs.Save();

        guestlogin = false;

        ResetUserData();
        Debug.Log("Guest data cleared");
        

        if (LoginPanel != null) LoginPanel.gameObject.SetActive(false);
        if (hide != null) hide.gameObject.SetActive(true);
        if (guestNameImage != null) guestNameImage.gameObject.SetActive(false);

        // Clear guest data by deleting the JSON file
        if (File.Exists(localDataPath))
        {
            File.Delete(localDataPath);
            Debug.Log("Guest data file deleted.");
        }
        
       
    }

    private void ResetUserData()
    {
        if (statusText != null)
        {
            statusText.text = "New Text";
        }
        else
        {
            Debug.LogError("FB_userName is null before resetting!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        ResetUserData();  // Clear UI components if logged out
    }

    public void SaveGameProgress(int progress)
    {
        GuestData guestData = LoadGuestDataFromFile();
        guestData.gameProgress = progress; // Update progress
        SaveGuestData(guestData); // Save updated guest data
        Debug.Log("Game progress saved: " + progress);
    }

    public int LoadGameProgress()
    {
        GuestData guestData = LoadGuestDataFromFile();
        return guestData.gameProgress; // Return the stored game progress
    }

    public void SaveSettings(string settings)
    {
        GuestData guestData = LoadGuestDataFromFile();
        guestData.settings = settings; // Update settings
        SaveGuestData(guestData); // Save updated guest data
        Debug.Log("Settings saved: " + settings);
    }

    public string LoadSettings()
    {
        GuestData guestData = LoadGuestDataFromFile();
        return guestData.settings; // Return the stored settings
    }

    // Save guest data to JSON file
    private void SaveGuestData(GuestData guestData)
    {
        string jsonData = JsonUtility.ToJson(guestData, true); // Serialize guest data to JSON
        File.WriteAllText(localDataPath, jsonData); // Write to file
        Debug.Log("Guest data saved to file.");
    }

    // Load guest data from JSON file
    private GuestData LoadGuestDataFromFile()
    {
        if (File.Exists(localDataPath))
        {
            string jsonData = File.ReadAllText(localDataPath);
            return JsonUtility.FromJson<GuestData>(jsonData); // Deserialize JSON to GuestData object
        }
        else
        {
            Debug.LogError("Guest data file not found.");
            return null; // Return null if the file does not exist
        }
    }

    // Load guest data on game start if file exists
    private void LoadGuestData()
    {
        if (File.Exists(localDataPath))
        {
            GuestData guestData = LoadGuestDataFromFile();
            statusText.text = "Logged in as Guest. ID: " + guestData.guestId;
            Debug.Log("Loaded guest data: " + guestData.guestId);
        }
        else
        {
            Debug.Log("No guest data found. Please log in as a guest.");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}