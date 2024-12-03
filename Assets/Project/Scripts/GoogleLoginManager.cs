using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Google;

public class GoogleLoginManager : MonoBehaviour
{
    public string imageURL;
    public string userNameStr;
    public Sprite _profilePic;
    public string Name;
    public bool googleLoginbool = false;
    private GoogleSignInConfiguration configuration;
   // private string webClientId = "YOUR_WEB_CLIENT_ID";  // Replace with your actual Web Client ID
    private string webClientId = "1051490529515-k6q6bgda78o0q34au9an9crb5bum7kc8.apps.googleusercontent.com";

    public static GoogleLoginManager instance;
    public TextMeshProUGUI gname;
    public TextMeshProUGUI id;
    public Image Google_userDp;
    public GameObject panel;
    public GameObject GuestBtn;
    public GameObject openpanel;

    private const string GoogleUserNameKey = "GoogleUserNameKey";
    private const string GoogleUserIdKey = "GoogleUserIdKey";
    private const string GoogleUserDpKey = "GoogleUserDpKey";

    void Awake()
    {
        Debug.Log("Elan google login manager Awake");
        // Debug.Log("GoogleLoginManager Awake==>" + GlobalManager.Instance); 
        // GlobalManager.Instance.googleLoginManager = this;
        // Don't destroy game object in Awake if you don't need this across scenes
        // DontDestroyOnLoad(gameObject);

        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true,
            RequestEmail = true,
            RequestAuthCode = true
        };
    }

    private void Start()
    {
        Debug.Log("Elan google login manager Start");

        //GlobalManager.Instance.InitializeGoogleLogin();
        googleLoginbool = PlayerPrefs.GetInt("googleLoginbool", 0) == 1;
        Debug.Log("Elan google login manager Star===>"+ googleLoginbool);
        if (googleLoginbool)
        {
            Debug.Log("Elan google login manager Star 11111===>" + googleLoginbool);
            LoadGoogleData();
            if (panel != null) panel.SetActive(true);
            if (openpanel != null) openpanel.SetActive(true);
            if (GuestBtn != null) GuestBtn.SetActive(false);
            Debug.Log("Elan google login manager Star 222222===>" + googleLoginbool);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
       // StartCoroutine(OpenPanel());
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        Debug.Log("Elan google login manager OnAuthenticationFinished===>");
        if (task.IsFaulted)
        {
            Debug.Log("Elan google login manager OnAuthenticationFinished 11111===>");
            Debug.LogError("Google SignIn failed.");
            foreach (var exception in task.Exception.InnerExceptions)
            {
                var error = (GoogleSignIn.SignInException)exception;
                Debug.LogError("Error: " + error.Status + " " + error.Message);
            }
        }
        else if (task.IsCanceled)
        {
            Debug.Log("Elan google login manager OnAuthenticationFinished 22222===>");
            Debug.LogError("Google SignIn was canceled.");
        }
        else
        {
            Debug.Log("Elan google login manager OnAuthenticationFinished 333333===>");
            userNameStr = task.Result.DisplayName;
            gname.text = userNameStr;
            id.text = task.Result.IdToken;
            Debug.Log("Elan google login manager OnAuthenticationFinished 444444===>");
            googleLoginbool = true;
            PlayerPrefs.SetInt("googleLoginbool", googleLoginbool ? 1 : 0);
            PlayerPrefs.SetString(GoogleUserNameKey, task.Result.DisplayName);
            PlayerPrefs.SetString(GoogleUserIdKey, task.Result.IdToken);
            PlayerPrefs.Save();
            Debug.Log("Elan google login manager OnAuthenticationFinished 55555===>");
            if (panel != null) panel.SetActive(true);
            Debug.Log("Elan google login manager OnAuthenticationFinished 55555 aaaaaa===>");
            if (openpanel != null) openpanel.SetActive(true);
            Debug.Log("Elan google login manager OnAuthenticationFinished 55555 bbbbbb===>");
            if (GuestBtn != null) GuestBtn.SetActive(false);
            Debug.Log("Elan google login manager OnAuthenticationFinished 66666===>");
        }
    }

    internal void OnAssignData(Task<GoogleSignInUser> task)
    {
        userNameStr = task.Result.DisplayName;
        gname.text = userNameStr;
        id.text = task.Result.IdToken;
    }

    public void OnSignOut()
    {
        Debug.Log("Elan google login manager OnSignOut 11111===>");
        PlayerPrefs.DeleteKey(GoogleUserNameKey);
        PlayerPrefs.DeleteKey(GoogleUserIdKey);
        PlayerPrefs.DeleteKey(GoogleUserDpKey);
        PlayerPrefs.DeleteKey("googleLoginbool");
        PlayerPrefs.Save();
        Debug.Log("Elan google login manager OnSignOut 22222===>");
        GoogleSignIn.DefaultInstance.SignOut();
        googleLoginbool = false;
        ResetUserData();
        Debug.Log("Elan google login manager OnSignOut 33333===>");
        if (panel != null) panel.SetActive(false);
        if (openpanel != null) openpanel.SetActive(false);
        if (GuestBtn != null) GuestBtn.SetActive(true);
        Debug.Log("Elan google login manager OnSignOut 44444===>");
    }

    private void ResetUserData()
    {
        if (gname != null) gname.text = "New Text";
        else Debug.LogError("gname is null before resetting!");
    }

    IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(0.3f);
        panel.gameObject.SetActive(true);
        //GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAssignData, TaskScheduler.Default);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetUserData();
    }

    public void LoadGoogleData()
    {
        Debug.Log("Elan google login manager LoadGoogleData 11111===>");
        if (PlayerPrefs.HasKey(GoogleUserNameKey))
        {
            Debug.Log("Elan google login manager LoadGoogleData 22222===>");
            string savedName = PlayerPrefs.GetString(GoogleUserNameKey);
            gname.text = savedName;
            Debug.Log("Elan google login manager LoadGoogleData 33333===>"+ savedName);
        }

        if (PlayerPrefs.HasKey(GoogleUserDpKey))
        {
            Debug.Log("Elan google login manager LoadGoogleData 4444===>");
            string savedProfilePicUrl = PlayerPrefs.GetString(GoogleUserDpKey);
            StartCoroutine(LoadProfilePic(savedProfilePicUrl));
            Debug.Log("Elan google login manager LoadGoogleData 5555===>");
        }
        else
        {
            Debug.Log("No Google data found in PlayerPrefs.");
        }
    }

    IEnumerator LoadProfilePic(string imageUrl)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            _profilePic = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            Google_userDp.sprite = _profilePic;

            PlayerPrefs.SetString(GoogleUserDpKey, imageUrl);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("Failed to load profile picture: " + www.error);
        }
    }
}
