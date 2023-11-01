using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform carTransform; // це трансформація автомобіля, за яким слідує камера
    public Vector3 offset; // це зміщення камери відносно автомобіля

    private void Update()
    {
        // Розраховуємо нову позицію камери
        Vector3 newPosition = carTransform.position + carTransform.TransformDirection(offset);

        // Оновлюємо позицію камери
        transform.position = newPosition;

        // Змушуємо камеру дивитися на автомобіль
        transform.LookAt(carTransform);
    }
}
