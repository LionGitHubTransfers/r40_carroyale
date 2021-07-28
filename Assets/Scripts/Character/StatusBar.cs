using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusBar : MonoBehaviour
{
    public Transform StatusBarTransform;
    public TMP_Text TextName;
    public TMP_Text TextHealth;

    private float OffsetLeft => GameController.Controller.ControllerUI.OffsetLeft.position.x;
    private float OffsetRight => GameController.Controller.ControllerUI.OffsetRight.position.x;
    private float OffsetTop => GameController.Controller.ControllerUI.OffsetTop.position.y;
    private float OffsetBottom => GameController.Controller.ControllerUI.OffsetBottom.position.y;

    private Transform _targetChatacter;

    public void Init(Transform targetChatacter, string name, float health)
    {
        _targetChatacter = targetChatacter;
        TextName.text = name;
        SetTextHealth(health);
    }

    public void SetTextHealth(float health)
    {
        TextHealth.text = health.ToString("F");
    }
    
    void Update()
    {
        var screenPos = GameController.Controller.ControllerUI.MainCamera.WorldToScreenPoint(_targetChatacter.position);

        if (screenPos.z < 0)
            screenPos *= -1;
        else if(screenPos.z < 0)
            if(screenPos.y > 0)
                screenPos *= -1;

        StatusBarTransform.position = new Vector2(Mathf.Clamp(screenPos.x, OffsetLeft, OffsetRight), Mathf.Clamp(screenPos.y, OffsetBottom, OffsetTop));
    }

    public string GetName()
    {
        return TextName.text;
    }

    public void DestroyStatusBar()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
