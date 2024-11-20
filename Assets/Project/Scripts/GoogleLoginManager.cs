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
    public string webClientId = "1051490529515-k6q6bgda78o0q34au9an9crb5bum7kc8.apps.googleusercontent.com";
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
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;
        Debug.LogError("Calling SignIn");

        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
            OnAuthenticationFinished, TaskScheduler.Default);

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
            imageURL = task.Result.ImageUrl?.ToString();
            StartCoroutine(LoadProfilePic(task.Result.ImageUrl.ToString()));
            googleLoginbool = true;

            //Name = task.Result.DisplayName;

            //userNameStr = userNameTxt.text;
            //userEmailTxt.text = "" + task.Result.Email;
            PlayerPrefs.SetString("DisplayName", task.Result.DisplayName);
            PlayerPrefs.SetString("LoadProfilePic", imageURL);
            PlayerPrefs.Save();
        }
    }


    IEnumerator LoadProfilePic(string imageUrl)
    {
        WWW www = new WWW(imageUrl);
        yield return www;

        _profilePic = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        SceneManager.LoadScene("MainMenu");
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
        SceneManager.LoadScene("LoginScene");
        googleLoginbool = false;
    }
}
