using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor.AssetHelper
{
    [Serializable]
    public class ShaderReferences
    {
        public const string SavePath = "Assets/EditorData/Texture Fixer/ShaderReferences.json";

        public string ShaderName;
        
        public ShaderMapsData ShaderMaps;
        
        public ShaderTexturesData ShaderTextures;

        [Serializable]
        public class ShaderMapsData
        {
            public string[] Names;
        }

        [Serializable]
        public class ShaderTexturesData
        {
            public string[] Names;
        }

        public static ShaderReferences Current;

        public static bool SaveToFile()
        {
            string json = JsonUtility.ToJson(Current, true);
            
            File.WriteAllText(SavePath, json);

            return true;
        }

        public static bool LoadFromFile()
        {
            if (!File.Exists(SavePath))
            {
                return false;
            }
            
            string json = File.ReadAllText(SavePath);
            
            Current = JsonUtility.FromJson<ShaderReferences>(json);
            
            return true;
        }
    }
    
    public class ImportAssetEditor : AssetPostprocessor
    {
        private const string TEXTURE_DIRECTORY = "Assets/Models/Battle/Bar/Textures/";

        private const string MODEL_DIRECTORY = "Assets/Models/Battle/";

        private static bool _isChecking;

        [MenuItem("Tools/Texture Fixer/Toggle Missing Texture Check")]
        private static void ToggleChecking()
        {
            _isChecking = !_isChecking;

            if (_isChecking)
            {
                ShaderReferences.LoadFromFile();
            }
        }
        
        [MenuItem("Tools/Texture Fixer/Toggle Missing Texture Check", true)]
        private static bool ToggleCheckingValidate()
        {
            Menu.SetChecked("Tools/Texture Fixer/Toggle Missing Texture Check", _isChecking);
            
            return true;
        }

        private void OnPostprocessModel(GameObject model)
        {
            if (!_isChecking)
            {
                return;
            }

            if (string.IsNullOrEmpty(assetPath) 
                || !assetPath.EndsWith(".fbx") || !assetPath.Contains(MODEL_DIRECTORY))
            {
                return;
            }

            foreach (var renderer in model.GetComponentsInChildren<Renderer>())
            {
                var materials = renderer.sharedMaterials;

                foreach (var material in materials)
                {
                    material.shader = Shader.Find(ShaderReferences.Current.ShaderName);

                    if (!AssignTexturesToMaterial(material, TEXTURE_DIRECTORY))
                    {
                        break;
                    }
                }
            }

            _isChecking = false;
        }
        
        private static bool AssignTexturesToMaterial(Material material, string textureDirectory)
        {
            string[] files = Directory.GetFiles(textureDirectory);
            
            bool texturesExist = files.Any(file => Path.GetFileName(file).StartsWith(material.name, StringComparison.OrdinalIgnoreCase));

            if (!texturesExist)
            {
                return false;
            }

            for (var i = 0; i < ShaderReferences.Current.ShaderMaps.Names.Length; i++)
            {
                string pathToTexture = Path.Combine(textureDirectory,
                    material.name + "_" + ShaderReferences.Current.ShaderTextures.Names[i]);

                var loadedTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(pathToTexture);
                
                if (!loadedTexture)
                {
                    continue;
                }
                
                material.SetTexture(Shader.PropertyToID(ShaderReferences.Current.ShaderMaps.Names[i]), loadedTexture);
            }
            
            return true;
        }
    }
}
