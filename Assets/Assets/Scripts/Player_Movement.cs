using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player_Movement : MonoBehaviour
{
    public float Speed = 5f;
    public float JumpForce = 300f;
    private bool _isGrounded;
    private Rigidbody _rb;

    // —сылка на камеру
    public Transform cameraTransform;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        JumpLogic();
        MovementLogic();
    }

    // Ћогика вращени€ игрока в сторону камеры
    private void PlayerRotationAtCamera(Vector3 movementDirection)
    {
        if (movementDirection.magnitude > 0.1f)  // ѕоворачиваем только при движении
        {
            // Ѕерем направление камеры по плоскости XZ (без учета высоты)
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;  // ”бираем вли€ние высоты
            cameraForward.Normalize();  // Ќормализуем вектор

            // ѕоворачиваем направление движени€ игрока относительно направлени€ камеры
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);  // ѕлавный поворот
        }
    }

    private void MovementLogic()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ѕолучаем направление движени€ игрока в мировых координатах относительно камеры
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = cameraTransform.TransformDirection(movement);
        movement.y = 0.0f;  // ”бираем вли€ние вертикальной оси

        // ѕеремещение игрока
        transform.Translate(movement * Speed * Time.fixedDeltaTime, Space.World);

        // ѕоворачиваем игрока в сторону камеры при движении
        PlayerRotationAtCamera(movement);
    }

    private void JumpLogic()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                _rb.AddForce(Vector3.up * JumpForce);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpate(collision, true);
    }

    void OnCollisionExit(Collision collision)
    {
        IsGroundedUpate(collision, false);
    }

    private void IsGroundedUpate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == ("Ground"))
        {
            _isGrounded = value;
        }
    }
}
