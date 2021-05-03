using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text hordeSizeText;
    public int hordeSizeNumber;

    public Text cashText;

    public CameraController cameraController;

    void Update()
    {
        hordeSizeText.text = cameraController.horde.Length.ToString();
        cashText.text = cameraController.cash.ToString();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
