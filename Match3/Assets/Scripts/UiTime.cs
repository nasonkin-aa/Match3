using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTime : MonoBehaviour
{
   
    public Text TextTime;
    public float Timer = 60f;
    public Slider IsSlider;
    public AudioSource Boom;
    public GameObject PanelGameOver;
    private void Start()
    {
        PanelGameOver.SetActive(false);
    }
    void Update()
    {
        if (Timer > 0)
        {
            Boom.Play();
            Timer -= Time.deltaTime;
            TextTime.text = Mathf.Round(Timer).ToString();
            IsSlider.value = Timer;
        }
        else
        {
            PanelGameOver.SetActive(true);
        }
    }
}
