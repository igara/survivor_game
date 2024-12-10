using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleScene_Stage1ButtonScript : MonoBehaviour
{
  [SerializeField] private GameObject memoCanvas;

  public void OnMouseDownStage1()
  {
    if (memoCanvas.activeSelf) return;

    Stage1Scene_SceneParameter.isTryAgain = true;
    SceneManager.LoadScene("Scenes/Stage1Scene/Scene");
  }
}
