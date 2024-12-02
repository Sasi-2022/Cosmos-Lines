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
    public bool googleLoginbool = false;

    // public GameObject loginPanel, profilePanel;
    private GoogleSignInConfiguration configuration;
    private string webClientId = "1051490529515-k6q6bgda78o0q34au9an9crb5bum7kc8.apps.googleusercontent.com";
    public static GoogleLoginManager instance;
    public TextMeshProUGUI gname;
    public TextMeshProUGUI id;
    public Image Google_userDp;
    public GameObject panel;
    public GameObject GuestBtn;
    public GameObject openpanel;


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

          
            PlayerPrefs.SetString("DisplayName", task.Result.DisplayName);
            PlayerPrefs.SetString("LoadProfilePic", imageURL);
            PlayerPrefs.Save();

            panel.gameObject.SetActive(true);
            openpanel.gameObject.SetActive(true);
            GuestBtn.gameObject.SetActive(false);
            
        }
    }


    IEnumerator LoadProfilePic(string imageUrl)
    {
        WWW www = new WWW(imageUrl);
        yield return www;

        _profilePic = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
        Google_userDp.sprite = _profilePic;

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
        Debug.Log("Calling SignOut");
        // PlayerPrefs.Save();
        GoogleSignIn.DefaultInstance.SignOut();
        SceneManager.LoadScene("LoginScene");
        googleLoginbool = false;
    }
    IEnumerator OpenPanel()
    {
        yield return new WaitForSeconds(0.3f);
        panel.gameObject.SetActive(true);
        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
            OnAssignData, TaskScheduler.Default);

    }

    public void destroy()
    {
        GoogleSignIn.DefaultInstance.SignOut();
        if (this.gameObject != null)
            DestroyImmediate(this.gameObject);
    }
}
