using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusBar : MonoBehaviour
{
    public Transform StatusBarTransform;
    public TMP_Text TextHealth;

    private Transform _targetChatacter;

    private float OffsetLeft => GameController.Controller.ControllerUI.OffsetLeft.position.x;
    private float OffsetRight => GameController.Controller.ControllerUI.OffsetRight.position.x;
    private float OffsetTop => GameController.Controller.ControllerUI.OffsetTop.position.y;
    private float OffsetBottom => GameController.Controller.ControllerUI.OffsetBottom.position.y;

    public void Init(Transform targetChatacter, float health)
    {
        _targetChatacter = targetChatacter;
        SetTextHealth(health);
    }

    public void SetTextHealth(float health)
    {
        TextHealth.text = health.ToString();
    }
    
    void Update()
    {
        var screenPos = GameController.Controller.ControllerUI.MainCamera.WorldToScreenPoint(_targetChatacter.position);
        StatusBarTransform.position = new Vector2(Mathf.Clamp(screenPos.x, OffsetLeft, OffsetRight), Mathf.Clamp(screenPos.y, OffsetBottom, OffsetTop));

        Debug.Log($"char - {_targetChatacter.name} - {_targetChatacter.position} - {screenPos} - {StatusBarTransform.position}");
    }

    public void DestroyStatusBar()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
