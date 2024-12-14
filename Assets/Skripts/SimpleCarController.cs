using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public float speed = 5f; // Скорость движения
    public float rotationSpeed = 100f; // Скорость поворота
    public float knockbackForce = 10f; // Сила отбрасывания
    public float flipSpeed = 360f; // Скорость переворота

    private Rigidbody rb; // Для управления отбрасыванием

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Получаем Rigidbody
    }

    void Update()
    {
        // Движение машины
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, move);

        // Поворот машины
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotate, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree")) // Если столкновение с деревом
        {
            // Отбрасывание
            Vector3 knockbackDirection = -collision.contacts[0].normal; // Направление отбрасывания
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            // Переворот
            StartCoroutine(FlipCar());
        }
    }

    private System.Collections.IEnumerator FlipCar()
    {
        float rotatedAmount = 0f; // Сколько уже повернулись
        while (rotatedAmount < 720f) // Два полных переворота (2 * 360 градусов)
        {
            float rotationStep = flipSpeed * Time.deltaTime; // Скорость переворота
            transform.Rotate(rotationStep, 0, 0); // Переворот вокруг оси X
            rotatedAmount += rotationStep;
            yield return null; // Ждём следующий кадр
        }
    }
}
