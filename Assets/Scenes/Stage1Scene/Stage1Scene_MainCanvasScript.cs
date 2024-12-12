using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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

  private float speed = 0.5f; // 移動速度
  private float inputThreshold = 0.1f;
  private float rotationSpeed = 5f;    // 角度変更のスムーズさ
  private float currentAngle = 0f;

  void Update()
  {
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
}
