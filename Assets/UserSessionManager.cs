using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSessionManager : MonoBehaviour
{
    public static UserSessionManager Instance { get; private set; }

    public string UserId { get; set; }
//    public LoginType LoginType { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }
}
