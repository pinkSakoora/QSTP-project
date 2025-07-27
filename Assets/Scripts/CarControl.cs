using UnityEngine;

public class CarControl : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _rotationspeed;
    [SerializeField] Transform _transform;
    void Update()
    {
        float acceleration = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        if (Mathf.Abs(acceleration) > 0.01f)
        {
            _transform.Translate(0, _speed * acceleration * Time.deltaTime, 0);
            if (Mathf.Abs(turn) > 0.01f)
            {
                _transform.Rotate(0, 0, _rotationspeed * turn * Time.deltaTime);
            }
        }
    }
}
