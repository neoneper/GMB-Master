using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMB
{
    /// <summary>
    /// Este dado e um container que guarda informacao sobre item ingredient que pode ser utilizado para fabricar um item origem!
    /// <seealso cref="GetItem"/> = Item do ingredient e <see cref="GetOwner"/> = Item que pode ser fabricado com este item de ingrediente
    /// <para>
    /// - Informa que um determinado item <see cref="GetOwner"/>, pode utilizar outro item <see cref="GetItem"/> como ingredient para ser fabricado.</para>
    /// <para>
    /// - Adicionalmente informa um campo de valor <see cref="GetRequiredValue"/> que pode ser utilizado por teceiros para
    /// condicionar o uso do item Ingrediente <see cref="GetItem"/>, como por exemplo, quantidade de iten ingrediente necessario para fabricar o item <see cref="GetOwner"/>!
    /// </para>
    /// </summary>
    [CreateAssetMenu(menuName = "GMB/Data/Data_ItemIngredient")]
    [ResourcesPath("ItemsIngredients")]
    public class Data_ItemIngredient : Data
    {
        [SerializeField] Data_Item _item;
        [SerializeField] Data_Item _owner;
        [SerializeField] private int _requiredValue = 1;

        /// <summary>
        /// <see cref="Data_Item"/> deste Data_ItemIngredient. Este e o dado original que sera utilizado para fabricar o item <see cref="GetOwner"/> 
        /// </summary>
        /// <returns></returns>
        public Data_Item GetItem()
        {
            return _item;
        }

        /// <summary>
        /// <seealso cref="Data_Item"/> que pode ser feito apartir do <see cref="GetItem"/>
        /// </summary>
        /// <returns></returns>
        public Data_Item GetOwner()
        {
            return _owner;
        }

        /// <summary>
        /// Quantidade necessaria do <see cref="GetItem"/>, para a fabricao do do item <see cref="GetOwner"/>
        /// </summary>
        /// <returns></returns>
        public int GetRequiredValue()
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
        public void SetRequiredValue(int value)
        {
            _requiredValue = value;
            this.SaveAsset();
        }

#endif
    }
}

