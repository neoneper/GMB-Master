using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace GMBEditor
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WindowTemplatateAttribute : Attribute
    {

        public WindowTemplatateAttribute()
        {
            Debug.Log("Creeeeeem");
        }
    }
}
