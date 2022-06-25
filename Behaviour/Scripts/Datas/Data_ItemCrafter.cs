using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMB
{
    [CreateAssetMenu(menuName = "GMB/Data/Data_ItemCrafter")]
    [ResourcesPath("ItemsCrafters")]
    public class Data_ItemCrafter : Data
    {
        [SerializeField] Data_Item _item;
        [SerializeField] Data_Item _owner;
        [SerializeField] private float _requiredValue = 1;

        public Data_Item GetItem()
        {
            return _item;
        }

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

