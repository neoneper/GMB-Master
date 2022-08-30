using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMB;
using System;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager.UI;

namespace GMBEditor
{
    public class DataItem_Window : GMBEditorWindow<Data_Item>
    {

        Button _bt_category;
        Button _bt_element;
        Button _bt_scope;
        Button _bt_usage;
        Button _bt_occasion;

        ObjectField _objectField_category;
        ObjectField _objectField_usage;
        ObjectField _objectField_element;
        ObjectField _objectField_scope;
        ObjectField _objectField_occasion;

        GMBEditorListView<Data_ItemIngredient> _listview_recipe;
        GMBEditorListView<Data_ItemCrafter> _listview_crafters;
        GMBEditorListView<Data_ItemAttribute> _listview_attributes;
        GMBEditorTagsView _tags;


        //WIndow Callbacks
        protected override void OnCreateGUI()
        {

            InitializeItem();
            InitializeAttributes();
            InitializeRecipe();
            InitializeCrafters();

            //Start selected item content
            //listview.SetSelectionItem(0);
        }

        protected override void OnCloseGUI()
        {
            UnInitializeItem();
            UnInitializeAttributes();
            UnInitializeRecipe();
            UnInitializeCrafters();

        }
        protected override void OnSelectedItemChanged()
        {
            _listview_recipe.Refresh();
            _listview_crafters.Refresh();
            _listview_attributes.Refresh();
            _tags.RefreshTagsContent(listview_selectedItem.GetTags());


            _bt_category.text = listview_selectedItem.GetCategory() == null ? "Find" : listview_selectedItem.GetCategory().GetFriendlyName();
            _bt_element.text = listview_selectedItem.GetElement() == null ? "Find" : listview_selectedItem.GetElement().GetFriendlyName();
            _bt_occasion.text = listview_selectedItem.GetOccasion() == null ? "Find" : listview_selectedItem.GetOccasion().GetFriendlyName();
            _bt_scope.text = listview_selectedItem.GetScope() == null ? "Find" : listview_selectedItem.GetScope().GetFriendlyName();
            _bt_usage.text = listview_selectedItem.GetUsage() == null ? "Find" : listview_selectedItem.GetUsage().GetFriendlyName();



        }




        #region PRIVATE UTIL FUNCTIONS
        private void InitializeItem()
        {
            _bt_category = GetElement<Button>("bt_category");
            _bt_element = GetElement<Button>("bt_element");
            _bt_scope = GetElement<Button>("bt_scope");
            _bt_occasion = GetElement<Button>("bt_occasion");
            _bt_usage = GetElement<Button>("bt_usage");

            _tags = new GMBEditorTagsView(this, GetElement<Button>("bt_add_tag"), GetElement<VisualElement>("tags").Q("content"));
            _tags.OnSerializedObjectItemRequest += OnTagObjectItemRequest;


            _objectField_category = GetElement<ObjectField>("data_category");
            _objectField_element = GetElement<ObjectField>("data_element");
            _objectField_occasion = GetElement<ObjectField>("data_occasion");
            _objectField_scope = GetElement<ObjectField>("data_scope");
            _objectField_usage = GetElement<ObjectField>("data_usage");


            _bt_category.RegisterCallback<PointerDownEvent>(OnCategorySearch, TrickleDown.TrickleDown);
            _bt_element.RegisterCallback<PointerDownEvent>(OnElementSearch, TrickleDown.TrickleDown);
            _bt_occasion.RegisterCallback<PointerDownEvent>(OnOccasionSearch, TrickleDown.TrickleDown);
            _bt_scope.RegisterCallback<PointerDownEvent>(OnScopeSearch, TrickleDown.TrickleDown);
            _bt_usage.RegisterCallback<PointerDownEvent>(OnUsageSearch, TrickleDown.TrickleDown);

            _objectField_category.RegisterValueChangedCallback(OnItem_CategoryChanged);
            _objectField_element.RegisterValueChangedCallback(OnItem_ElementChanged);
            _objectField_occasion.RegisterValueChangedCallback(OnItem_OccasionChanged);
            _objectField_scope.RegisterValueChangedCallback(OnItem_ScopeChanged);
            _objectField_usage.RegisterValueChangedCallback(OnItem_UsageChanged);
        }
        private void UnInitializeItem()
        {
            _bt_category.UnregisterCallback<PointerDownEvent>(OnCategorySearch, TrickleDown.TrickleDown);
            _bt_element.UnregisterCallback<PointerDownEvent>(OnElementSearch, TrickleDown.TrickleDown);
            _bt_occasion.UnregisterCallback<PointerDownEvent>(OnOccasionSearch, TrickleDown.TrickleDown);
            _bt_scope.UnregisterCallback<PointerDownEvent>(OnScopeSearch, TrickleDown.TrickleDown);
            _bt_usage.UnregisterCallback<PointerDownEvent>(OnUsageSearch, TrickleDown.TrickleDown);

            _objectField_category.UnregisterValueChangedCallback(OnItem_CategoryChanged);
            _objectField_element.UnregisterValueChangedCallback(OnItem_ElementChanged);
            _objectField_occasion.UnregisterValueChangedCallback(OnItem_OccasionChanged);
            _objectField_scope.UnregisterValueChangedCallback(OnItem_ScopeChanged);
            _objectField_usage.UnregisterValueChangedCallback(OnItem_UsageChanged);

            _tags.OnSerializedObjectItemRequest += OnTagObjectItemRequest;
            _tags.Unitialize();
        }



