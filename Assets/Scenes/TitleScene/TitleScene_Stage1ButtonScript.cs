using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleScene_Stage1ButtonScript : MonoBehaviour
{
  [SerializeField]
  private GameObject memoCanvas;

  [SerializeField]
  private AudioSource dartlikeUpAudio;

  public void OnMouseDownStage1()
  {
    if (memoCanvas)
    {
      if (memoCanvas.activeSelf) return;
    }

    dartlikeUpAudio.Play();

    StartCoroutine(DelayCoroutine());
  }

  private IEnumerator DelayCoroutine()
  {
    yield return new WaitForSeconds(1);

    SceneManager.LoadScene("Scenes/Stage1Scene/Scene");
  }
}
