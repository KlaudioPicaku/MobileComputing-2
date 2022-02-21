using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] GameObject grid;
    [SerializeField] GameObject cellSize;
    [SerializeField] GameObject leaderBoard;
    [SerializeField] PauseMenu pause;
    [SerializeField] SaveManager mainMenu;
    [SerializeField] GameObject joystick;
    int counter = 0;

    private int localScore = 0;
    private void Update()
    {
        localScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().score;
    }

    public void gotoMain()
    {
        mainMenu.mainMenu();
    }
    public void sendData()
    {
        sendLeaderboard(localScore);
    }
    public void activateLeader()
    {
        leaderBoard.SetActive(true);
        pause.Pause();
        joystick.SetActive(false);
        GetLeaderBoard();
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
    public void GetLeaderBoard()
    {
        var request = new GetLeaderboardRequest { StatisticName = "Highscores", StartPosition = 0, MaxResultsCount = 10 };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderBoardGet, OnError);
    }
    void OnLeaderBoardGet(GetLeaderboardResult result)
    {
        counter = 0;
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            Destroy(grid.transform.GetChild(i).gameObject);
        }
        Debug.Log(result.Leaderboard.Count);
        foreach (var item in result.Leaderboard)
        {
            GameObject temp = Instantiate(cellSize, grid.transform);
            GameObject temp2 = Instantiate(cellSize, grid.transform);
            GameObject temp3 = Instantiate(cellSize, grid.transform);
            temp.GetComponent<Text>().text = counter++.ToString();
            temp2.GetComponent<Text>().text = item.DisplayName;
            temp3.GetComponent<Text>().text = item.StatValue.ToString();
        }
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error updating the leaderBoard");
    }
}