        private void InitializeAttributes()
        {
            _listview_attributes = new GMBEditorListView<Data_ItemAttribute>();
            _listview_attributes.Initialize(contentVisualElement.Q("attributes"), "listview", "bt_add", "bt_remove", DataEditorUtility.GetAbsoluteResourcesDatasPath<Data_ItemAttribute>());
            _listview_attributes.OnListViewBindItemCallback += OnAttribute_BindItem;
            _listview_attributes.OnListViewMakeItemCallback += OnAttribute_MakeItem;
            _listview_attributes.OnListViewSelectionChangedCallback += OnAttribute_SelectionChanged;
            _listview_attributes.OnListViewAddButtonCallback += OnAttribute_SearchItemRequest;
            _listview_attributes.OnListViewLoadDatasCallback += OnAttribute_LoadDatas;
        }
        private void UnInitializeAttributes()
        {
            _listview_attributes.OnListViewBindItemCallback += OnAttribute_BindItem;
            _listview_attributes.OnListViewMakeItemCallback += OnAttribute_MakeItem;
            _listview_attributes.OnListViewSelectionChangedCallback += OnAttribute_SelectionChanged;
            _listview_attributes.OnListViewAddButtonCallback += OnAttribute_SearchItemRequest;
            _listview_attributes.OnListViewLoadDatasCallback += OnAttribute_LoadDatas;
            _listview_attributes.UnInitialize();

        }
        private void InitializeRecipe()
        {
            _listview_recipe = new GMBEditorListView<Data_ItemIngredient>();
            _listview_recipe.Initialize(contentVisualElement.Q("recipe"), "listview", "bt_add", "bt_remove", DataEditorUtility.GetAbsoluteResourcesDatasPath<Data_ItemIngredient>());
            _listview_recipe.OnListViewBindItemCallback += OnRecipe_BindItem;
            _listview_recipe.OnListViewMakeItemCallback += OnRecipe_MakeItem;
            _listview_recipe.OnListViewSelectionChangedCallback += OnRecipe_SelectionChanged;
            _listview_recipe.OnListViewAddButtonCallback += OnRecipe_SearchItemRequest;
            _listview_recipe.OnListViewLoadDatasCallback += OnRecipe_LoadDatas;
        }
        private void UnInitializeRecipe()
        {
            _listview_recipe.OnListViewBindItemCallback -= OnRecipe_BindItem;
            _listview_recipe.OnListViewMakeItemCallback -= OnRecipe_MakeItem;
            _listview_recipe.OnListViewSelectionChangedCallback -= OnRecipe_SelectionChanged;
            _listview_recipe.OnListViewAddButtonCallback -= OnRecipe_SearchItemRequest;
            _listview_recipe.OnListViewLoadDatasCallback -= OnRecipe_LoadDatas;
            _listview_recipe.UnInitialize();
        }
        private void InitializeCrafters()
        {
            //
            _listview_crafters = new GMBEditorListView<Data_ItemCrafter>();
            _listview_crafters.Initialize(contentVisualElement.Q("crafters"), "listview", "bt_add", "bt_remove", DataEditorUtility.GetAbsoluteResourcesDatasPath<Data_ItemCrafter>());
            _listview_crafters.OnListViewBindItemCallback += OnCrafters_BindItem;
            _listview_crafters.OnListViewMakeItemCallback += OnCrafters_MakeItem;
            _listview_crafters.OnListViewSelectionChangedCallback += OnCrafters_SelectionChanged;
            _listview_crafters.OnListViewAddButtonCallback += OnCrafters_SearchItemRequest;
            _listview_crafters.OnListViewLoadDatasCallback += OnCrafters_LoadDatas;
        }
        private void UnInitializeCrafters()
        {
            _listview_crafters.OnListViewBindItemCallback -= OnCrafters_BindItem;
            _listview_crafters.OnListViewMakeItemCallback -= OnCrafters_MakeItem;
            _listview_crafters.OnListViewSelectionChangedCallback -= OnCrafters_SelectionChanged;
            _listview_crafters.OnListViewAddButtonCallback -= OnCrafters_SearchItemRequest;
            _listview_crafters.OnListViewLoadDatasCallback -= OnCrafters_LoadDatas;
            _listview_crafters.UnInitialize();
        }

