using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoto : MonoBehaviour
{

   
  public CharacterController m_Controller;
    public Transform Camera;
    public Animator anim;
    public int m_Speed;
    public int _runSpeed;
    public int _walkSpeed;
    public float m_Horizontal;
    public float m_vertical;
    Vector3 dir;
    Vector3 m_MoveDir;
    float m_lookAngle;
    public float m_smoothingTime;
    float m_smoothingAngle;
    float m_smoothingVelocity;

    /////////////////////////
    ////////////////////////
    public float m_gravity = -9.81f;
   public Vector3 velocity;
    [SerializeField]
    bool isGrounded;
    /////////////////////////
    ////////////////////////
    public Transform GroundCheck;
    public LayerMask GroundMask;
    public float GroundDistance =0.4f;
    //////////////////////////////////
    //////////////////////////////////
    public float jumpHeight = 3f;
    public bool running;

    /////////////////////////////
    
    // Start is called before the first frame update
   

    void Update()
         {
             
             //getting player input from keypress, Dpad or joystick
          m_vertical= Input.GetAxisRaw("Vertical");
          m_Horizontal= Input.GetAxisRaw("Horizontal");
          //running = Input.GetKeyDown(KeyCode.e);
          if(Input.GetKeyDown("e")){
              running = true;
          }
          if(Input.GetKeyUp("e")){
              running = !true;
          }
          //else{running = false;}
          ///////////////////////////////////////////
          gravity();
          movements();
          jump();
          groundCheck();
          applyMoveAnim();
          }

         

        void movements(){
             //covert movement input from player to 3 demntional cordinates

          dir = new Vector3(m_Horizontal,0f,m_vertical).normalized;
          if(dir.magnitude >= 0.1 ){
              m_lookAngle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
              m_smoothingAngle =Mathf.SmoothDampAngle(transform.eulerAngles.y,m_lookAngle,ref m_smoothingVelocity, m_smoothingTime);
              transform.rotation = Quaternion.Euler(0f,m_smoothingAngle,0f);

              m_MoveDir = Quaternion.Euler(0f,m_lookAngle,0f) * Vector3.forward;
              m_Controller.Move(m_MoveDir.normalized*m_Speed*Time.deltaTime);
              

          }
        }
        //applies a downward force to the player
          void gravity(){
            velocity.y += m_gravity* Time.deltaTime;
            m_Controller.Move(velocity* Time.deltaTime);

        }
         public void jump(){
            if(Input.GetButtonDown("Jump") && isGrounded){
                
            //velocity.y =Mathf.Sqrt(jumpHeight * -2f * m_gravity);
            anim.SetTrigger("Jump");
            Invoke("resetTrigger",0.5f);
            performJump();
            }
         }

            public void performJump(){
                 if( isGrounded){
               velocity.y =Mathf.Sqrt(jumpHeight * -2f * m_gravity);
            //anim.SetTrigger("Jump");
            Invoke("resetTrigger",0.5f); 
            }
            }
    
        void resetTrigger(){
            anim.ResetTrigger("Jump");
        }
        

        void groundCheck(){
            isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance,GroundMask);

            if(isGrounded && velocity.y < 0){
                velocity.y = -1;
            }
        }

        void applyMoveAnim(){
            if(dir.magnitude >= 0.1 && !Input.GetKeyDown(KeyCode.LeftShift)){
                anim.SetBool("IsWalking", true);
                m_Speed = _walkSpeed;

            }
             if(dir.magnitude >= 0.1 && running){
               anim.SetBool("IsRunning", true);
               m_Speed = _runSpeed;}

               else if(dir.magnitude >= 0.1 && !running){
                anim.SetBool("IsRunning", false);
               m_Speed = _walkSpeed;
               }
        
         if(dir.magnitude <=0.09){
            anim.SetBool("IsWalking", !true);
            anim.SetBool("IsRunning", !true);

        }

        }

    // Update is called once per frame
        // void Update()
        // {
        //     isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance,GroundMask);

        //     if(isGrounded && velocity.y < 0){
        //         velocity.y = -1;

            
        //     }
        //     anim.transform.rotation = GroundCheck.transform.rotation;


        // m_Horizontal = Input.GetAxisRaw("Horizontal");
        // m_vertical = Input.GetAxisRaw("Vertical");

        // if(Input.GetAxisRaw("Horizontal")!= 0f || Input.GetAxisRaw("Vertical")!= 0f){
        //     anim.SetBool("isWalking", true);
        // }
        // else 
        // {
        //     anim.SetBool("isWalking", false);
        //     anim.SetBool("IsIdle", true);
        // }
        // dir = new Vector3(m_Horizontal,0f,m_vertical).normalized;
        // GroundMovements();
        // gravity();
        // if(Input.GetKeyDown(KeyCode.LeftShift)){
        //     m_Speed = _runSpeed;
        //     // anim.SetBool("Isrunning", true);
        //     // anim.SetBool("IsIdle", !true);
        //     // anim.SetBool("isRunning", !true);
        // }
        // if(Input.GetKeyUp(KeyCode.LeftShift)){
        //     m_Speed = _walkSpeed;
        //     // anim.SetBool("Isrunning", !true);
        // }

        // jump();
        // }


        // void GroundMovements(){
        //     if (dir.magnitude >=0.1f){
        //     m_lookAngle =Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
        //     m_smoothingAngle =Mathf.SmoothDampAngle(transform.eulerAngles.y,m_lookAngle,ref m_smoothingVelocity, m_smoothingTime);
        //     transform.rotation = Quaternion.Euler(0f,m_smoothingAngle,0f);

        //     m_MoveDir = Quaternion.Euler(0f,m_lookAngle,0f) * Vector3.forward;
        //     m_Controller.Move(m_MoveDir.normalized*m_Speed*Time.deltaTime);
        // }

        // }
       

       


        // void animationState(){

        // }

}
