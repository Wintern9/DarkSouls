using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;  // ����, �� ������� ����� ��������� ������ (��������)
    public float distance = 5.0f;  // ���������� �� ������ �� ���������
    public float height = 2.0f;    // ������ ������ ��� ����������
    public float rotationSpeed = 0.2f;  // �������� �������� ������

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float yMinLimit = -40f;
    private float yMaxLimit = 80f;
    private Vector2 lastTouchPosition;

    void LateUpdate()
    {
        if (target == null)
            return;

        // ���������, ���� �� �������
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // ������������ ������ ��� �������� ������
                currentX += touch.deltaPosition.x * rotationSpeed;
                currentY -= touch.deltaPosition.y * rotationSpeed;

                // ������������ ������������ ��������
                currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);
            }
        }

        // ���������� ������� ������ � � �������
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, height, -distance);
        Vector3 position = target.position + rotation * direction;

        // ������������� ������� � ���������� ������ �� ����
        transform.position = position;
        transform.LookAt(target.position + Vector3.up * height);
    }
}
