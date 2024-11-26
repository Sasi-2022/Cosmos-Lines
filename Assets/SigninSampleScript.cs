using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Google;
using System.Threading.Tasks;
using UnityEngine.Networking;
//using UnityEngine.SceneManagement;
//using BizzyBeeGames.DotConnect;

public class SigninSampleScript : MonoBehaviour
{
    public static SigninSampleScript instance;
    public string imageURL;
    // public TextMeshProUGUI userNameTxt, userEmailTxt;
    public string userNameStr;
    public Sprite _profilePic;
    public string Name;
    //private const string GUEST_LOGIN_KEY = "GuestLogin";
    public bool googleLoginbool = false;
    public string guestId;
    public GameObject panel;
    public bool FacebookLogin = false;
    public TextMeshProUGUI gname;
    public TextMeshProUGUI id;
    public Image Google_userDp;


    // public GameObject loginPanel, profilePanel;
    private GoogleSignInConfiguration configuration;
    public string webClientId = "428715215399-mojnuu26nd7ge9oqvbi2n4slt8n87rib.apps.googleusercontent.com";



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //instance = null;
            Destroy(gameObject);
        }

    }

    private void OnEnable()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true,
            UseGameSignIn = false,
            RequestEmail = true
        };

    }

    private void Start()
    {

    }


    public void OnSignIn()
    {
        //  LoadingScreenManager.Instance.ShowLoadingScreen("Signing in progress...");
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;
        Debug.LogError("Calling SignIn");

        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
            OnAuthenticationFinished, TaskScheduler.Default);
        StartCoroutine(OpenPanel());
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<System.Exception> enumerator =
                task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error =
                        (GoogleSignIn.SignInException)enumerator.Current;
                    Debug.LogError("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    Debug.LogError("Got unexpected exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Cancelled");
        }
        else
        {
            userNameStr = "" + task.Result.DisplayName;
            gname.text = userNameStr;
            Debug.LogError("Welcome: " + task.Result.DisplayName + "!");
            id.text = task.Result.IdToken;
            imageURL = task.Result.ImageUrl?.ToString();
            StartCoroutine(LoadProfilePic(task.Result.ImageUrl.ToString()));
            googleLoginbool = true;

            //Name = task.Result.DisplayName;

            //userNameStr = userNameTxt.text;
            //userEmailTxt.text = "" + task.Result.Email;
            PlayerPrefs.SetString("DisplayName", task.Result.DisplayName);
            PlayerPrefs.SetString("LoadProfilePic", imageURL);
            PlayerPrefs.Save();

            // UserSessionManager.Instance.LoginType = LoginType.Google;
            // UserSessionManager.Instance.UserId = task.Result.UserId;
            //PlayerPrefs.SetString("GProfilePic", task.Result.ImageUrl);
            //SceneManager.LoadScene("Main");
        }
    }


    IEnumerator LoadProfilePic(string imageUrl)
    {
        WWW www = new WWW(imageUrl);
        yield return www;

        _profilePic = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        Google_userDp.sprite = _profilePic;
        // panel.gameObject.SetActive(true);
    }
    internal void OnAssignData(Task<GoogleSignInUser> task)
    {
        userNameStr = "" + task.Result.DisplayName;
        gname.text = userNameStr;
        id.text = task.Result.IdToken;
    }

    public void OnSignOut()
    {
        if (userNameStr != null) userNameStr = "";
        //userEmailTxt.text = "";

        if (imageURL != null) imageURL = "";
        //loginPanel.SetActive(true);
        //profilePanel.SetActive(false);
        Debug.Log("Calling SignOut");
        // PlayerPrefs.Save();
        GoogleSignIn.DefaultInstance.SignOut();
        panel.gameObject.SetActive(false);
        googleLoginbool = false;
    }
    IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(0.3f);
        panel.gameObject.SetActive(true);
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
            OnAssignData, TaskScheduler.Default);

    }
}
