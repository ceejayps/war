using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

public class PlayerStates : MonoBehaviour
{
   
     public enum m_MovementType { basic, aiming, climbing};
      [Header("Fundamental States")]
	public m_MovementType m_movement;

    public enum m_WeaponType { basic, sword, bow};
	public m_WeaponType m_weaponClass;

    public enum m_MagicType { earth, fire, water, wind, lightening, rawMana};
	public m_MagicType  m_MagicClass;
    ////////////////////////////////////////////////////////////////////////
    [Header("Max Stats")]
    public int MaxHealth;
    public int MaxStamina;
    public int MaxMana;
    public int MaxDurability;
    public int MaxStrength;
    public int MaxSpeed;

     [Header("Current Stats")]
    public int CurrentHealth;
    public int CurrentStamina;
    public int CurrentMana;
    //[Header("Physical attributes)]
    [MinMaxSlider(0,50)]
    public Vector2 WalkSpeed;
    [MinMaxSlider(0,50)]
    public Vector2 RunSpeed;
    public Vector2 AttackSpeed;
    [MinMaxSlider(0,256)]
    public Vector2 ReactionSpeed;
    [MinMaxSlider(0,256)]
    public Vector2 Durability;
    [MinMaxSlider(0,256)]
    public Vector2 Strength;


    [Header("Attacks Potency")]
    [MinMaxSlider(0,256)]
    public Vector2 Range;
    [MinMaxSlider(0,100)]
    public Vector2 Radius;
    [MinMaxSlider(0,100)]
    public Vector2 Accuracy;
    [MinMaxSlider(0,100)]
    public Vector2 Distruction;
    [MinMaxSlider(0,256)]
    public Vector2 control;
    [MinMaxSlider(0,256)]
    public Vector2 CoolDown;
    [MinMaxSlider(0,256)]
    public Vector2 RechargeRate;

    ////////////////////////////
    MonoBehaviour basicController;
    MonoBehaviour AIMController;
    MonoBehaviour ClimbController;
    /////////////////////////////
    public Animator m_cmv;







    // Start is called before the first frame update
    void Start()
    {
        basicController = gameObject.GetComponent<PlayerMoto2>();
       AIMController = gameObject.GetComponent<PlayerAimMoto>();
       ClimbController = gameObject.GetComponent<PlayerClimbingMoto>();
       m_cmv = GameObject.Find("/m_cmv").GetComponent<Animator>();
       }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // m_movement = m_MovementType.aiming;
        }

        else if(Input.GetKeyUp(KeyCode.Q))
        {
             m_movement = m_MovementType.basic;
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



    void MoveBasic()
    {
        basicController.enabled = true;
        AIMController.enabled =false;
        ClimbController.enabled=false;
        m_cmv.SetBool("IsAiming",!true);

    }
    void MoveAim()
    {
        basicController.enabled =false;
        AIMController.enabled =!false;
        ClimbController.enabled=false;
        m_cmv.SetBool("IsAiming",true);

    }
    void MoveClimb()
    {
        basicController.enabled =false;
        AIMController.enabled =false;
        ClimbController.enabled=!false;

    }
}

