using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMoto2 : MonoBehaviour
{
    public enum m_MovementType { basic, aiming, climbing};
	public m_MovementType m_movement;
    public CharacterController m_Controller;
    public Transform Camera;
    public Animator anim;
    public float m_Speed;
    public float _runSpeed =7f;
    public float _walkSpeed = 2f;
    public float acceleration = 0.3333333f;
    public float deceleration = 0.1f;
    public float m_Horizontal;
    public float m_vertical;
    public  Vector3 dir;
    public  Vector3 m_MoveDir;
    float m_lookAngle;
    public float m_smoothingTime;
    float m_smoothingAngle;
    float m_smoothingVelocity;
    private biology m_biology;
    //////////////////////////////////
    //////////////////////////////////
    public float m_gravity = -9.81f;
    public Vector3 velocity;
    [SerializeField]
    bool isGrounded;
    //////////////////////////////////
    //////////////////////////////////
    public Transform GroundCheck;
    public LayerMask GroundMask;
    public float GroundDistance =0.4f;
    //////////////////////////////////
    //////////////////////////////////
    public float jumpHeight = 3f;
    public bool running;
    bool wasRunning;
    Vector3 charBaiscCenter;
    float charBaiscHeight;
    public bool crouched;
    public Vector3 CharCrouchedCenter;
    public float charChrouchedHeight;
    public float crouchedDelay;
    //////////////////////////////////
    //////////////////////////////////
    public float startdashtime;
    public float dashtime;
    public float dashSpeed;
    public float m_currentSashSpeed;
    public float m_chargeRate;
    
    // Start is called before the first frame update
   void Start(){
       m_Controller = gameObject.GetComponent<CharacterController>();
       charBaiscCenter = m_Controller.center;
       charBaiscHeight = m_Controller.height;
       m_biology = gameObject.GetComponent<biology>();
   }

    void Update()
         {
             
             //getting player input from keypress, Dpad or joystick
          m_vertical= Input.GetAxisRaw("Vertical");
          m_Horizontal= Input.GetAxisRaw("Horizontal");
          anim.SetFloat("speed",m_Speed);
          anim.SetBool("IsCrouched",crouched);
          //running = Input.GetKeyDown(KeyCode.e);
          if(m_biology.m_stamina >=1){
          if(Input.GetKeyDown(KeyCode.LeftShift) && dir.magnitude >= 0.1f){
              if(m_biology.m_stamina >=1) {
              running = true;
              }
              else{
                  running =!true;
              }

          }
          }else{
              running =!true;
              wasRunning = true;
          }
          if(Input.GetKeyUp(KeyCode.LeftShift)){
              running = !true;
              wasRunning = true;
          }

          if(m_Speed < 2.5f){
               wasRunning = !true;
          }
          ChargeDash();
          MovementLogic();
          }

        ////////////////////////////////////////////////
        ///////MOVE////////MOVE//////MOVE///////MOVE////
        ////////////////////////////////////////////////


        void MovementLogic(){
            //determing what state of movement the player deseres
            if(!running){
                m_biology.AdjustStamina(0.05f);
            }
              switch (m_movement)
              {
                  case m_MovementType.basic:
                  MoveBasic();
                  break;
                  case m_MovementType.aiming:
                  MoveAim();
                  break;
                  case m_MovementType.climbing:
                  MoveClimb();
                  break;
                  }
            }

            void MoveBasic(){
                //basic movement if for walking state
                WalkingGravity();
                WalkingMovements();
                jump();
                Applycrouched();
                groundCheck();
                applyMoveAnim();

            }

            void MoveAim(){
                //is for aimming state

            }

            void MoveClimb(){
                //is for climbing state
                ClimbingGravity();

            }


            void ChargeDash(){
                   if(Input.GetKey(KeyCode.R)){

              if(m_currentSashSpeed <= dashSpeed)
              {
                  m_currentSashSpeed += m_chargeRate;
                  }
              }

                  if(Input.GetKeyUp(KeyCode.R )&& m_biology.m_stamina >=m_currentSashSpeed){
                  if(m_currentSashSpeed >= dashSpeed/4.0f){
                      StartCoroutine(Dash());

                  }
                  else{
                       m_currentSashSpeed = 0;
                  }
              }
            }

          IEnumerator Dash(){
              crouched =! true;

              startdashtime = Time.time;
              while (Time.time < startdashtime + dashtime ){
                  anim.SetTrigger("dash");
                  m_Controller.Move(m_MoveDir*m_currentSashSpeed*Time.deltaTime);
                  
                  yield return null;

              }
              yield return new WaitForSeconds(dashtime+0.1f);
              m_biology.AdjustStamina(-m_currentSashSpeed);
              m_currentSashSpeed = 0;
               }

         

        void WalkingMovements(){
             //covert movement input from player to 3 demntional cordinates

          dir = new Vector3(m_Horizontal,0f,m_vertical).normalized;
          if(dir.magnitude >= 0.1 )
          {
              m_lookAngle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
              m_smoothingAngle =Mathf.SmoothDampAngle(transform.eulerAngles.y,m_lookAngle,ref m_smoothingVelocity, m_smoothingTime);
              transform.rotation = Quaternion.Euler(0f,m_smoothingAngle,0f);

              m_MoveDir = Quaternion.Euler(0f,m_lookAngle,0f) * Vector3.forward;
              m_Controller.Move(m_MoveDir.normalized*0*Time.deltaTime);
         }
        }

          void Applycrouched ()
            {
                //check if crouched is applied if ot the apply it else unapply it
                if(Input.GetKeyDown(KeyCode.C)){
                    if(crouched == crouched)
                    crouched = !crouched;
                    }
                    Invoke("crouchMod",crouchedDelay);
            }

            void crouchMod(){
                if(crouched){
                m_Controller.center = CharCrouchedCenter;
                m_Controller.height = charChrouchedHeight;
                    
                }
                else{
                    m_Controller.center = charBaiscCenter;
                    m_Controller.height = charBaiscHeight;

                }
            }

        
        ////////////////////////////////////////////////
        ///////JUMP////////JUMP//////JUMP///////JUMP////
        ////////////////////////////////////////////////


        public void jump(){
            if(Input.GetButtonDown("Jump") && isGrounded && m_biology.m_stamina>= 1){
                
        //     //velocity.y =Mathf.Sqrt(jumpHeight * -2f * m_gravity);
        //     anim.SetTrigger("Jump");
        //     Invoke("resetTrigger",0.5f);
             performJump();
            }
         }

            public void performJump(){
                  if( isGrounded){
                velocity.y =Mathf.Sqrt(jumpHeight * -2f * m_gravity);
            anim.SetTrigger("Jump");
            //Invoke("resetTrigger",0.5f); 
            m_biology.AdjustStamina(-1);
             }
             }
    
         public void resetTrigger(){
            anim.ResetTrigger("Jump");
        }


        ////////////////////////////////////////////////
        ///gravity/////gravity/////gravity////gravity///
        ////////////////////////////////////////////////


        //applies a downward force to the player
          void WalkingGravity(){
            velocity.y += m_gravity* Time.deltaTime;
            m_Controller.Move(velocity* Time.deltaTime);

        }

        //applies a pulling force to the player from wall

           void ClimbingGravity(){
            velocity.z += m_gravity* Time.deltaTime;
           // m_Controller.Move(transform.forward*m_gravity* Time.deltaTime*Time.deltaTime);
            transform.Translate(Vector3.forward *m_gravity* Time.deltaTime* Time.deltaTime);

        }
        
        void groundCheck(){
            isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance,GroundMask);

            if(isGrounded && velocity.y < 0){
                velocity.y = -1;
            }
        }

        ////////////////////////////////////////////////
        ///animate/////animate/////animate////animate///
        ////////////////////////////////////////////////


        void applyMoveAnim(){
          
            // handle walking
            if(dir.magnitude >= 0.1 && !running){
                if (m_Speed < _walkSpeed){
                    m_Speed += acceleration;


                }
                //checks if walking is faster then it should be, the regulates it
                if(m_Speed > _walkSpeed && !wasRunning){
                    m_Speed = _walkSpeed;
                }
                if(m_Speed > _walkSpeed && wasRunning){
                    m_Speed -= deceleration;
                }

            }

            //handle running
               if(dir.magnitude >= 0.1 && running){
                if (m_Speed < _runSpeed){
                    m_Speed += acceleration;
                    crouched =! true;
                    

                }
                //checks if running is faster than it should be, the regulates it
                if(m_Speed > _runSpeed){
                    m_Speed = _runSpeed;
                }
                m_biology.AdjustStamina(-0.1f);

            }

            // handle idle
            if(dir.magnitude < 0.1 && m_Speed > 0)
            {
            m_Speed -= deceleration;    
            }
             if(dir.magnitude < 0.1 && m_Speed < 0)
            {
            m_Speed = 0;    
            }

        }
      


    
}
