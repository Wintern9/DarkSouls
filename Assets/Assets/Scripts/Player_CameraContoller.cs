using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CameraContoller : MonoBehaviour
{
    public Transform target;  // Цель, за которой будет следовать камера (персонаж)
    public float distance = 5.0f;  // Расстояние от камеры до персонажа
    public float height = 2.0f;    // Высота камеры над персонажем
    public float rotationSpeed = 5.0f;  // Скорость поворота камеры вокруг персонажа
    public float collisionRadius = 0.5f;  // Радиус для обработки столкновений камеры

    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float yMinLimit = -40f;
    private float yMaxLimit = 80f;

    private void Start()
    {
        // Инициализируем стартовую позицию
        Vector3 angles = transform.eulerAngles;
        currentX = angles.y;
        currentY = angles.x;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Получаем оси мыши для управления камерой
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Ограничиваем вертикальное вращение
        currentY = Mathf.Clamp(currentY, yMinLimit, yMaxLimit);

        // Определяем направление камеры и позицию с учетом высоты
        rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 direction = new Vector3(0, height, -distance);
        Vector3 desiredPosition = target.position + rotation * direction;

        // Обрабатываем столкновения камеры с объектами
        RaycastHit hit;
        if (Physics.SphereCast(target.position, collisionRadius, desiredPosition - target.position, out hit, distance))
        {
            // Если камера столкнулась с объектом, мы перемещаем её ближе к персонажу
            desiredPosition = hit.point;
        }

        // Плавное движение камеры к нужной позиции
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        // Направляем камеру на персонажа
        transform.LookAt(target.position + Vector3.up * height);
    }

    Quaternion rotation;

    public Quaternion GetQuaternionCamera()
    {
        return rotation;
    }
}
