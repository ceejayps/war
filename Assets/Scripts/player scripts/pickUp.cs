using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour
{
     public enum m_pickUpType { Bio, Coins, Weapon};
     public enum m_bioType { health, mana, stamina};
      [Header("Fundamental States")]
	public m_pickUpType m_catagory;
    public m_bioType m_BioType;
    public float amount;
    private GameObject cal;
    public Text displayText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerStay(Collider other) {
       if(other.tag == "Player"){
           displayText.gameObject.SetActive(true);
           displayText.text ="Pick Up [E]";
           cal = other.gameObject;

           if(Input.GetKeyDown(KeyCode.E)){
           
           PickUp();
           Destroy(gameObject);
           displayText.gameObject.SetActive(!true);
           }
       }
        
    }

    private void OnTriggerExit(Collider other) {
        displayText.gameObject.SetActive(!true);
    }

    void PickUp()
    {
        switch (m_catagory)
        {
    case m_pickUpType.Bio:
            bioPickUp();
            break;
    case m_pickUpType.Weapon:
            weaponPickUp();
            break;
    case m_pickUpType.Coins:
            coinPickUp();
            break;

        }
    }

    void bioPickUp()
    {
   switch (m_BioType)
        {
    case m_bioType.health:
            cal.GetComponent<biology>().AdjustHealth(amount);
            break;
    case m_bioType.stamina:
            cal.GetComponent<biology>().AdjustStamina(amount);
            break;
    case m_bioType.mana:
            cal.GetComponent<biology>().AdjustMana(amount);
            break;
   

        }
    }
    void weaponPickUp(){}
    void coinPickUp(){}
}
