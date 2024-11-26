using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public Button logoutBtn;
    public Button playBtn;

    private void OnEnable()
    {
        logoutBtn.onClick.AddListener(LogOutButtonClk);
    }

    void Start()
    {
        
    }

    public void LogOutButtonClk()
    {
        if (FaceBookLogin.instance.FBLoginbool == true)
        {
            FaceBookLogin.instance.LogOut();
        }
        if(SigninSampleScript.instance.googleLoginbool == true)
        {
            SigninSampleScript.instance.OnSignOut();
        }
        if(GuestLogin.instance.guestlogin == true)
        {
            GuestLogin.instance.OnLogoutButtonClick();
        }
    }
    public void OnClickPlayBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
