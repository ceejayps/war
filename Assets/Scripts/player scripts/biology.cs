using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class biology : MonoBehaviour
{
  public float m_health;
   float _maxHealth = 256.0f;
    public float m_stamina;
     float _maxStamina= 256.0f;
     public float m_mana;
     float _maxMana= 256.0f;
     bool Dead;
     bool tired;
    bool manaless;
    public  float healthPercent;
    public  float staminaPercent;
    public  float manaPercent;
    int _whole =100;
    public Image healthBar;
    public Image StaminaBar;
    public Image ManaBar;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthPercent = m_health/_maxHealth*_whole;
        staminaPercent = m_stamina/_maxStamina*_whole;
        manaPercent = m_mana/_maxMana*_whole;
        healthBar.fillAmount=healthPercent/_whole;
        StaminaBar.fillAmount=staminaPercent/_whole;
        ManaBar.fillAmount=manaPercent/_whole;

        autoHeal();
    }

    ////////////////////////////////////////////////
    ///////LIFE////////LIFE//////LIFE///////LIFE////
    ////////////////////////////////////////////////
    public void AdjustHealth(float healthPoint)
    {
        //calculate lost and or gain to health
        m_health += healthPoint;

        // if health shoots over max health, normalize to max
        if(m_health >= _maxHealth){
            m_health = _maxHealth;
        }
        // if health drops below 0 health, normalize to 0
       if(m_health <=0)
       {
           m_health = 0;
           Dead = true;
       }

       

    }

    void autoHeal(){
        if(m_health>= 1)
        {
            AdjustHealth(0.03f);
        }
        
    }

    
    ///END-LIFE////END_LIFE///END-LIFE///END-LIFE///

//--------------------------------------------------//

    ////////////////////////////////////////////////
    ///////MANA////////MANA//////MANA///////MANA////
    ////////////////////////////////////////////////
    public void AdjustMana(float manaPoint)
    {
        //calculate lost and or gain to mana
        m_mana += manaPoint;

        // if mana shoots over max mana normalize to max
        if(m_mana >= _maxMana){
            m_mana = _maxMana;
        }
        // if manadrops below 0 mana, normalize to 0
       if(m_mana <=0)
       {
           m_mana = 0;
           manaless = true;
       }

    }
    ///END-MANA////END-MANA///END-MANA//END-MANA///

//--------------------------------------------------//

    ////////////////////////////////////////////////
    ////STAMINA/////STAMINA///STAMINA////STAMINA////
    ////////////////////////////////////////////////
    public void AdjustStamina(float enrgPoint)
    {
        //calculate lost and or gain to stamina
        m_stamina += enrgPoint;

        // if mana shoots over max mana normalize to max
        if(m_stamina >= _maxStamina){
            m_stamina = _maxStamina;
        }
        // if manadrops below 0 mana, normalize to 0
       if(m_stamina <=0)
       {
           m_stamina = 0;
           tired = true;
       }

    }
    ///END-END_////END-END///END-END//END-END///



}
