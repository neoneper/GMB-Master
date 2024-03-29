﻿using System.Collections;
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
    //This is the windows implementation of the your Data Type.
    public class DataAttribute_Window : GMBEditorWindow<Data_Attribute> 
    {

        //Trigged when this window is oppen
        protected override void OnCreateGUI()
        {

        }

        //Trigged when this window has closed
        protected override void OnCloseGUI()
        {

        }

        //Return the VisualTree of this template
        protected override string GetTemplate_FilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_DEFAULTS.Combine("Data.uxml");
        }

        //Return the VisualTree of the Item Template, used by ListView content to show each Data (ScriptableObject) created to this implementation of the Data Type
        protected override string GetTemplate_ListViewItemFilePath()
        {
            return EditorStringsProvider._PATH_GMB_EDITOR_TEMPLATES_DEFAULTS.Combine("Data_Listview_Item.uxml");
        }

        //The menu name referencied at GMBWindow template. This name need to equal the button menu name of the GMBWindow template.
        //Its will be used to Binding automatically this Window implementation with some menu button of the Root window
        public override GMBWindowMenuItem GetGMBWindowMenuItem()
        {
            return new GMBWindowMenuItem(this, "menu_attribute_items", "Attributes List", "Attributes");
        }

    }
}