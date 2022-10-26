using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoardUIManager : MonoBehaviour
{
    public static ScoreBoardUIManager Instance;

    public ScoreBoard scoreBoardPrefab;

    public Transform parent;

    [SerializeField] GameObject scoreBoard;

    public Dictionary<string, ScoreBoard> boardDic = new Dictionary<string, ScoreBoard>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreBoard.SetActive(!scoreBoard.activeSelf);
        }
    }

    public void AddScoreBoard(string name)
    {
        var newScoreBoard = Instantiate(scoreBoardPrefab);
        newScoreBoard.transform.SetParent(parent);
        newScoreBoard.name = name;
        newScoreBoard.playerName = name;
        newScoreBoard.playerNameTMP.text = name;
        newScoreBoard.scoreTMP.text = 0.ToString();
        boardDic.Add(name, newScoreBoard);
    }

    public void UpdateScoreBoard(string name)
    {
        ScoreBoard scoreBoard;
        boardDic.TryGetValue(name, out scoreBoard);
        scoreBoard.score += 100;
        scoreBoard.scoreTMP.text = scoreBoard.score.ToString();
    }
}
