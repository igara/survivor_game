using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Stage1Scene_TitleButtonScript : MonoBehaviour
{
    public void OnMouseDownTitle()
    {
        SceneManager.LoadScene("Scenes/TitleScene/Scene");
    }
}
