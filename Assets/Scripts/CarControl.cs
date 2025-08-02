using System.Collections;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    [SerializeField] float _accelPower;
    [SerializeField] float _rotationSpeed;
    float _xInput;
    float _yInput;
    [SerializeField] float _speed;
    [SerializeField] float _speedDecay;
    [SerializeField] float _brakePower;
    float _accelMult;
    bool _isBoosting;
    float _boostMult = 1;

    const float TOP_SPEED = 2;
    const float GEAR1_TOP = TOP_SPEED * 0.1f;
    const float GEAR2_TOP = TOP_SPEED * 0.2f;
    const float GEAR3_TOP = TOP_SPEED * 0.35f;
    const float GEAR4_TOP = TOP_SPEED * 0.5f;
    const float GEAR5_TOP = TOP_SPEED * 0.7f;

    void Update()
    {
        HandleInput();
        Acceleration();
        Gear();
    }

    void FixedUpdate()
    {
        Movement();
    }

    /*
    - Make acceleration actually be the accelerator, i.e., increase speed but let speed decay be controlled by friction instead.
    - Add top speed
    - Add friction/speed decay
    - Make turning be dependent on speed too
    - Separate braking and acceleration
    */

    /*
    TASK - 0: Speed increased based on input, decreased based on friction.
    - Speed increases on getting input
        - When input > 0, increase _speed float
    - Translate car according to Speed
    */

    /*
    TASK - 1: Acceleration based on current speed
    Set acceleration values according to current "gear" - current speed bracket
    */

    void HandleInput()
    {
        if (!_isBoosting && Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isBoosting = true;
            StartCoroutine(Boost());
        }
        _xInput = Input.GetAxis("Horizontal");
        _yInput = Input.GetAxis("Vertical");
    }

    void Acceleration()
    {
        _speed = Mathf.Clamp(_speed, 0, TOP_SPEED * _boostMult);
        if (_yInput > 0.01f)
        {
            _speed += _accelPower * _accelMult * _yInput * _boostMult;
        }
        else if (_yInput == 0 && _speed > 0)
        {
            _speed -= _speedDecay;
        }
        else if (_yInput < 0)
        {
            if (_speed > 0)
            {
                _speed -= _brakePower;
            }
            if (_speed < 0)
            {
                _speed = 0;
            }
        }
    }

    IEnumerator Boost()
    {
        _boostMult = 1.3f;
        yield return new WaitForSeconds(5f);
        _boostMult = 1;
        yield return new WaitForSeconds(10f);
        _isBoosting = false;
        yield return null;
    }

    void Gear()
    {
        _accelMult = _speed
        switch
        {
            float n when n >= 0 && n <= GEAR1_TOP => 1,
            float n when n > GEAR1_TOP && n <= GEAR2_TOP => 0.8f,
            float n when n > GEAR2_TOP && n <= GEAR3_TOP => 0.65f,
            float n when n > GEAR3_TOP && n <= GEAR4_TOP => 0.5f,
            float n when n > GEAR4_TOP && n <= GEAR5_TOP => 0.4f,
            float n when n > GEAR5_TOP => 0.3f,
            _ => 0.1f
        };
    }

    void Movement()
    {
        transform.Translate(0, _speed, 0);
        if (Mathf.Abs(_xInput) > 0.01f && _speed > 0)
        {
            transform.Rotate(0, 0, -1 * _rotationSpeed * _xInput);
        }
    }
}
