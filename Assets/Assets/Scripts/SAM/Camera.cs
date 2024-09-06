using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;  // Цель, за которой будет следовать камера (персонаж)
    public float distance = 5.0f;  // Расстояние от камеры до персонажа
    public float height = 2.0f;    // Высота камеры над персонажем
    public float rotationSpeed = 0.2f;  // Скорость поворота камеры

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float yMinLimit = -40f;
    private float yMaxLimit = 80f;
    private Vector2 lastTouchPosition;

    void LateUpdate()
    {
        if (target == null)
            return;

        // Проверяем, есть ли касания
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Поворачиваем камеру при движении пальца
                currentX += touch.deltaPosition.x * rotationSpeed;
                currentY -= touch.deltaPosition.y * rotationSpeed;

                // Ограничиваем вертикальное вращение
                currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);
            }
        }

        // Определяем поворот камеры и её позицию
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, height, -distance);
        Vector3 position = target.position + rotation * direction;

        // Устанавливаем позицию и направляем камеру на цель
        transform.position = position;
        transform.LookAt(target.position + Vector3.up * height);
    }
}
