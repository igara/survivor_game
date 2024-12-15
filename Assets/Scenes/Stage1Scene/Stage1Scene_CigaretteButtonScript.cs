using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Stage1Scene_CigaretteButtonScript : MonoBehaviour
{
  [SerializeField]
  private GameObject mainCanvas;


  public void OnMouseDownCigarette()
  {
    var script = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
    script.GetCigarette();
  }
}
