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

  [SerializeField]
  private AudioSource dartlikeDownAudio;

  public void OnMouseDownCigarette()
  {
    dartlikeDownAudio.Play();

    var script = mainCanvas.GetComponent<Stage1Scene_MainCanvasScript>();
    script.cigaretteLevel++;
    script.selectCanvas.SetActive(false);
    script.isRunning = true;
  }
}
