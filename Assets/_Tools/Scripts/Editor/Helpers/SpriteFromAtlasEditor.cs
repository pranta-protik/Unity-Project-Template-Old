using UnityEditor;

namespace _Tools.Helpers
{
    [CustomEditor(typeof(SpriteFromAtlas))]
    public class SpriteFromAtlasEditor : Editor
    {
        #region Variables

        private SpriteFromAtlas _spriteFromAtlas;
        private SerializedProperty _spriteAtlasProperty;
        private SerializedProperty _spriteNameProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _spriteFromAtlas = target as SpriteFromAtlas;
            _spriteAtlasProperty = serializedObject.FindProperty("_spriteAtlas");
            _spriteNameProperty = serializedObject.FindProperty("_spriteName");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _spriteFromAtlas.TryAssignSprite();
            
            EditorGUILayout.PropertyField(_spriteNameProperty);
            EditorGUILayout.PropertyField(_spriteAtlasProperty);
            
            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}