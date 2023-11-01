using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public GameObject[] wheelMeshes = new GameObject[4];
    public float maxSteerAngle = 30f;
    public float motorForce = 3500f; // Змінено на 3500
    public float handBrakeForce = 1000f; // Сила гальмування ручником
    public float frontBrakeForce = 1000f; // Сила гальмування передніми колесами
    public float smoothSteerTime = 0.1f;
    public float smoothSpeedTime = 5f; // Змінено на 5

    private float steer;
    private float throttle;
    private float handBrake; // Гальмування ручником
    private float frontBrake; // Гальмування передніми колесами

    void FixedUpdate()
    {
        float targetSteer = Input.GetAxis("Horizontal") * maxSteerAngle;
        float targetThrottle = Input.GetAxis("Vertical") * motorForce;

        steer = Mathf.Lerp(steer, targetSteer, Time.deltaTime / smoothSteerTime);
        throttle = Mathf.Lerp(throttle, targetThrottle, Time.deltaTime / smoothSpeedTime);

        // Гальмування ручником при натисканні клавіші пробілу
        if (Input.GetKey(KeyCode.Space))
        {
            handBrake = handBrakeForce;
            throttle = 0f; // Додано цей рядок
        }
        else
        {
            handBrake = 0f;
        }

        // Гальмування передніми колесами при натисканні клавіші alt
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            frontBrake = frontBrakeForce;
            throttle = 0f; // Додано цей рядок
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
                wheelColliders[i].brakeTorque = frontBrake; // Застосовуємо силу гальма до передніх колес
            }

            // Apply power to the rear wheels
            if (i > 1)
            {
                wheelColliders[i].motorTorque = throttle;
                wheelColliders[i].brakeTorque = handBrake; // Застосовуємо силу гальма ручником до задніх колес
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
