using UnityEngine;

public class FollowCarCamera : MonoBehaviour
{
    public GameObject car; // ��'��� ���������, �� ���� ����� ������
    public float distance = 10.0f; // ³������ �� ������ �� ���������
    public float height = 5.0f; // ������ ������

    void Update()
    {
        // ������������ ������� ������ �� ������� ������� �� ����������
        Vector3 carPosition = car.transform.position;
        Vector3 offset = new Vector3(0, height, -distance);
        offset = Quaternion.Euler(0, car.transform.eulerAngles.y, 0) * offset;
        transform.position = carPosition + offset;

        // ����������� ������ �� ���������
        transform.LookAt(car.transform);
    }
}
