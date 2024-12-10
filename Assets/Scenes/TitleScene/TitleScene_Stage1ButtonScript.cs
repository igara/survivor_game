using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleScene_Stage1ButtonScript : MonoBehaviour
{
    public void OnMouseDownStage1()
    {
        Stage1Scene_SceneParameter.isTryAgain = true;
        SceneManager.LoadScene("Scenes/Stage1Scene/Scene");
    }
}