        #endregion

        #region FUNCTION CALLBACKS

        //Tags
        private SerializedObject OnTagObjectItemRequest()
        {
            return listview_selectedItem.GetSerializedObject();
        }
        //Item
        private void OnCategorySearch(PointerDownEvent evt)
        {
            DataEditorUtility.ShowSearchWindow<Data_ItemCategory>("Item Categories", GUIUtility.GUIToScreenPoint(evt.position), OnItem_CategoryRequest);
        }
        private void OnUsageSearch(PointerDownEvent evt)
        {
            DataEditorUtility.ShowSearchWindow<Data_Usage>("Item Usages", GUIUtility.GUIToScreenPoint(evt.position), OnItem_UsageRequest);
        }
        private void OnElementSearch(PointerDownEvent evt)
        {
            DataEditorUtility.ShowSearchWindow<Data_Element>("Item Elements", GUIUtility.GUIToScreenPoint(evt.position), OnItem_ElementRequest);
        }
        private void OnScopeSearch(PointerDownEvent evt)
        {
            DataEditorUtility.ShowSearchWindow<Data_Scope>("Item Scopes", GUIUtility.GUIToScreenPoint(evt.position), OnItem_ScopeRequest);
        }
        private void OnOccasionSearch(PointerDownEvent evt)
        {
            DataEditorUtility.ShowSearchWindow<Data_Occasion>("Item Occasions", GUIUtility.GUIToScreenPoint(evt.position), OnItem_OccasionRequest);
        }



