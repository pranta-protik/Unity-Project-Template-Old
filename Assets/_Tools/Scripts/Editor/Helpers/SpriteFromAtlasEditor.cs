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
        private SerializedProperty _imageProperty;
        private SerializedProperty _spriteRendererProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            _spriteFromAtlas = target as SpriteFromAtlas;
            _spriteAtlasProperty = serializedObject.FindProperty("_spriteAtlas");
            _spriteNameProperty = serializedObject.FindProperty("_spriteName");
            _imageProperty = serializedObject.FindProperty("_image");
            _spriteRendererProperty = serializedObject.FindProperty("_spriteRenderer");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _spriteFromAtlas.TryAssignSprite();
            
            EditorGUILayout.PropertyField(_spriteNameProperty);
            EditorGUILayout.PropertyField(_spriteAtlasProperty);

            if (!_spriteFromAtlas.Image && !_spriteFromAtlas.SpriteRenderer)
            {
                EditorGUILayout.PropertyField(_imageProperty);
                EditorGUILayout.PropertyField(_spriteRendererProperty);
            }

            if (_spriteFromAtlas.Image) EditorGUILayout.PropertyField(_imageProperty);
            if(_spriteFromAtlas.SpriteRenderer) EditorGUILayout.PropertyField(_spriteRendererProperty);

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}