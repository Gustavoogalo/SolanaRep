using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private Animator _animator;

    private Vector3 _input;

    public float jumpForce = 8;
    public float gravity = -10;
    public float groundDistance = 0.5f;

    bool isGrounded;

    //Vector3 _velocityPlayer;


    

    private void Update()
    {

        if (isGrounded && _input.y < 0)
        {
            //para que o jogador nao fique flutuando.
            _input.y = -0.2f;
        }
        TakeInput();
        Look();
        Jump();
        ;

    }

    private void FixedUpdate()
    {
        Walking();
    }







    void TakeInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }





    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _rb.velocity = Vector3.up * jumpForce;
        }

        //_input.y = _input.y + gravity * Time.deltaTime;



    }



    void Look()
    {
        if (_input != Vector3.zero)
        {
            //chama o _input.ToIso() do Script Helpers - um Script de classe Static;
            var relative = (transform.position + _input.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            //transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
            transform.rotation = rot;
        }
    }

    void Walking()
    {
        if (_input != Vector3.zero)
        {
            _animator.SetBool("IsMoving", true);
            _rb.MovePosition(transform.position + (transform.forward * _input.magnitude) * _speed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("IsMoving", false);
        }

    }
}
