using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_SelectCanvasScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;
  private Stage1Scene_MainCanvasScript mainCanvasScript;

  [SerializeField]
  private TMP_Text cigaretteLevelText;
  [SerializeField]
  private TMP_Text dartLevelText;

  [SerializeField]
  private TMP_Text bellCountText;

  [SerializeField]
  private TMP_Text butamanCountText;


  void Awake()
  {
    mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
  }
  private void OnEnable()
  {
    cigaretteLevelText.text = mainCanvasScript.cigaretteLevel.ToString();
    dartLevelText.text = mainCanvasScript.dartLevel.ToString();
    bellCountText.text = mainCanvasScript.bellCount.ToString();
    butamanCountText.text = mainCanvasScript.butamanCount.ToString();
  }
}
