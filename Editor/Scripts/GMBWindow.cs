using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;
using System.Linq;
using GMB;

namespace GMBEditor
{
    public class GMBWindow : EditorWindow
    {
        DataItem_Window items_Window = new DataItem_Window();
        VisualElement _root;
        VisualElement _menu_container;
        VisualElement _menu_content;
        VisualElement _content_container;
        VisualElement _content;

        //Getters
        public VisualElement menuContent { get { return _menu_content; } }
        public VisualElement menuContainer { get { return _menu_container; } }
        public VisualElement contentContainer { get { return _content_container; } }
        public VisualElement content { get { return _content; } }
        public VisualElement root { get { return _root; } }

        [MenuItem("Tools/GMBEditor")]
        public static void ShowWindow()
        {
            GMBWindow wnd = GetWindow<GMBWindow>(true);
            wnd.titleContent = new GUIContent("GMB");
        }

        public void CreateGUI()
        {
            _root = rootVisualElement;
            GetGMBWindowTemplate().CloneTree(root);


            _menu_container = root.Q("root_menu_container");
            _menu_content = menuContainer.Q("content");
            _content_container = _root.Q("root_content_container");
            _content = contentContainer.Q("content");

            //Buttons events
            menuContent.Q<VisualElement>("menu_item_items").Q<Button>().clicked += OnMenu_Items;

        }

        private void OnMenu_Items()
        {
            content.Clear();
            items_Window.CreateGUI(this);
        }

        private void OnDisable()
        {
            menuContent.Q<VisualElement>("menu_item_items").Q<Button>().clicked -= OnMenu_Items;
        }

        private VisualTreeAsset GetGMBWindowTemplate()
        {
            return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_ + "Win_Root/Content.uxml");

        }
    }
}