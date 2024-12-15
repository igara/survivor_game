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

  public void OnMouseDownButaman()
  {
    var script = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
    script.GetButaman();
  }
}
