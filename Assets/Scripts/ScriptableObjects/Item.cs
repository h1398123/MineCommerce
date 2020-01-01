using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewItem", menuName = "Item/New Item", order = 1)]
public class Item : ScriptableObject
{
    public string itemName = "New Item";

    public string itemDescripttion = "Item Descripttion";
    public ItemType itemType = ItemType.EMPTY;
    public int itemLevel = 0;
    public int itemPrice = 0;

    public Sprite itemIcon = null;//物品贴图
    public Rigidbody weaponSlotObject = null;// 作用于Player上的实体物品，比如盔甲和武器

    /// <summary>
    /// 是否属于装备
    /// </summary>
    public bool isEquipped = false;
    /// <summary>
    /// 是否可以实际互动
    /// </summary>
    public bool isInteractable = false;
    /// <summary>
    /// 是否可以装入背包
    /// </summary>
    public bool isStorable = false;
    /// <summary>
    /// 是否属于独特物品
    /// </summary>
    public bool isUnique = false;
    /// <summary>
    /// 是否不可摧毁
    /// </summary>
    public bool isIndestructable = false;
    /// <summary>
    /// 是否属于任务物品
    /// </summary>
    public bool isQuestItem = false;
    /// <summary>
    /// 是否可以堆叠
    /// </summary>
    public bool isStackable = false;
    /// <summary>
    /// 是否只能使用一次，即使用后会销毁
    /// </summary>
    public bool destroyOnUse = false;
    /// <summary>
    /// 物品重量
    /// </summary>
    public float itemWeight = 0f;
}



/// <summary>
/// 物品类型
/// </summary>
public enum ItemType
{
    /// <summary>
    /// 药物
    /// </summary>
    HEALTH,
    /// <summary>
    /// 财物
    /// </summary>
    WEALTH,
    /// <summary>
    /// 食物
    /// </summary>
    FOOD,
    /// <summary>
    /// 军需
    /// </summary>
    WAR,
    /// <summary>
    /// 资源
    /// </summary>
    MATERIAL,
    /// <summary>
    /// Buff物品
    /// </summary>
    BUFF,
    /// <summary>
    /// 其他
    /// </summary>
    OTHER,
    /// <summary>
    /// 特殊
    /// </summary>
    SPECIAL,
    /// <summary>
    /// 空
    /// </summary>
    EMPTY
};
