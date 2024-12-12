using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Stage1Scene_MainCanvasScript : MonoBehaviour
{
  [SerializeField]
  private Stage1Scene_JoystickControllerScript joystick; // 仮想スティック
  [SerializeField]
  private Transform cameraTransform;
  [SerializeField]
  private GameObject gameOverCanvas;
  [SerializeField]
  private Transform playerTransform;
  [SerializeField]
  private Transform shinyaTransform;
  [SerializeField]
  private Transform uiTransform;

  private float speed = 2f; // 移動速度
  private float inputThreshold = 0.1f;
  private float rotationSpeed = 5f;    // 角度変更のスムーズさ
  private float currentAngle = 0f;

  private float timeRemaining = 180f;
  [SerializeField]
  private TMP_Text timerText;
  private bool isRunning = true;

  private int hp = 100;
  [SerializeField]
  private TMP_Text hpValueText;
  [SerializeField]
  private Slider hpSlider;

  private int exp = 0;
  private TMP_Text lavelValueText;
  [SerializeField]
  private Slider expSlider;

  private int bullCount = 0;

  void Start()
  {
    hpValueText.text = hp.ToString();
    hpSlider.value = hp / 100;

    expSlider.value = exp;
  }

  void Update()
  {
    if (isRunning)
    {
      if (timeRemaining > 0)
      {
        timeRemaining -= Time.deltaTime;
        UpdateTimerDisplay();
      }
      else
      {
        timeRemaining = 0;
        isRunning = false;
        UpdateTimerDisplay();
        OnTimerEnd(); // タイマーが終了したときの処理
      }
    }

    // 仮想スティックの入力値を取得
    float horizontal = joystick.Horizontal();
    float vertical = joystick.Vertical();

    // 入力ベクトルを作成（Y軸の値も含む）
    Vector3 direction = new Vector3(horizontal, vertical, 0); // Z軸方向をY軸に置き換える場合もあり

    // ベクトルの大きさが閾値以上なら移動
    if (direction.magnitude >= inputThreshold)
    {
      direction.Normalize(); // ベクトルを正規化
      playerTransform.transform.Translate(direction * speed * Time.deltaTime);

      // 角度計算
      float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      targetAngle += 90f; // 右回りに90度加算して調整
      // 角度をスムーズに変更
      currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
      shinyaTransform.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    // カメラをプレイヤーの位置に追従させる
    if (cameraTransform != null)
    {
      cameraTransform.position = new Vector3(playerTransform.transform.position.x, playerTransform.transform.position.y, cameraTransform.position.z);

      uiTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, uiTransform.position.z);
      uiTransform.rotation = cameraTransform.rotation;

      gameOverCanvas.transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, gameOverCanvas.transform.position.z);
      gameOverCanvas.transform.rotation = cameraTransform.rotation;
    }
  }

  void UpdateTimerDisplay()
  {
    int minutes = Mathf.FloorToInt(timeRemaining / 60);
    int seconds = Mathf.FloorToInt(timeRemaining % 60);
    timerText.text = $"{minutes:D2}:{seconds:D2}";
  }

  void OnTimerEnd()
  {
    // タイマー終了時の処理
    Debug.Log("タイマーが終了しました！");
  }
}
