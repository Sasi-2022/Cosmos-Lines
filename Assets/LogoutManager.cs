using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoutManager : MonoBehaviour
{
    public Button logoutBtn;
    public Button playBtn;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        /*if(GoogleLoginManager.instance.googleLoginbool == true || FaceBookLogin.instance.FBLoginbool == true || GuestLogin.instance.guestlogin == true)
        {
             panel.gameObject.SetActive(true);
        }*/
      Debug.Log("Elan LogoutManager ===> " + GlobalManager.Instance.faceBookLogin);
       //Debug.Log("Elan LogoutManager11111 ===> " + FaceBookLogin.instance.FBLoginbool);

        if (GlobalManager.Instance.faceBookLogin != null)
        {
            Debug.Log("Elan LogoutManager 1111===> " + GlobalManager.Instance.faceBookLogin);
           // FaceBookLogin.instance.LoadFacebookData();
        }

    }
    public void LogOutButtonClk()
    {
        Debug.Log("Elan comes Logoutmanager");
        Debug.Log("ELan Logoutmanager ===>" + GlobalManager.Instance.faceBookLogin);
        if (GlobalManager.Instance.googleLoginManager.googleLoginbool == true)
        {
            GlobalManager.Instance.googleLoginManager.OnSignOut();
        }
        else if (GlobalManager.Instance.faceBookLogin.FBLoginbool == true)
        {
            GlobalManager.Instance.faceBookLogin.LogOut();
        }
        else if (GlobalManager.Instance.guestLogin.guestlogin == true)
        {
            GlobalManager.Instance.guestLogin.OnLogoutButtonClick();
        }
    }

    public void OnClickPlayBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

