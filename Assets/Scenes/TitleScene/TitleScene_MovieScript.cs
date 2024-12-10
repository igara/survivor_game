using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TitleScene_MovieScript : MonoBehaviour
{
  [SerializeField]
  private VideoPlayer videoPlayer;

  [SerializeField]
  private GameObject canvas;

  [SerializeField]
  private AudioSource bgm;

  void Awake()
  {
    canvas.SetActive(false);
    videoPlayer.url =
        System
            .IO
            .Path
            .Combine(Application.streamingAssetsPath, "title.mp4");
    videoPlayer.Play();

    StartCoroutine(DelayAwakeCoroutine());
  }

  private IEnumerator DelayAwakeCoroutine()
  {
    // 3秒間待つ
    yield return new WaitForSeconds(3);

    canvas.SetActive(true);
    bgm.Play();
  }

  void Update()
  {
  }
}
