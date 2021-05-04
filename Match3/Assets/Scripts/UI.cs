using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Insctance;


    public Text TextScore;
    //public Text MenuTextScore;
    private int Score = 0;
    public GameObject Shest1;
    public GameObject Shest2;
    public GameObject Shest3;
    public GameObject Shest4;

    private void Awake()
    {
        Insctance = this;
    }
    private void Update()
    {
        if (Score > 100)
        {
            Shest1.SetActive(true);
        }
        if (Score > 200)
        {
            Shest2.SetActive(true);
        }
        if (Score > 300)
        {
            Shest3.SetActive(true);
        }
        if (Score > 400)
        {
            Shest4.SetActive(true);
        }
    }
    public void AddScore(int value)
    {
        Score += value;
        TextScore.text = "Score: " + Score.ToString();
        //MenuTextScore.text = "Score: " + Score.ToString();

    }
}
