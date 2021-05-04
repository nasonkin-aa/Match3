using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonGame : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Menu");
    }
}
