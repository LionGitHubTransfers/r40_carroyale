using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public DynamicJoystick JoystickControl;
    public Camera MainCamera;
    public Transform ContainerCharacterStatusBar;

    public GameObject TapToStart;
    public GameObject ContainerLoos;
    public GameObject ContainerResult;

    [Header("Offsets")]
    public Transform OffsetLeft;
    public Transform OffsetRight;
    public Transform OffsetTop;
    public Transform OffsetBottom;

    public TMP_Text TextLider1;
    public TMP_Text TextLider2;
    public TMP_Text TextLider3;

    public void Init()
    {
        HidePanels();
    }

    //public void LoadLevel()
    //{
    //    TapToStart.SetActive(true);
    //}
    
    public void ShowTapToStart()
    {
        TapToStart.SetActive(true);
    }

    public void ShowContainerLoos()
    {
        ContainerLoos.SetActive(true);
    }

    public void ShowContainerResult()
    {
        ContainerResult.SetActive(true);
    }

    public void OnClickTapToStart()
    {
        TapToStart.SetActive(false);
        GameController.Controller.ControllerLevel.StartRace();
    }

    public void OnClockRestart()
    {
        HidePanels();
        GameController.Controller.Restart();
    }

    public void OnClockContinue()
    {
        HidePanels();
        GameController.Controller.Continue();
    }

    private void HidePanels()
    {
        ContainerLoos.SetActive(false);
        ContainerResult.SetActive(false);
    }

    public void LevelCompleted(List<string> liders)
    {
        if (liders.Count >= 2)
            TextLider3.text = liders[liders.Count -2];
        else
            TextLider3.text = "-";

        if (liders.Count >= 1)
            TextLider2.text = liders[liders.Count - 1];
        else
            TextLider2.text = "-";

            TextLider1.text = "Player";

        StartCoroutine(Finish());
    }
    private IEnumerator Finish()
    {
        yield return new WaitForSeconds(1);

        ShowContainerResult();
    }
}
