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
    public GameObject defaultAvatar;
    private GoogleSignInConfiguration configuration;
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
        Debug.Log("Google login manager Awake");

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
        Debug.Log("Google login manager Start");

        GlobalManager.Instance.InitializeGoogleLogin();
        googleLoginbool = PlayerPrefs.GetInt("googleLoginbool", 0) == 1;
        if (googleLoginbool)
        {
            LoadGoogleData();
            StartCoroutine(ShowPanels());
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
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Google SignIn failed.");
            foreach (var exception in task.Exception.InnerExceptions)
            {
                var error = (GoogleSignIn.SignInException)exception;
                Debug.LogError("Error: " + error.Status + " " + error.Message);
            }
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Google SignIn was canceled.");
        }
        else
        {
            userNameStr = task.Result.DisplayName;
            gname.text = userNameStr;
            id.text = task.Result.IdToken;

            googleLoginbool = true;
            PlayerPrefs.SetInt("googleLoginbool", googleLoginbool ? 1 : 0);
            PlayerPrefs.SetString(GoogleUserNameKey, task.Result.DisplayName);
            PlayerPrefs.SetString(GoogleUserIdKey, task.Result.IdToken);
            PlayerPrefs.Save();

            StartCoroutine(ShowPanels());
            if (defaultAvatar != null)
                defaultAvatar.SetActive(task.Result.ImageUrl == null);
        }
    }

    public void OnSignOut()
    {
        PlayerPrefs.DeleteKey(GoogleUserNameKey);
        PlayerPrefs.DeleteKey(GoogleUserIdKey);
        PlayerPrefs.DeleteKey(GoogleUserDpKey);
        PlayerPrefs.DeleteKey("googleLoginbool");
        PlayerPrefs.Save();

        GoogleSignIn.DefaultInstance.SignOut();
        googleLoginbool = false;
        ResetUserData();

        StartCoroutine(HidePanels());
    }

    private void ResetUserData()
    {
        if (gname != null) gname.text = "New Text";
    }

    private IEnumerator ShowPanels()
    {
        yield return new WaitForEndOfFrame();  // Wait until the frame is fully rendered
        if (panel != null) panel.SetActive(true);
        if (openpanel != null) openpanel.SetActive(true);
        if (GuestBtn != null) GuestBtn.SetActive(false);
    }

    private IEnumerator HidePanels()
    {
        yield return new WaitForEndOfFrame();  // Wait until the frame is fully rendered
        if (panel != null) panel.SetActive(false);
        if (openpanel != null) openpanel.SetActive(false);
        if (GuestBtn != null) GuestBtn.SetActive(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetUserData();
    }

    public void LoadGoogleData()
    {
        if (PlayerPrefs.HasKey(GoogleUserNameKey))
        {
            string savedName = PlayerPrefs.GetString(GoogleUserNameKey);
            gname.text = savedName;
        }

        if (PlayerPrefs.HasKey(GoogleUserDpKey))
        {
            string savedProfilePicUrl = PlayerPrefs.GetString(GoogleUserDpKey);
            StartCoroutine(LoadProfilePic(savedProfilePicUrl));
        }
    }

    private IEnumerator LoadProfilePic(string imageUrl)
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
