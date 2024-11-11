using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.IO;

public class SigninSampleScript : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public GameObject LoginPanel;
    public string webClientId = "469113767278-gmqd22a84r6ebdn7106kjgm8i6qje3q9.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    private string localDataPath;

    // Serializable class for saving user data as JSON
    [System.Serializable]
    public class UserData
    {
        public string displayName;
        public string email;
        public string userId;
    }

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true
        };

        // Set up path for saving the data locally
        localDataPath = Application.persistentDataPath + "/userData.json";
    }

    private void Start()
    {
        LoadUserData(); // Attempt to load user data when the app starts
    }

    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddStatusText("Calling SignIn");
        GoogleSignIn.DefaultInstance.SignOut();
        StartCoroutine(SignInCoroutine());
    }

    IEnumerator SignInCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    public void OnSignOut()
    {
        Debug.Log("signout");
        AddStatusText("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
        PlayerPrefs.DeleteKey("USERNAME");
        PlayerPrefs.DeleteKey("LOGIN");
        // Clear saved user data file
        if (File.Exists(localDataPath))
        {
            File.Delete(localDataPath);
        }

        LoginPanel.gameObject.SetActive(true);
    }

    public void OnDisconnect()
    {
        AddStatusText("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.Log("task.IsFaulted ");
            AddStatusText("Got Error: " + task.Exception.Message);
            LoginPanel.gameObject.SetActive(true);
        }
        else if (task.IsCanceled)
        {
            Debug.Log("task.IsCanceled");
            AddStatusText("Canceled");
            LoginPanel.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("task.success ");
            AddStatusText("Welcome: " + task.Result.DisplayName + "!");
            // Save user data locally (PlayerPrefs and JSON file)
            SaveUserData(task.Result);
            LoginPanel.gameObject.SetActive(true);
        }
    }

    public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddStatusText("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently()
              .ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        AddStatusText("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
          OnAuthenticationFinished);
    }

    private List<string> messages = new List<string>();
    void AddStatusText(string text)
    {
        statusText.text += text + "\n";
        Debug.Log(statusText.text);
    }

    // Save user data locally in a JSON file
    private void SaveUserData(GoogleSignInUser user)
    {
        UserData userData = new UserData
        {
            displayName = user.DisplayName,
            email = user.Email,
            userId = user.UserId
        };

        // Serialize to JSON and save to file
        string jsonData = JsonUtility.ToJson(userData, true);
        File.WriteAllText(localDataPath, jsonData);
        Debug.Log("User data saved to file.");

        // Also store basic info in PlayerPrefs (optional)
        PlayerPrefs.SetString("USERNAME", user.DisplayName);
        PlayerPrefs.SetString("EMAIL", user.Email);
        PlayerPrefs.SetString("USER_ID", user.UserId);
        PlayerPrefs.Save();
    }

    // Load user data from the JSON file
    private void LoadUserData()
    {
        if (File.Exists(localDataPath))
        {
            string jsonData = File.ReadAllText(localDataPath);
            UserData loadedData = JsonUtility.FromJson<UserData>(jsonData);
            AddStatusText("Welcome back: " + loadedData.displayName);
        }
        else
        {
            Debug.Log("No user data found. Please log in.");
        }
    }
}