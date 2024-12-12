using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Stage1Scene_JoystickControllerScript : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
  [SerializeField]
  private RectTransform background; // スティックの背景
  [SerializeField]
  private RectTransform handle;     // スティックの本体
  [SerializeField]
  private Vector2 inputVector;      // 入力ベクトル

  public void OnDrag(PointerEventData eventData)
  {
    Vector2 position;
    // タッチ位置を背景内の座標に変換
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        background,
        eventData.position,
        eventData.pressEventCamera,
        out position
    );

    // スティックの動きを制限
    position.x = (position.x / background.sizeDelta.x) * 2;
    position.y = (position.y / background.sizeDelta.y) * 2;
    inputVector = new Vector2(position.x, position.y);
    inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

    // ハンドルを動かす
    handle.anchoredPosition = new Vector2(
        inputVector.x * (background.sizeDelta.x / 2),
        inputVector.y * (background.sizeDelta.y / 2)
    );
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    OnDrag(eventData);
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    inputVector = Vector2.zero;
    handle.anchoredPosition = Vector2.zero;
  }

  // 入力値を取得する関数
  public float Horizontal()
  {
    return inputVector.x;
  }

  public float Vertical()
  {
    return inputVector.y;
  }
}
