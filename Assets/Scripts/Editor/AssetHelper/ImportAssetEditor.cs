using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor.AssetHelper
{
    public static class ShaderMapReferences
    {
        public const string MetallicMap = "_MetallicGlossMap";
            
        public const string RoughnessMap = "_SpecGlossMap";
            
        public const string HeightMap = "_ParallaxMap";
    }
    
    public class ImportAssetEditor : AssetPostprocessor
    {
        private const string TEXTURE_DIRECTORY = "Assets/Models/Battle/Bar/Textures/";
        
        private void OnPostprocessModel(GameObject model)
        {
            if (string.IsNullOrEmpty(assetPath) || !assetPath.EndsWith(".fbx"))
            {
                return;
            }
            
            string objectName = model.GetComponent<Mesh>().name;
            
            foreach (var renderer in model.GetComponentsInChildren<Renderer>())
            {
                var materials = renderer.sharedMaterials;
                
                foreach (var material in materials)
                {
                    if (!AssignTexturesToMaterial(material, objectName, TEXTURE_DIRECTORY))
                    {
                        break;
                    }
                }
            }
        }
        
        private bool AssignTexturesToMaterial(Material material, string objectName, string textureDirectory)
        {
            string[] files = Directory.GetFiles(textureDirectory);
            
            bool texturesExist = files.Any(file => Path.GetFileName(file).StartsWith(material.name, StringComparison.OrdinalIgnoreCase));

            if (!texturesExist)
            {
                return false;
            }
            
            string metallicPath = Path.Combine(textureDirectory, objectName + "_" + "Metalness.png");
            
            string roughnessPath = Path.Combine(textureDirectory, objectName + "_" + "Roughness.png");

            string heightPath = Path.Combine(textureDirectory, objectName + "_" + "Height");
            
            
            var metallicTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(metallicPath);
            
            var roughnessTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(roughnessPath);

            var heightTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(heightPath);
            
            
            material.SetTexture(Shader.PropertyToID(ShaderMapReferences.MetallicMap), metallicTexture);
            
            material.SetTexture(Shader.PropertyToID(ShaderMapReferences.RoughnessMap), roughnessTexture);
            
            material.SetTexture(Shader.PropertyToID(ShaderMapReferences.HeightMap), heightTexture);

            return true;
        }
    }
}
