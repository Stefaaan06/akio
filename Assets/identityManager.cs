using TMPro;
using UnityEngine;
using UnityEngine;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;
using Unity.Services.Core;

public class identityManager : MonoBehaviour
{
    public string leaderboardID = "time";
    public GameObject Menu;
    public GameObject Login;
    
    public TMP_InputField name;
    private async void Awake()
    {
        Menu.SetActive(false);
        await UnityServices.InitializeAsync();
        if(!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        else
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerName);
        }
        if(PlayerPrefs.GetInt("Login") == 1)
        {
            Menu.SetActive(true);
            Login.SetActive(false);
        }
        else
        {
            LoginSceen();
        }
    }

    public void LoginSceen()
    {
        Login.SetActive(true);
        Menu.SetActive(false);
    }
    
    public async void SetUserName()
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(name.text);
        Menu.SetActive(true);
        Login.SetActive(false);
        Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerName);
        PlayerPrefs.SetInt("Login", 1);

    }
}
