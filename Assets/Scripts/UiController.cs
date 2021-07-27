using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public DynamicJoystick JoystickControl;
    public Camera MainCamera;
    public Transform ContainerCharacterStatusBar;

    public GameObject TapToStart;

    [Header("Offsets")]
    public Transform OffsetLeft;
    public Transform OffsetRight;
    public Transform OffsetTop;
    public Transform OffsetBottom;

    public void Init()
    {
        
    }

    public void LoadLevel()
    {
        TapToStart.SetActive(true);
    }
    
    public void ShowTapToStart()
    {
        TapToStart.SetActive(true);
    }

    public void OnClickTapToStart()
    {
        TapToStart.SetActive(false);
        GameController.Controller.ControllerLevel.StartRace();
    }
}
