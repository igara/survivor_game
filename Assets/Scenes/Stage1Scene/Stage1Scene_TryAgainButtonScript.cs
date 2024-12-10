using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Stage1Scene_TryAgainButtonScript : MonoBehaviour
{
    public void OnMouseDownTryAgain()
    {
        Stage1Scene_SceneParameter.isTryAgain = true;
        SceneManager.LoadScene("Scenes/Stage1Scene/Scene");
    }
}
