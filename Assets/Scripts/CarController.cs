using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public GameObject[] wheelMeshes = new GameObject[4];
    public float maxSteerAngle = 30f;
    public float motorForce = 3500f; // ������ �� 3500
    public float handBrakeForce = 1000f; // ���� ����������� ��������
    public float frontBrakeForce = 1000f; // ���� ����������� �������� ��������
    public float smoothSteerTime = 0.1f;
    public float smoothSpeedTime = 5f; // ������ �� 5

    private float steer;
    private float throttle;
    private float handBrake; // ����������� ��������
    private float frontBrake; // ����������� �������� ��������

    void FixedUpdate()
    {
        float targetSteer = Input.GetAxis("Horizontal") * maxSteerAngle;
        float targetThrottle = Input.GetAxis("Vertical") * motorForce;

        steer = Mathf.Lerp(steer, targetSteer, Time.deltaTime / smoothSteerTime);
        throttle = Mathf.Lerp(throttle, targetThrottle, Time.deltaTime / smoothSpeedTime);

        // ����������� �������� ��� ��������� ������ ������
        if (Input.GetKey(KeyCode.Space))
        {
            handBrake = handBrakeForce;
            throttle = 0f; // ������ ��� �����
        }
        else
        {
            handBrake = 0f;
        }

        // ����������� �������� �������� ��� ��������� ������ alt
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            frontBrake = frontBrakeForce;
            throttle = 0f; // ������ ��� �����
        }
        else
        {
            frontBrake = 0f;
        }

        for (int i = 0; i < 4; i++)
        {
            // Apply steering to the front wheels
            if (i < 2)
            {
                wheelColliders[i].steerAngle = steer;
                wheelColliders[i].brakeTorque = frontBrake; // ����������� ���� ������ �� ������� �����
            }

            // Apply power to the rear wheels
            if (i > 1)
            {
                wheelColliders[i].motorTorque = throttle;
                wheelColliders[i].brakeTorque = handBrake; // ����������� ���� ������ �������� �� ����� �����
            }

            // Update the wheel mesh positions
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);
            wheelMeshes[i].transform.position = pos;
            wheelMeshes[i].transform.rotation = quat;
        }
    }
}
