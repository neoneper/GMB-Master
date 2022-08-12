using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GMB
{
    [CreateAssetMenu(menuName = "GMB/Data/Data_Item")]
    [ResourcesPath("Items")]
    public class Data_Item : Data
    {

        [SerializeField] private Data_ItemCategory _category;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _maxStack = 1;
        [SerializeField] private float _weight = 0.1f;
        [SerializeField] private long _buyPrice = 0;
        [SerializeField] private long _sellPrice = 0;
        [SerializeField] private int _tagsValue = 0;
        [SerializeField] private Data_Element _element = null;
        [SerializeField] private Data_Scope _scope = null;
        [SerializeField] private Data_Usage _usage = null;
        [SerializeField] private Data_Occasion _occasion = null;

       


        public GameObject GetPrefab()
        {
            return _prefab;
        }
        public Data_ItemCategory GetCategory()
        {
            return _category;
        }
        public Data_Element GetElement()
        {
            return _element;
        }
        public Data_Scope GetScope()
        {
            return _scope;
        }
        public Data_Usage GetUsage()
        {
            return _usage;
        }
        public Data_Occasion GetOccasion()
        {
            return _occasion;
        }
        public int GetMaxStack()
        {
            return _maxStack;
        }
        public float GetWeight()
        {
            return _weight;
        }
        public long GetBuyPrice()
        {
            return _buyPrice;
        }
        public long GetSellPrice()
        {
            return _sellPrice;
        }
        public int GetTagsValue()
        {
            return _tagsValue;
        }

        public List<Data_ItemIngredient> GetRecipe()
        {
           
            return new List<Data_ItemIngredient>();
        }
        public List<Data_ItemAttribute> GetEffects()
        {
            return new List<Data_ItemAttribute>();
        }
        public List<Data_ItemCrafter> GetCrafters()
        {
            return new List<Data_ItemCrafter>();
        }

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


    }
}

