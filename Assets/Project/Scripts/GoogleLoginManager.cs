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
    // public TextMeshProUGUI userNameTxt, userEmailTxt;
    public string userNameStr;
    public Sprite _profilePic;
    public string Name;
    public bool googleLoginbool;

    // public GameObject loginPanel, profilePanel;
    private GoogleSignInConfiguration configuration;
    private string webClientId = "1051490529515-k6q6bgda78o0q34au9an9crb5bum7kc8.apps.googleusercontent.com";
    public static GoogleLoginManager instance;


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

        configuration = new GoogleSignInConfiguration
        {
            WebClientId = webClientId,
            RequestIdToken = true,
            RequestEmail = true,
            RequestAuthCode = true
        };

    }

    private void OnEnable()
    {
        

    }

    private void Start()
    {

    }


    public void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        Debug.LogError("Calling SignIn");
     
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);

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
            Debug.LogError("Welcome: " + task.Result.DisplayName + "!");

            // Check if ImageUrl is not null before accessing OriginalString
            if (task.Result.ImageUrl != null)
            {
                imageURL = task.Result.ImageUrl.OriginalString;
                StartCoroutine(LoadProfilePic(imageURL));
            }
            else
            {
                Debug.Log("GoogleLoginManager OnAuthenticationFinished: No profile picture available.");
            }

            googleLoginbool = true;

            PlayerPrefs.SetString("DisplayName", task.Result.DisplayName);
            PlayerPrefs.SetString("LoadProfilePic", imageURL);
            PlayerPrefs.Save();

            SceneManager.LoadScene("MainMenu");
        }
    }


    IEnumerator LoadProfilePic(string imageUrl)
    {
        WWW www = new WWW(imageUrl);
        yield return www;

        _profilePic = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));

    }

    public void OnSignOut()
    {
        if (userNameStr != null) userNameStr = "";
        //userEmailTxt.text = "";

        if (imageURL != null) imageURL = "";
        Debug.Log("Calling SignOut");
        // PlayerPrefs.Save();
        GoogleSignIn.DefaultInstance.SignOut();
        SceneManager.LoadScene("LoginScene");
        googleLoginbool = false;
    }

    public void destroy()
    {
        GoogleSignIn.DefaultInstance.SignOut();
        if (this.gameObject != null)
            DestroyImmediate(this.gameObject);
    }
}
