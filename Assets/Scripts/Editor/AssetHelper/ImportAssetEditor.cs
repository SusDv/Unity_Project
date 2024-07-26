using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor.AssetHelper
{
    public static class ShaderMapReferences
    {
        public const string ShaderName = "Autodesk Interactive";
        
        public const string MetallicMap = "_MetallicGlossMap";
            
        public const string RoughnessMap = "_SpecGlossMap";
            
        public const string HeightMap = "_ParallaxMap";
    }
    
    public class ImportAssetEditor : AssetPostprocessor
    {
        private const string TEXTURE_DIRECTORY = "Assets/Models/Battle/Bar/Textures/";

        private const string MODEL_DIRECTORY = "Assets/Models/Battle/";
        
        private void OnPostprocessModel(GameObject model)
        {
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
                    material.shader = Shader.Find(ShaderMapReferences.ShaderName);
                    
                    if (!AssignTexturesToMaterial(material, TEXTURE_DIRECTORY))
                    {
                        break;
                    }
                }
            }
        }
        
        private bool AssignTexturesToMaterial(Material material, string textureDirectory)
        {
            string[] files = Directory.GetFiles(textureDirectory);
            
            bool texturesExist = files.Any(file => Path.GetFileName(file).StartsWith(material.name, StringComparison.OrdinalIgnoreCase));

            if (!texturesExist)
            {
                return false;
            }
            
            string metallicPath = Path.Combine(textureDirectory, material.name + "_" + "Metalness.png");
            
            string roughnessPath = Path.Combine(textureDirectory, material.name + "_" + "Roughness.png");

            string heightPath = Path.Combine(textureDirectory, material.name + "_" + "Height");
            
            
            var metallicTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(metallicPath);
            
            var roughnessTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(roughnessPath);

            var heightTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(heightPath);

            if(metallicTexture)
                material.SetTexture(Shader.PropertyToID(ShaderMapReferences.MetallicMap), metallicTexture);
            
            if(roughnessTexture)
                material.SetTexture(Shader.PropertyToID(ShaderMapReferences.RoughnessMap), roughnessTexture);
            
            if(heightTexture)
                material.SetTexture(Shader.PropertyToID(ShaderMapReferences.HeightMap), heightTexture);

            return true;
        }
    }
}
