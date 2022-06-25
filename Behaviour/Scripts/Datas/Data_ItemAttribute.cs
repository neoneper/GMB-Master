using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMB
{
    [CreateAssetMenu(menuName = "GMB/Data/Data_ItemAttribute")]
    [ResourcesPath("ItemsAttributes")]
    public class Data_ItemAttribute : Data
    {
        [SerializeField] Data_Attribute _attribute;
        [SerializeField] Data_Item _owner;
        [Range(-1, 1)]
        [SerializeField] private float _requiredAmountValue = 0;
        [SerializeField] private float _requiredTimeLapse = 0;

        public Data_Attribute GetAttribute()
        {
            return _attribute;
        }

        public Data_Item GetOwner()
        {
            return _owner;
        }

        public float GeetRequiredAmountValue()
        {
            return _requiredAmountValue;
        }

        public float GetRequiredTimeLapse()
        {
            return _requiredTimeLapse;
        }

        public override Sprite GetIcon()
        {
            return GetAttribute().GetIcon();
        }

        public override string GetDescription()
        {
            return GetAttribute().GetDescription();
        }

        public override string GetFriendlyName()
        {
            return GetAttribute().GetFriendlyName();
        }



#if UNITY_EDITOR

        public void SetAttribute(Data_Attribute attribute)
        {
            _attribute = attribute;
            this.SaveAsset();
        }
        public void SetOwner(Data_Item item)
        {
            _owner = item;
            this.SaveAsset();
        }
        public void SetRequiredAmountValue(float value)
        {
            _requiredAmountValue = value;
            this.SaveAsset();
        }
        public void SetRequiredTimeLapse(float value)
        {
            _requiredTimeLapse = value;
            this.SaveAsset();
        }

#endif
    }
}

