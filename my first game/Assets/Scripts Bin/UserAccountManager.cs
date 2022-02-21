using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Events;
using PlayFab.MultiplayerModels;
using UnityEngine.UI;

public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager Instance;

    [SerializeField] GameObject logInButton;
    [SerializeField] GameObject logOutButton;
    [SerializeField] Text textLogOut;

    [SerializeField] GameObject grid;
    [SerializeField] GameObject cellLeader;
    [SerializeField] GameObject leaderboardButton;
    public static UnityEvent OnLoginSuccess = new UnityEvent();
    public static UnityEvent OnCreatSuccess = new UnityEvent();
    public static UnityEvent<string> OnLoginFailed = new UnityEvent<string>();

    public static UnityEvent<string> OnCreateAccountFailed = new UnityEvent<string>();

    private void Awake()
    {
        Instance = this;   
    }
   public void CreateAccount(string username, string emailAddress, string password,string displayName)
    {

        PlayFabClientAPI.RegisterPlayFabUser(
            new RegisterPlayFabUserRequest()
            {
                Email = emailAddress,
                Password=password,
                Username=username,
                DisplayName=displayName,
                RequireBothUsernameAndEmail=true
            },
            response =>
            {
                Debug.Log($"Successful Account Creation: {username}, {emailAddress}");
                SignIn(username, password);
            },
            error =>
            {
                Debug.Log($"Unsuccessful Account Creation: {username}, {emailAddress}\n {error.ErrorMessage}");
                OnCreateAccountFailed.Invoke(error.ErrorMessage);
            }

            );
        
    }
    public void SignIn(string username, string password)
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
        {
            Username = username,
            Password = password,
        },
          response =>
          {
              Debug.Log($"Successful Account Login: {username}");
              OnLoginSuccess.Invoke();
              logInButton.SetActive(false);
              logOutButton.SetActive(true);
              leaderboardButton.SetActive(true);
              textLogOut.text = textLogOut.text + username;
          },
            error =>
            {
                Debug.Log($"Unsuccessful Account Login: {username}");
                OnLoginFailed.Invoke(error.ErrorMessage);
            }

            );
    }
  

   public void sendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest { Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = "Highscores", Value = score } } };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Success!");
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error updating the leaderBoard");
    }
    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest { StatisticName = "Highscores", StartPosition = 0, MaxResultsCount = 10 };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);
    }
    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        for(int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
        foreach (var item in result.Leaderboard)
        {
            GameObject temp = Instantiate(cellLeader, grid.transform);
            GameObject temp2 = Instantiate(cellLeader, grid.transform);
            GameObject temp3 = Instantiate(cellLeader, grid.transform);
            temp.GetComponent<Text>().text = item.Position.ToString();
            temp2.GetComponent<Text>().text = item.DisplayName;
            temp3.GetComponent<Text>().text = item.StatValue.ToString();

        }
    }
    public void logOut()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        logInButton.SetActive(true);
        logOutButton.SetActive(false);
        leaderboardButton.SetActive(false);
    }
}
