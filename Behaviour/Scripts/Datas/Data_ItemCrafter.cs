using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMB
{
    /// <summary>
    /// Este dado e um container que guarda informacao sobre item construtor que pode ser utilizado para construir um item origem!
    /// <seealso cref="GetItem"/> = Item construtor e <see cref="GetOwner"/> = Item que pode ser construido
    /// <para>
    /// - Informa que um determinado item <see cref="GetOwner"/>, pode utilizar outro item <see cref="GetItem"/> como construtor.</para>
    /// <para>
    /// - Adicionalmente informa um campo de valor <see cref="GetRequiredValue"/> que pode ser utilizado por teceiros para
    /// condicionar o uso do item Construtor, como por exemplo, tempo de preparo!
    /// </para>
    /// </summary>
    [CreateAssetMenu(menuName = "GMB/Data/Data_ItemCrafter")]
    [ResourcesPath("ItemsCrafters")]
    public class Data_ItemCrafter : Data
    {
        [SerializeField] Data_Item _item;
        [SerializeField] Data_Item _owner;
        [SerializeField] private float _requiredValue = 1;

        /// <summary>
        /// <seealso cref="Data_Item"/>  Item construtor. Item base que sera utilizado para construir o <see cref="GetOwner"/>
        /// </summary>
        /// <returns></returns>
        public Data_Item GetItem()
        {
            return _item;
        }

        /// <summary>
        /// <seealso cref="Data_Item"/>  Item que pode ser construido com <see cref="GetItem"/>
        /// </summary>
        /// <returns></returns>
        public Data_Item GetOwner()
        {
            return _owner;
        }

        public float GetRequiredValue()
        {
            return _requiredValue;
        }

        public override Sprite GetIcon()
        {
            return GetItem().GetIcon();
        }

        public override string GetDescription()
        {
            return GetItem().GetDescription();
        }

        public override string GetFriendlyName()
        {
            return GetItem().GetFriendlyName();
        }

#if UNITY_EDITOR

        public void SetItem(Data_Item item)
        {
            _item = item;
            this.SaveAsset();
        }
        public void SetOwner(Data_Item item)
        {
            _owner = item;
            this.SaveAsset();
        }
        public void SetRequiredValue(float value)
        {
            _requiredValue = value;
            this.SaveAsset();
        }

#endif
    }
}

