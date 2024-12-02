using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoutManager : MonoBehaviour
{
    public Button logoutBtn;
    public Button playBtn;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void LogOutButtonClk()
    {
        if (GoogleLoginManager.instance.googleLoginbool == true)
        {
            GoogleLoginManager.instance.OnSignOut();
        }
        else if (FaceBookLogin.instance.FBLoginbool == true)
        {
            FaceBookLogin.instance.LogOut();
        }
        else if (GuestLogin.instance.guestlogin == true);
        {
            GuestLogin.instance.OnLogoutButtonClick();
        }
    }

    public void OnClickPlayBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

