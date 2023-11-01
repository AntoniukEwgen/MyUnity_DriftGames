using UnityEngine;

public class FollowCarCamera : MonoBehaviour
{
    public GameObject car; // Об'єкт автомобіля, за яким слідкує камера
    public float distance = 10.0f; // Відстань від камери до автомобіля
    public float height = 5.0f; // Висота камери

    void Update()
    {
        // Встановлюємо позицію камери на вказану відстань за автомобілем
        Vector3 carPosition = car.transform.position;
        Vector3 offset = new Vector3(0, height, -distance);
        offset = Quaternion.Euler(0, car.transform.eulerAngles.y, 0) * offset;
        transform.position = carPosition + offset;

        // Направляємо камеру на автомобіль
        transform.LookAt(car.transform);
    }
}
