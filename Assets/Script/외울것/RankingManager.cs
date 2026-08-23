using System;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    public InputField NameText;
    public Text[] RankText;

    private void Update()
    {
        LoadRank();
    }

    public void SaveRank()
    {
        string newName = NameText.text;
        int newScore = GameManager.instance.endScore;

        int[] score = new int[6];
        string[] name = new string[6];

        // 기존 5위까지 불러오기
        for (int i = 0; i < 5; i++) // 0~4 : 5개
        {
            score[i] = PlayerPrefs.GetInt("Score" + i);
            name[i] = PlayerPrefs.GetString("Name" + i);
        }

        // 이번 점수 추가
        score[5] = newScore;
        name[5] = newName;

        // 점수 기준 정렬
        Array.Sort(score, name);
        Array.Reverse(score);
        Array.Reverse(name);

        // 상위 5개만 저장
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Score" + i, score[i]);
            PlayerPrefs.SetString("Name" + i, name[i]);
        }

        PlayerPrefs.Save();
    }

    public void LoadRank()
    {
        for (int i = 0; i < 5; i++)
        {
            RankText[i].text = PlayerPrefs.GetString("Name" + i) + " - " + PlayerPrefs.GetInt("Score" + i, 0) + "점";
        }
    }
}