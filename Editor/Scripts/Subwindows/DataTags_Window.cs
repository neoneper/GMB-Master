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

namespace GMBEditor
{
    public class DataTags_Window : GMBEditorWindow<Data_Tag>
    {
        protected override void OnCloseGUI()
        {

        }

        protected override void OnCreateGUI()
        {

        }

        protected override string GetTemplate_FilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_DEFAULTS.Combine("Data.uxml");
        }

        protected override string GetTemplate_ListViewItemFilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_DEFAULTS.Combine("Data_Listview_Item.uxml");
        }

        public override GMBWindowMenuItem GetGMBWindowMenuItem()
        {
            return new GMBWindowMenuItem(this, "menu_tag_items", "Tag List", "Tag");
        }
    }
}