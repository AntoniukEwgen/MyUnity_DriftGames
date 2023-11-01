using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform carTransform; // �� ������������� ���������, �� ���� ���� ������
    public Vector3 offset; // �� ������� ������ ������� ���������

    private void Update()
    {
        // ����������� ���� ������� ������
        Vector3 newPosition = carTransform.position + carTransform.TransformDirection(offset);

        // ��������� ������� ������
        transform.position = newPosition;

        // ������� ������ �������� �� ���������
        transform.LookAt(carTransform);
    }
}
