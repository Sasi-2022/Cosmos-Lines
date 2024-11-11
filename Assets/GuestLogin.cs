using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class GuestLogin : MonoBehaviour
{
    private const string GuestDataFileName = "guestData.json"; // JSON file name to store guest data
    public GameObject LoginPanel1;
    public GameObject LoginPanel;

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

    private void Awake()
    {
        localDataPath = Application.persistentDataPath + "/" + GuestDataFileName;
        LoadGuestData(); // Load data on startup if it exists
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
            statusText.text = "Logged in as Guest. ID: " + guestData.guestId;
            Debug.Log("Guest ID created: " + guestData.guestId);
        }
        else
        {
            GuestData guestData = LoadGuestDataFromFile();
            statusText.text = "Already logged in as Guest. ID: " + guestData.guestId;
            Debug.Log("Guest already logged in with ID: " + guestData.guestId);
        }

        LoginPanel1.SetActive(true);
        LoginPanel.SetActive(false); // Hide login panel
    }

    public void OnLogoutButtonClick()
    {
        LoginPanel.gameObject.SetActive(false);
        LoginPanel1.gameObject.SetActive(true);

        // Clear guest data by deleting the JSON file
        if (File.Exists(localDataPath))
        {
            File.Delete(localDataPath);
            Debug.Log("Guest data file deleted.");
        }

        statusText.text = "Logged out.";
        Debug.Log("Guest data cleared");
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
}
