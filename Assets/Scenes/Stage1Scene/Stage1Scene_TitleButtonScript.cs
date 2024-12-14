using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Stage1Scene_TitleButtonScript : MonoBehaviour
{
  [SerializeField]
  private AudioSource dartlikeDownAudio;

  public void OnMouseDownTitle()
  {
    dartlikeDownAudio.Play();

    StartCoroutine(DelayCoroutine());
  }

  private IEnumerator DelayCoroutine()
  {
    yield return new WaitForSeconds(1);

    SceneManager.LoadScene("Scenes/TitleScene/Scene");
  }
}
