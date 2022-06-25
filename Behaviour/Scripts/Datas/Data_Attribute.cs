using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMB
{
    [CreateAssetMenu(menuName = "GMB/Data/Data_Attribute")]
    [ResourcesPath("Attributes")]
    public class Data_Attribute : Data
    {
        [SerializeField] private Data_AttributeCategory _category;
        [SerializeField] private float _maxValue;
        public float GetMaxValue() { return _maxValue; }

        public override string GetNameAsRelativePath()
        {

            string result = "";

            if (_category != null)
                result += _category.GetFriendlyName();
            else
                result += StringsProvider._UNCATEGORIZED;

            result += "/" + GetFriendlyName();

            return result;
        }

#if UNITY_EDITOR
        public void SetMaxValue(float value)
        {
            _maxValue = value;
            this.SaveAsset();
        }
        public void SetCategory(Data_AttributeCategory category)
        {
            _category = category;
            this.SaveAsset();
        }
#endif


    }
}

