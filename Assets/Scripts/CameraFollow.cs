using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _camTransform;
    [SerializeField] Transform _carTransform;

    void LateUpdate()
    {
        _camTransform.position = new Vector3(_carTransform.position.x,_carTransform.position.y, -10);
    }
}
