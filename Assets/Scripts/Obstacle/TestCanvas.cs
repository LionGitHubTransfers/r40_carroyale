using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCanvas : MonoBehaviour
{
    public Transform PointChatacter; //����� ��� ������ ��� ������ ���� �������� 
    public Transform Panel; // ������ ������� ������ ���� ��� ���������� ��� ��������� ��� �����������, ������� UI ������ Canvas

    public Camera Cam;

    void Update()
    {
        var screenPos = Cam.WorldToScreenPoint(PointChatacter.position);
        Panel.position = new Vector2(Mathf.Clamp(screenPos.x, 100, 980), Mathf.Clamp(screenPos.y, 100, 1820)); // Mathf.Clamp �� ���� �������� ���� �� �����, ���� ������� ��� �� �� ����������
    }
}
