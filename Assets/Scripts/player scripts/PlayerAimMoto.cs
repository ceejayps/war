using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimMoto : MonoBehaviour
{
    // Start is called before the first frame update
        public CharacterController m_Controller;
    public Transform Camera;
    public Animator anim;
    public int m_Speed;
    public int _runSpeed;
    public int _walkSpeed;
    public float m_Horizontal;
    public float m_vertical;
    public Vector3 dir;
    public Vector3 m_MoveDir;
   
    // Start is called before the first frame update
    void Start()
    {
        m_Controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
         m_vertical= Input.GetAxisRaw("Vertical");
          m_Horizontal= Input.GetAxisRaw("Horizontal");
           dir = new Vector3(m_Horizontal,0f,m_vertical).normalized;


           if(dir.magnitude >= 0.1 ){
              
              m_Controller.Move(dir*m_Speed*Time.deltaTime);
              

          }
        

    }
}
