using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace GMBEditor
{
    public static class GMBEditorExtensions
    {
        public static Sprite GetEditorProperty_Icon(this GMB.Data data)
        {
            if (data.GetIcon() == null)
            {
                data.GetSerializedObject().FindProperty("_icon").objectReferenceValue = GMBEditorStyles.SpriteUnKnow;
                EditorUtility.SetDirty(data);
                AssetDatabase.SaveAssetIfDirty(data);
            }

            return data.GetIcon();
        }

        //Attributes
        public static T GetPropertyAttribute<T>(this SerializedProperty prop, bool inherit) where T : PropertyAttribute
        {
            if (prop == null) { Debug.Log(0); return null; }

            Type t = prop.serializedObject.targetObject.GetType();
            FieldInfo f = null;
            PropertyInfo p = null;

            //return null;
            var path = prop.propertyPath;
            var elements = path.Split('.');


            string[] slices = prop.propertyPath.Split('.');
            System.Type type = prop.serializedObject.targetObject.GetType();


            //Field
            for (int i = 0; i < slices.Length; i++)
            {
                if (slices[i] == "Array")
                {
                    i++; //skips "data[x]"
                    type = type.GetElementType(); //gets info on array elements
                }
                else
                {

                    f = type.GetField(slices[i], (BindingFlags)(-1));
                    p = type.GetProperty(slices[i], (BindingFlags)(-1));
                    type = f.FieldType;

                }
            }


            T[] attributes;
            if (f != null)
            {
                attributes = f.GetCustomAttributes(typeof(T), inherit) as T[];

            }
            else if (p != null)
            {
                attributes = p.GetCustomAttributes(typeof(T), inherit) as T[];
            }
            else
            {
                return null;
            }

            return attributes.Length > 0 ? attributes[0] : null;
        }

        //VisualElements
        public static void SetVisible(this VisualElement element, bool visible)
        {

            element.style.visibility = visible == true ? Visibility.Visible : Visibility.Hidden;
        }
        public static void SetDisplay(this VisualElement element, bool display)
        {

            element.style.display = display == true ? DisplayStyle.Flex : DisplayStyle.None;
        }


        //Strings
        public static string Combine(this string a, string b)
        {
            return a + b;
        }

        public static string RemoveAfter(this string input, string character)
        {

            int index = input.IndexOf(character);
            if (index >= 0)
                input = input.Substring(0, index);

            return input;
        }
        public static string RemoveBefore(this string input, string character)
        {

            int index = input.IndexOf(character);
            if (index >= 0)
                input = input.Substring(index+1);

            return input;
        }
    }

}
