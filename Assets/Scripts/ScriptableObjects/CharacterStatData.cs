using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewStats",menuName ="Character/Stats",order =1)]
public class CharacterStatData : ScriptableObject
{
    [System.Serializable]
    public class LevelUps
    {
        /// <summary>
        /// 最大生命值
        /// </summary>
        public int maxHealth;
        /// <summary>
        /// 最大饥饿值
        /// </summary>
        public int maxHungry;
        /// <summary>
        /// 最大金币值
        /// </summary>
        public int maxWealth;
        /// <summary>
        /// 最大精力值
        /// </summary>
        public int maxEnergy;
        /// <summary>
        /// 最大库存槽位
        /// </summary>
        public int maxSlot;
        /// <summary>
        /// 最大堆叠
        /// </summary>
        public int maxStack;
        /// <summary>
        /// 最大负重
        /// </summary>
        public float maxEncumbrance;
    }


    public bool setManually = false;
    public bool saveDataOnClose = false;

    public int maxHealth = 0;
    public int currentHealth = 0;

    public int maxHungry = 0;
    public int currentHungry = 0;

    public int maxWealth = 0;
    public int currentWealth = 0;

    public int maxEnergy = 0;
    public int currentEnergy = 0;

    public int maxSlot = 0;

    public int maxStack = 0;

    public float maxEncumbrance = 0f;
    public float currentEncumbrance = 0f;

    public int charExperience = 0;
    public int charLevel = 0;

    public LevelUps[] levelUps;

    public void ApplyHealth(int healthAmount)
    {
        if((currentHealth+healthAmount)>maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healthAmount;
        }
    }

    public void ApplyHungry(int hungryAmount)
    {
        if((currentHungry+hungryAmount)>maxHungry)
        {
            currentHungry = maxHungry;
        }
        else
        {
            currentHungry += hungryAmount;
        }
    }


    public void GiveWealth(int wealthAmount)
    {
        if ((currentWealth + wealthAmount) > maxWealth)
        {
            currentWealth = maxWealth;
        }
        else
        {
            currentWealth += wealthAmount;
        }
    }

   
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeHungry(int amount)
    {
        currentHungry -= amount;

        if (currentHungry < 0)
        {
            currentHungry = 0;
        }
    }

    #region Character Level Up and Death
    private void Death()
    {
        Debug.Log("You kicked it! Sorry Moosa-Magoose.");
        //Call to Game Manager for Death State to trigger respawn
        //Dispaly the Death visualization
    }

    private void LevelUp()
    {
        charLevel += 1;
        //Display Level Up Visualization

        maxHealth = levelUps[charLevel - 1].maxHealth;
        maxHungry = levelUps[charLevel - 1].maxHungry;
        maxWealth = levelUps[charLevel - 1].maxWealth;
        maxEnergy = levelUps[charLevel - 1].maxEnergy;
        maxSlot = levelUps[charLevel - 1].maxSlot;
        maxStack = levelUps[charLevel - 1].maxStack;
        maxEncumbrance = levelUps[charLevel - 1].maxEncumbrance;
    }
    #endregion
}
