using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;

public class FacebookLoginManager : MonoBehaviour
{
    //public TextMeshProUGUI FB_userName;
    public string Name;
    //public Image FB_profilePic;
    // public RawImage rawImg;
    public Texture fbProfilepicTexture;

    public bool FBLoginbool = false;

    #region Initialize

    private void Awake()
    {
        //FB.Init(SetInit, onHidenUnity);

        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                else
                    print("Couldn't initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }

    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Facebook is Login!");
            string s = "client token" + FB.ClientToken + "User Id" + AccessToken.CurrentAccessToken.UserId + "token string" + AccessToken.CurrentAccessToken.TokenString;
        }
        else
        {
            Debug.Log("Facebook is not Logged in!");
        }
        DealWithFbMenus(FB.IsLoggedIn);
    }

    void onHidenUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void DealWithFbMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
            SceneManager.LoadScene("MainMenu");
            FBLoginbool = true;
        }
        else
        {
            print("Not logged in");
        }
    }
    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            string name = "" + result.ResultDictionary["first_name"];
            if (Name != null) Name = name;
            Name = name;
            //fbUsernameStr = FB_userName.text;
            Debug.Log("" + name);
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
    void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            Debug.Log("Profile Pic");
            fbProfilepicTexture = result.Texture;
            //fbProfilepicTexture = rawImg.texture;
            //if (FB_profilePic != null) FB_profilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
            /*JSONObject json = new JSONObject(result.RawResult);

            StartCoroutine(DownloadTexture(json["picture"]["data"]["url"].str, profile_texture));*/
        }
        else
        {
            Debug.Log(result.Error);
        }
    }



    #endregion


    //login
    public void Facebook_LogIn()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        //permissions.Add("user_friends");
        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }
    void AuthCallBack(IResult result)
    {
        if (FB.IsLoggedIn)
        {
            SetInit();
            //AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;
            foreach (string perm in aToken.Permissions)
            {
                print(perm);
            }
        }
        else
        {
            Debug.Log("Failed to log in");
        }

    }




    //logout
    public void Facebook_LogOut()
    {
        //StartCoroutine(LogOut());
        LogOut();
    }
    private void LogOut()
    {
        FB.LogOut();
        /* while (FB.IsLoggedIn)
         {
             print("Logging Out");
             yield return null;
         }*/
        // if (FB_profilePic != null) FB_profilePic.sprite = null;
        if (Name != null) Name = "";
        if (fbProfilepicTexture != null) fbProfilepicTexture = null;
        //PlayerPrefs.Save();
        SceneManager.LoadScene("LoginScene");
    }


    #region other
    #endregion

}

