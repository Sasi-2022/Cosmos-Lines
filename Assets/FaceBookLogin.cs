using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class FaceBookLogin : MonoBehaviour
{
    public TextMeshProUGUI FB_userName;
    public TextMeshProUGUI FB_userId;
    public Image FB_userDp;
    private string localDataPath;
    public static FaceBookLogin instance;
    public GameObject LoginPanel;
    public GameObject LoginPanel1;
    public Button Next;

    // Serializable class to hold user data for JSON
    [System.Serializable]
    public class UserData
    {
        public string userId;
        public string userName;
        public string pictureURL;
    }

    private void Awake()
    {
        localDataPath = Application.persistentDataPath + "/fbUserData.json";

        // Initialize Facebook SDK
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback);
        }
        else
        {
            FB.ActivateApp();
        }

        // Load local data if available
        LoadLocalData();
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize the Facebook SDK");
        }
    }

    public void Login()
    {
        if (!FB.IsLoggedIn)
        {
            FB.LogInWithReadPermissions(new List<string> { "public_profile", "email" }, LoginCallback);
        }
        else
        {
            Debug.Log("Already logged in to Facebook");
        }
    }

    public void LogOut()
    {
        if (FB.IsLoggedIn)
        {
            FB.LogOut();
            LoginPanel1.SetActive(true);
            LoginPanel.SetActive(false);

            ResetUserData();
            DeleteLocalData(); // Remove local data on logout
        }
        else
        {
            Debug.Log("Not logged in to Facebook");
        }
    }

    private void ResetUserData()
    {
        FB_userName.text = "New User";
        FB_userId.text = "ID";
        FB_userDp.sprite = null;
    }

    private void LoginCallback(ILoginResult result)
    {
        if (result.Cancelled)
        {
            Debug.Log("Facebook login cancelled");
        }
        else if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Facebook login error: " + result.Error);
        }
        else if (FB.IsLoggedIn)
        {
            Debug.Log("Facebook login successful");

            // Retrieve user data
            FB.API("/me?fields=id,first_name,last_name,email", HttpMethod.GET, UserDataCallback);
            LoginPanel.SetActive(true);
        }
    }

    private void UserDataCallback(IGraphResult result)
    {
        if (result.Error != null)
        {
            Debug.Log("Error retrieving user data: " + result.Error);
        }
        else
        {
            var userData = result.ResultDictionary;
            string userId = userData["id"].ToString();
            string firstName = userData["first_name"].ToString();
            FB_userName.text = firstName;
            FB_userId.text = userId;

            // Save data locally
            SaveLocalData(userId, firstName);

            FB.API("/me/picture?redirect=false&type=large", HttpMethod.GET, ProfilePictureCallback);
        }
    }

    private void ProfilePictureCallback(IGraphResult result)
    {
        if (result.Error != null)
        {
            Debug.Log("Error retrieving profile picture: " + result.Error);
        }
        else
        {
            var pictureData = result.ResultDictionary["data"] as Dictionary<string, object>;
            string pictureURL = pictureData["url"].ToString();

            Debug.Log("Profile Picture URL: " + pictureURL);
            StartCoroutine(FetchProfilePicture(pictureURL));
        }
    }

    private IEnumerator FetchProfilePicture(string pictureURL)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(pictureURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(www);
            FB_userDp.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Save profile picture URL locally
            SaveLocalData(FB_userId.text, FB_userName.text, pictureURL);
        }
        else
        {
            Debug.Log("Error fetching profile picture: " + www.error);
        }
    }

    private void SaveLocalData(string userId, string userName, string pictureURL = "")
    {
        UserData userData = new UserData
        {
            userId = userId,
            userName = userName,
            pictureURL = pictureURL
        };

        string jsonData = JsonUtility.ToJson(userData);
        File.WriteAllText(localDataPath, jsonData);
        Debug.Log("Data saved locally.");
    }

    private void LoadLocalData()
    {
        if (File.Exists(localDataPath))
        {
            string jsonData = File.ReadAllText(localDataPath);
            UserData userData = JsonUtility.FromJson<UserData>(jsonData);

            FB_userId.text = userData.userId;
            FB_userName.text = userData.userName;

            if (!string.IsNullOrEmpty(userData.pictureURL))
            {
                StartCoroutine(FetchProfilePicture(userData.pictureURL));
            }

            Debug.Log("Data loaded from local storage.");
        }
        else
        {
            Debug.Log("No local data found.");
        }
    }

    private void DeleteLocalData()
    {
        if (File.Exists(localDataPath))
        {
            File.Delete(localDataPath);
            Debug.Log("Local data deleted.");
        }
    }

    public void NextBTN()
    {
        SceneManager.LoadScene("MainMenu");
    }
}