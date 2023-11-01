using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public GameObject[] wheelMeshes = new GameObject[4];
    public float maxSteerAngle = 30f;
    public float motorForce = 350f;
    public float handBrakeForce = 1000f; // Сила гальмування ручником
    public float smoothSteerTime = 0.1f;
    public float smoothSpeedTime = 0.5f;

    private float steer;
    private float throttle;
    private float handBrake; // Гальмування ручником

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
        }
        else
        {
            handBrake = 0f;
        }

        for (int i = 0; i < 4; i++)
        {
            // Apply steering to the front wheels
            if (i < 2)
                wheelColliders[i].steerAngle = steer;

            // Apply power to the rear wheels
            if (i > 1)
            {
                wheelColliders[i].motorTorque = throttle;

                // Apply handbrake to the rear wheels
                wheelColliders[i].brakeTorque = handBrake;
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
