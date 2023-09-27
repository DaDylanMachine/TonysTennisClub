using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    //GameObject variables needed to refrence in this script.
    public Slider staminaBar;
    public static StaminaBar instance;

    //Variables used to customize the stamina bar.
    public int maxStamina = 150;
    private int currentStamina;
    public int regenRate = 100;
    public bool staminaDepleted = false;

    //Variables to be used in the coroutine. Ensures a new variable isn't made everytime.
    private WaitForSeconds regenTick = new(0.01f);//note this value as it might affect performance.
    private Coroutine regen;

    //Awake is called the first time the script is run.
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Make sure the stamina bar is full on startup.
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    //Function that depletes stamina as needed.
    public void UseStamina(int amount)
    {
        //Depletes the stamina bar by the amount requested until the bar is at 0, in which it then denotes the bar as depleted.
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            //Stop the coroutine if this function runs before the coroutine finishes.
            if (regen != null)
                StopCoroutine(regen);
            regen = StartCoroutine(RegenStamina());
        }
        else
            staminaDepleted = true;
    }

   //Coroutine that regens stamina over time.
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(2);


        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / regenRate;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
        staminaDepleted = false;
    }
}
