using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botoes : MonoBehaviour
{
    Rigidbody rb2D;
    Animator playerAnim;

    public float Velocity, Speed;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody>();
        playerAnim = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        Move(Speed);
    }

    void Move(float _speed)
    {
        rb2D.transform.Translate(_speed * Time.deltaTime, 0, 0);

     
    }
   
    public void MoveRight()
    {
        Speed = Velocity;
    }
    public void MoveLeft()
    {
        Speed = -Velocity;
    }
    
    public void Jump()
    {
        rb2D.AddForce(Vector2.up * 4, ForceMode.Impulse);
    }
}
