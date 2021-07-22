using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCanvas : MonoBehaviour
{
    public Transform PointChatacter; //точка над юнитом где должна быть панелька 
    public Transform Panel; // панель которая должна быть над персонажем или указывать его направление, обычное UI внутри Canvas

    public Camera Cam;

    void Update()
    {
        var screenPos = Cam.WorldToScreenPoint(PointChatacter.position);
        Panel.position = new Vector2(Mathf.Clamp(screenPos.x, 100, 980), Mathf.Clamp(screenPos.y, 100, 1820)); // Mathf.Clamp не дает панельке уйти за экран, плюс отступы что бы не обрезалась
    }
}
