using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    [SerializeField] private Rigidbody _rb;
    //[SerializeField] private Animator _anim;

    private Vector3 _inputs;
    public float rotateVelocity = 180;



    Vector3 velocityPlayer;

    bool isGrounded;

    public float jumpForce = 8;
    public float gravity = -10;

    private void Update()
    {
        isGrounded = cc.isGrounded;
        if (isGrounded && velocityPlayer.y < 0)
        {
            //para que o jogador nao fique flutuando.
            //velocityPlayer.y = -0.2f;
        }
        TakeInputs();
        Jump();

        //if (_inputs.X != 0f && _speed < _velocity)
        //{
        //    _speed += Time.deltaTime * _acceleration;
        //}
        Walking();
    }

    void FixedUpdate()
    {
        
    }

    #region Inputs
    void TakeInputs()
    {
        _inputs = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        //persongem olha para onde esta se movendo
        //cc.Move(transform.forward * _inputs.Z * _velocity * Time.deltaTime);
    }
    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocityPlayer.y = jumpForce;
        }
        velocityPlayer.y = velocityPlayer.y + gravity * Time.deltaTime;

        cc.Move(velocityPlayer * Time.deltaTime);

    }
    #endregion

    #region Detections
    [Header("Detection")]
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _coliserOffset = -0.85f, _grounderRadius = 0.2f;
    [SerializeField] private float _wallCheckOffset = 0.5f, _wallCheckRadius = 0.38f;


    private readonly Collider[] _ground = new Collider[1];



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // TakeisGrounded
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, _coliserOffset), _grounderRadius); //esfera de colisao para o chao;

        // Wall
        //Gizmos.DrawWireSphere(WallDetectPosition, _wallCheckRadius);
    }
    #endregion

    #region Walking

    [Header("Walking")]
    //[SerializeField] private float _speed = 0f;
    [SerializeField] private float _velocity = 10;
    [SerializeField] private float _acceleration = 2;
    [SerializeField] private float _currentMovementLerpSpeed = 10;

    [SerializeField] private float _maxPenaltyVelocity = 0.5f;
    private float _currentPenaltyVelocity;

    private Vector3 _dir;
    void Walking()
    {
        _currentMovementLerpSpeed = Mathf.MoveTowards(_currentMovementLerpSpeed, _velocity, _acceleration * Time.deltaTime);

        var normalizedDir = _dir.normalized;

        //atribui a velocidade lentamente.
        if (_dir != Vector3.zero)
        {
            _currentPenaltyVelocity += _acceleration * Time.deltaTime;
        }
        else
        {
            _currentPenaltyVelocity -= _acceleration * Time.deltaTime;
        }
        _currentPenaltyVelocity = Mathf.Clamp(_currentPenaltyVelocity, _maxPenaltyVelocity, 1);

        // Set current y vel and add walking penalty
        var targetVel = new Vector3(normalizedDir.x, _rb.velocity.y, normalizedDir.z) * _currentPenaltyVelocity * _velocity;

        // Set vel
        var idealVel = new Vector3(targetVel.x, _rb.velocity.y, targetVel.z);
        _rb.velocity = Vector3.MoveTowards(_rb.velocity, idealVel, _currentMovementLerpSpeed * Time.deltaTime);
    }

    #endregion


   
}
