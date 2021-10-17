using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class playerSetUp : MonoBehaviour
{
     CharacterController m_Controller;
     PlayerMoto2 m_playerMoto;
     PlayerStates m_PlayerState;
     PlayerAimMoto m_PlayerAimMoto;
     public Animator m_anim;
    public float charHeight = 1.8f;
    public Vector3 charCenter = new Vector3(0f,0.9f,0f);
    public Vector3 CharCrouchedCenter;
    public float charChrouchedHeight;
    public float crouchedDelay;
    public float charRadius = 0.34f;
    public float SommothTime = 0.25f;
    public float m_gravity = -9.81f;
    public LayerMask m_groundMask;

    public float m_dashTime = 0.25f;
    public float m_dashSpeed = 30f;
        public float m_groundDistance = 0.4f;
    public float m_jumpHeight = 3f;
    public float m_chargeRate;
    public float RunSpeed;
    public float acceleration;
    public float walkSpeed;

    

    // Start is called before the first frame update
    void Awake() {
        //char controller
        gameObject.layer = 8;
    
        m_anim = gameObject.GetComponent<Animator>();
      m_Controller = gameObject.AddComponent<CharacterController>();
      
      m_playerMoto = gameObject.AddComponent<PlayerMoto2>();
      SetUpPlayerMoto();
      
          if(gameObject.GetComponent<PlayerStates>() == null){
      m_PlayerState =gameObject.AddComponent<PlayerStates>();
          }
      
      


    }
    void Start()
    {
        SetupCharControl();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupCharControl()
    {
        m_Controller.height = charHeight;
        m_Controller.center = charCenter;
        m_Controller.radius = charRadius;
    }
    void SetUpPlayerMoto()
    {
        m_playerMoto.Camera = Camera.main.transform;
        m_playerMoto.anim = m_anim;
        m_playerMoto.m_smoothingTime =SommothTime;
        m_playerMoto.GroundCheck =transform.Find("groundCheck");
        m_playerMoto.m_gravity = m_gravity;
        m_playerMoto.GroundMask = m_groundMask;
        m_playerMoto.GroundDistance = m_groundDistance;
        m_playerMoto.jumpHeight = m_jumpHeight;
        m_playerMoto.dashtime =m_dashTime;
        m_playerMoto.dashSpeed =m_dashSpeed;
        m_playerMoto.m_chargeRate = m_chargeRate;
        m_playerMoto._runSpeed = RunSpeed;
        m_playerMoto.acceleration =acceleration;
        m_playerMoto._walkSpeed = walkSpeed;
        m_playerMoto.CharCrouchedCenter = CharCrouchedCenter;
        m_playerMoto.charChrouchedHeight = charChrouchedHeight;
        m_playerMoto.crouchedDelay = crouchedDelay;
        // m_playerMoto.charChrouchedHeight = charHeight;
        // m_playerMoto.charBaiscCenter = charCenter;

    }

    void SetUpPlayerAnimMotot()
    {

    }

    void SetUpPlayerState()
    {

    }
}

