using UnityEngine;

public class SimpleCarController : MonoBehaviour
{
    public float speed = 5f; // �������� ��������
    public float rotationSpeed = 100f; // �������� ��������
    public float knockbackForce = 10f; // ���� ������������
    public float flipSpeed = 360f; // �������� ����������

    private Rigidbody rb; // ��� ���������� �������������

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // �������� Rigidbody
    }

    void Update()
    {
        // �������� ������
        float move = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, move);

        // ������� ������
        float rotate = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotate, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree")) // ���� ������������ � �������
        {
            // ������������
            Vector3 knockbackDirection = -collision.contacts[0].normal; // ����������� ������������
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            // ���������
            StartCoroutine(FlipCar());
        }
    }

    private System.Collections.IEnumerator FlipCar()
    {
        float rotatedAmount = 0f; // ������� ��� �����������
        while (rotatedAmount < 720f) // ��� ������ ���������� (2 * 360 ��������)
        {
            float rotationStep = flipSpeed * Time.deltaTime; // �������� ����������
            transform.Rotate(rotationStep, 0, 0); // ��������� ������ ��� X
            rotatedAmount += rotationStep;
            yield return null; // ��� ��������� ����
        }
    }
}
