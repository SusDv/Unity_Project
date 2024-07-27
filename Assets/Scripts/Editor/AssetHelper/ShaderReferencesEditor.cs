using UnityEditor;
using UnityEngine;

namespace Editor.AssetHelper
{
    public class ShaderReferencesEditor : EditorWindow
    {
        [MenuItem("Tools/Texture Fixer/Shader References/Save")]
        private static void Save()
        {
            if (ShaderReferences.SaveToFile())
            {
                Debug.Log("Shader References saved"); 
            }
        }

        [MenuItem("Tools/Texture Fixer/Shader References/Load")]
        private static void Load()
        {
            if (ShaderReferences.LoadFromFile())
            {
                Debug.Log("Shader References loaded"); 
            }
        }
    }
}