        private void OnItem_CategoryRequest(GMBEditorSearchProvider.SearchResult result)
        {

            if (result.resultFriendlyName == EditorStringsProvider._LISTVIEW_NEW_OPTIONS_)
            {
                GetGMBWindow().OnMenuSelected(typeof(DataItemCategory_Window));
                return;
            }

            SerializedObject serializedObject = listview_selectedItem.GetSerializedObject();
            serializedObject.FindProperty("_category").objectReferenceValue = result.GetDataFile<Data_ItemCategory>();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
        private void OnItem_ScopeRequest(GMBEditorSearchProvider.SearchResult result)
        {
            if (result.resultFriendlyName == EditorStringsProvider._LISTVIEW_NEW_OPTIONS_)
            {
                GetGMBWindow().OnMenuSelected(typeof(DataScope_Window));
                return;
            }

            SerializedObject serializedObject = listview_selectedItem.GetSerializedObject();
            serializedObject.FindProperty("_scope").objectReferenceValue = result.GetDataFile<Data_Scope>();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
        private void OnItem_UsageRequest(GMBEditorSearchProvider.SearchResult result)
        {
            if (result.resultFriendlyName == EditorStringsProvider._LISTVIEW_NEW_OPTIONS_)
            {
                GetGMBWindow().OnMenuSelected(typeof(DataUsage_Window));
                return;
            }

            SerializedObject serializedObject = listview_selectedItem.GetSerializedObject();
            serializedObject.FindProperty("_usage").objectReferenceValue = result.GetDataFile<Data_Usage>();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
        private void OnItem_OccasionRequest(GMBEditorSearchProvider.SearchResult result)
        {
            if (result.resultFriendlyName == EditorStringsProvider._LISTVIEW_NEW_OPTIONS_)
            {
                GetGMBWindow().OnMenuSelected(typeof(DataOccasion_Window));
                return;
            }
            SerializedObject serializedObject = listview_selectedItem.GetSerializedObject();
            serializedObject.FindProperty("_occasion").objectReferenceValue = result.GetDataFile<Data_Occasion>();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }
        private void OnItem_ElementRequest(GMBEditorSearchProvider.SearchResult result)
        {
            if (result.resultFriendlyName == EditorStringsProvider._LISTVIEW_NEW_OPTIONS_)
            {
                GetGMBWindow().OnMenuSelected(typeof(DataElement_Window));
                return;
            }
            SerializedObject serializedObject = listview_selectedItem.GetSerializedObject();
            serializedObject.FindProperty("_element").objectReferenceValue = result.GetDataFile<Data_Element>();
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        private void OnItem_CategoryChanged(ChangeEvent<UnityEngine.Object> evt)
        {
            Data_ItemCategory category = evt.newValue as Data_ItemCategory;
            _bt_category.text = category == null ? "Find" : category.GetFriendlyName();
            listview.RefreshSelectedItem();
        }
        private void OnItem_ScopeChanged(ChangeEvent<UnityEngine.Object> evt)
        {
            Data_Scope scope = evt.newValue as Data_Scope;
            _bt_scope.text = scope == null ? "Find" : scope.GetFriendlyName();
        }
        private void OnItem_ElementChanged(ChangeEvent<UnityEngine.Object> evt)
        {
            Data_Element element = evt.newValue as Data_Element;
            _bt_element.text = element == null ? "Find" : element.GetFriendlyName();
        }
        private void OnItem_UsageChanged(ChangeEvent<UnityEngine.Object> evt)
        {
            Data_Usage usage = evt.newValue as Data_Usage;
            _bt_usage.text = usage == null ? "Find" : usage.GetFriendlyName();
        }
        private void OnItem_OccasionChanged(ChangeEvent<UnityEngine.Object> evt)
        {
            Data_Occasion occasion = evt.newValue as Data_Occasion;
            _bt_occasion.text = occasion == null ? "Find" : occasion.GetFriendlyName();
        }

        protected override void OnListView_BindItem_Requested(VisualElement element, Data_Item item)
        {
            base.OnListView_BindItem_Requested(element, item);

            Label subTitle = element.Q<Label>("subtitle");
            subTitle.text = item.GetCategory()?.GetFriendlyName();
        }

        //Recipe
        private void OnRecipe_SearchItemRequest(PointerDownEvent evt)
        {

            List<Data_Item> itemsToIgnore = new List<Data_Item>();
            itemsToIgnore.Add(listview_selectedItem);
            itemsToIgnore.AddRange(_listview_recipe.Items.Select(r => r.GetItem()).ToList());

            DataEditorUtility.ShowSearchWindow<Data_Item>("Items for recipe", GUIUtility.GUIToScreenPoint(evt.position), OnRecipe_AddItemRequest, itemsToIgnore);
        }
        private void OnRecipe_AddItemRequest(GMBEditorSearchProvider.SearchResult obj)
        {
           

            Data_Item item = obj.GetDataFile<Data_Item>();

            if (item == null)
                return;

            Data_ItemIngredient ingredient = ScriptableObject.CreateInstance<Data_ItemIngredient>();
            ingredient.SetItem(item);
            ingredient.SetOwner(listview_selectedItem);
            _listview_recipe.OnAddNewIntemFromInstance(ingredient);
        }
        private void OnRecipe_SelectionChanged(List<Data_ItemIngredient> items)
        {

        }
        private void OnRecipe_BindItem(VisualElement element, Data_ItemIngredient item)
        {
            SerializedObject serializedObject = item.GetSerializedObject();
            element.Unbind();
            element.Q<Label>("title").text = item.GetFriendlyName();
            element.Q<IntegerField>().value = item.GetRequiredValue();
            element.Bind(serializedObject);
        }
        private VisualTreeAsset OnRecipe_MakeItem()
        {
            return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(GetTemplate_ListViewIngredientItemFilePath());
        }
        private List<Data_ItemIngredient> OnRecipe_LoadDatas(List<Data_ItemIngredient> loadedItems)
        {
            return loadedItems.Where(r => r.GetOwner() == listview_selectedItem).ToList();
        }
        //Crafters
        private void OnCrafters_SearchItemRequest(PointerDownEvent evt)
        {
            List<Data_Item> itemsToIgnore = new List<Data_Item>();
            itemsToIgnore.Add(listview_selectedItem);
            itemsToIgnore.AddRange(_listview_crafters.Items.Select(r => r.GetItem()).ToList());
            DataEditorUtility.ShowSearchWindow<Data_Item>("Items of craft", GUIUtility.GUIToScreenPoint(evt.position), OnCrafters_AddItemRequest, itemsToIgnore);
        }
        private void OnCrafters_AddItemRequest(GMBEditorSearchProvider.SearchResult obj)
        {
            Data_Item item = obj.GetDataFile<Data_Item>();

            if (item == null)
                return;

            Data_ItemCrafter crafter = ScriptableObject.CreateInstance<Data_ItemCrafter>();
            crafter.SetItem(item);
            crafter.SetOwner(listview_selectedItem);
            _listview_crafters.OnAddNewIntemFromInstance(crafter);
        }
        private void OnCrafters_SelectionChanged(List<Data_ItemCrafter> items)
        {

        }
        private void OnCrafters_BindItem(VisualElement element, Data_ItemCrafter item)
        {
            SerializedObject serializedObject = item.GetSerializedObject();
            element.Unbind();
            element.Q<Label>("title").text = item.GetFriendlyName();
            element.Q<FloatField>().value = item.GetRequiredValue();
            element.Bind(serializedObject);
        }
        private VisualTreeAsset OnCrafters_MakeItem()
        {
            return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(GetTemplate_ListViewCrafterItemFilePath());
        }
        private List<Data_ItemCrafter> OnCrafters_LoadDatas(List<Data_ItemCrafter> loadedItems)
        {
            return loadedItems.Where(r => r.GetOwner() == listview_selectedItem).ToList();
        }
        //Attributes
        private void OnAttribute_SearchItemRequest(PointerDownEvent evt)
        {
            List<Data_Attribute> itemsToIgnore = new List<Data_Attribute>();
            //itemsToIgnore.Add(listview_selectedItem);
            itemsToIgnore.AddRange(_listview_attributes.Items.Select(r => r.GetAttribute()).ToList());
            DataEditorUtility.ShowSearchWindow<Data_Attribute>("Attributes", GUIUtility.GUIToScreenPoint(evt.position), OnAttribute_AddItemRequest, itemsToIgnore);
        }
        private void OnAttribute_AddItemRequest(GMBEditorSearchProvider.SearchResult obj)
        {

            if (obj.resultFriendlyName == EditorStringsProvider._LISTVIEW_NEW_OPTIONS_)
            {
                GetGMBWindow().OnMenuSelected(typeof(DataAttribute_Window));
                return;
            }

           

            Data_Attribute attribute = obj.GetDataFile<Data_Attribute>();
            if (attribute == null)
                return;

            Data_ItemAttribute itemAttr = ScriptableObject.CreateInstance<Data_ItemAttribute>();
            itemAttr.SetAttribute(attribute);
            itemAttr.SetOwner(listview_selectedItem);
            _listview_attributes.OnAddNewIntemFromInstance(itemAttr);
        }
        private void OnAttribute_SelectionChanged(List<Data_ItemAttribute> items)
        {

        }
        private void OnAttribute_BindItem(VisualElement element, Data_ItemAttribute item)
        {
            SerializedObject serializedObject = item.GetSerializedObject();
            element.Unbind();
            element.Q<Label>("title").text = item.GetFriendlyName();
            element.Q<Slider>().value = item.GeetRequiredAmountValue();
            element.Q<FloatField>().value = item.GetRequiredTimeLapse();

            element.Bind(serializedObject);
        }
        private VisualTreeAsset OnAttribute_MakeItem()
        {
            return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(GetTemplate_ListViewAttributeItemFilePath());
        }
        private List<Data_ItemAttribute> OnAttribute_LoadDatas(List<Data_ItemAttribute> loadedItems)
        {
            return loadedItems.Where(r => r.GetOwner() == listview_selectedItem).ToList();
        }

        #endregion

        #region PROTECTED GETTERS


        protected override string GetTemplate_FilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_ + "Win_Items/Data_Item.uxml";
        }
        protected override string GetTemplate_ListViewItemFilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_ + "Defaults/Data_Listview_Item_Advanced.uxml";
        }
        protected string GetTemplate_ListViewIngredientItemFilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_ + "Win_Items/Data_Item_ListviewItem_Ingredient.uxml";
        }
        protected string GetTemplate_ListViewCrafterItemFilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_ + "Win_Items/Data_Item_ListviewItem_Crafter.uxml";
        }
        protected string GetTemplate_ListViewAttributeItemFilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_ + "Win_Items/Data_Item_ListviewItem_Attribute.uxml";
        }

        public override GMBWindowMenuItem GetGMBWindowMenuItem()
        {
            return new GMBWindowMenuItem(this, "menu_item_items", "Items List", "Items");
        }

        #endregion
    }
}