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


        VisualElement _root; //Todos os demais conteudos estao dentro deste container
        VisualElement _menu_container; //Os menus de janelas e outros conteudos relacionados estao dentro deste container
        VisualElement _menu_content; //A lista de botoes esta dentro deste container
        VisualElement _content_container; //O Conteudo apresentado indivudualmente para cada menu selecionado bem como relacionados esta dentro deste container 
        VisualElement _content; //O Conteudo apresentado indivudualmente para cada menu selecionado esta dentro deste container  

        //Getters
        public VisualElement menuContent { get { return _menu_content; } }
        public VisualElement menuContainer { get { return _menu_container; } }
        public VisualElement contentContainer { get { return _content_container; } }
        public VisualElement content { get { return _content; } }
        public VisualElement root { get { return _root; } }


        Dictionary<Type, Button> menuButtonsTypes = new Dictionary<Type, Button>();
        Dictionary<Type, IGMBEditorWindow> menuWindowsTypes = new Dictionary<Type, IGMBEditorWindow>();



        IGMBEditorWindow currentSelectedWindowMenu;

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



            BindMenus();

        }
      
        private void OnDisable()
        {
            if (currentSelectedWindowMenu != null)
            {
                currentSelectedWindowMenu.CloseGUI();
            }

            foreach (Button bt in menuButtonsTypes.Values)
            {
                bt.clickable.clickedWithEventInfo -= OnMenuSelected;
            }

            menuButtonsTypes.Clear();
            menuButtonsTypes.Clear();


            content.Clear();
        }

        private void OnMenuSelected(EventBase obj)
        {
            if (currentSelectedWindowMenu != null)
            {
                currentSelectedWindowMenu.CloseGUI();
            }

            content.Clear();

            VisualElement target = obj.target as VisualElement;
            currentSelectedWindowMenu = (IGMBEditorWindow)target.userData;
            currentSelectedWindowMenu.CreateGUI(this);
        }

        /// <summary>
        /// Procura e cria dinamicamente instancias para todos as implementacoes de <see cref="GMBEditorWindow{T}"/>, adicionando automaticamente
        /// a caada referencia de botao de menu.
        /// <para>Tenha em mente que o botao sera assosiado a instancia da janela atraves de <see cref="IGMBEditorWindow.GetGMBWindowMenuReferenceName"/></para>,
        /// assim sendo retorne extamente o nome referenciado em cada <see cref="Button"/> em suas implementacoes de janela
        /// </summary>
        private void BindMenus()
        {
            var type = typeof(IGMBEditorWindow);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);

            foreach (Type i in types)
            {

                IGMBEditorWindow igmb = (IGMBEditorWindow)Activator.CreateInstance(i);
                Button but = _menu_content.Q<VisualElement>(igmb.GetGMBWindowMenuReferenceName()).Q<Button>();

                menuButtonsTypes.Add(i, but);
                menuWindowsTypes.Add(i, igmb);
                but.userData = igmb;
                but.clickable.clickedWithEventInfo += OnMenuSelected;


            }
        }



        private VisualTreeAsset GetGMBWindowTemplate()
        {
            return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_ + "Win_Root/Content.uxml");

        }

    }
}