using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Stage1Scene_ButamanButtonScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;

  private Stage1Scene_MainCanvasScript mainCanvasScript;

  void Awake()
  {
    mainCanvasScript = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
  }

  public void OnMouseDownButaman()
  {
    mainCanvasScript.GetButaman();
  }
}
