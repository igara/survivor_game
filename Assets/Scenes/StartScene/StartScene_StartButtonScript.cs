using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartScene_StartButtonScript : MonoBehaviour
{
    public void OnMouseDownStart()
    {
        SceneManager.LoadScene("Scenes/TitleScene/Scene");
    }
}
