using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Stage1Scene_TryAgainButtonScript : MonoBehaviour
{
  [SerializeField]
  private AudioSource dartlikeUpAudio;

  public void OnMouseDownTryAgain()
  {
    dartlikeUpAudio.Play();

    StartCoroutine(DelayCoroutine());
  }

  private IEnumerator DelayCoroutine()
  {
    yield return new WaitForSeconds(1);

    SceneManager.LoadScene("Scenes/Stage1Scene/Scene");
  }
}
