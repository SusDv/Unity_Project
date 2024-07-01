using CharacterModule.Utility;
using CharacterModule.Utility.Stats;
using UnityEditor;
using UnityEngine;

namespace Editor.Stats
{
    [CustomPropertyDrawer(typeof(ModifierData))]
    public class ModifierDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            var modifierTypeProp = property.FindPropertyRelative("<ModifierType>k__BackingField");
            var modifiedParamProp = property.FindPropertyRelative("<ModifiedParam>k__BackingField");
            var derivedFromProp = property.FindPropertyRelative("<DerivedFrom>k__BackingField");
            var valueProp = property.FindPropertyRelative("<Value>k__BackingField");
            
            var modifierTypeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var modifiedParamRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight);
            var valueRect = new Rect(position.x, position.y + 2 * (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.PropertyField(modifierTypeRect, modifierTypeProp);
            EditorGUI.PropertyField(modifiedParamRect, modifiedParamProp);
            
            if ((ModifierType)modifierTypeProp.enumValueIndex == ModifierType.PERCENTAGE)
            {
                valueRect = new Rect(position.x, position.y + 3 * (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);

                var calculateFromRect = new Rect(position.x, position.y + 2 * (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);
                
                EditorGUI.PropertyField(calculateFromRect, derivedFromProp);
            }
            
            EditorGUI.PropertyField(valueRect, valueProp);
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var modifierTypeProp = property.FindPropertyRelative("<ModifierType>k__BackingField");
        
            bool shouldDrawCalculateFrom = modifierTypeProp.enumValueIndex == (int)ModifierType.PERCENTAGE;
        
            int numberOfFields = shouldDrawCalculateFrom ? 4 : 3;
        
            return numberOfFields * (EditorGUIUtility.singleLineHeight + 2);
        }
    }
}