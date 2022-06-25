using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System.Linq;
using UnityEditor;

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor.Experimental.GraphView;

#endif


namespace GMB
{

    /// <summary>
    /// Garante que as implementacoes tenham a extrutura basica de dados obrigatorios de GMB
    /// - ID Automatico, Data de criacao e Modificacao. 
    /// Utilize para implementar todos os tipos de dados GMB. 
    /// </summary>
    [ResourcesPath("")]
    public abstract class Data : ScriptableObject
    {
        [SerializeField, ReadOnly] private string _id = Guid.NewGuid().ToString().ToUpper();
        [SerializeField, ReadOnly] private string _createdDate = DateTime.Now.ToString(StringsProvider._DATE_TIME_FORMAT_);
        [SerializeField] private string _friendlyName = StringsProvider._UNDEFINED_;
        [SerializeField] private string _description = "";
        [SerializeField] private Sprite _icon = null;


        public string GetID()
        {
            return _id;
        }
        public string GetCreatedDate()
        {
            return _createdDate;
        }

        public virtual string GetFriendlyName()
        {
            return _friendlyName;
        }

        public virtual string GetDescription()
        {
            return _description;
        }
        public virtual Sprite GetIcon()
        {

            return _icon;
        }

        /// <summary>
        /// Retorna o nome do arquivo como um caminho relativo para ser usado por provedoers de pesquisa.
        /// Se este dado contem categoria, ele devera retornar a categoria + o nome. Ex:
        /// categoriaName/dataName
        /// </summary>
        /// <returns></returns>
        public virtual string GetNameAsRelativePath()
        {
            return _friendlyName;
        }


#if UNITY_EDITOR

        public UnityEditor.SerializedObject GetSerializedObject()
        {
            return new UnityEditor.SerializedObject(this);
        }
        // <summary>
        ///   <para>Returns the path name relative to the project folder where the asset is stored.</para>
        /// </summary>
        /// <returns>
        ///   <para>The asset path name, or null, or an empty string if the asset does not exist.</para>
        /// </returns>
        public string GetAssetPath()
        {
            return AssetDatabase.GetAssetPath(this);
        }

        public void SaveAsset()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }

#endif
    }

}