using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text hordeSizeText;
    public int hordeSizeNumber;

    public CameraController cameraController;

    void Update()
    {
        hordeSizeText.text = cameraController.horde.Length.ToString();
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
