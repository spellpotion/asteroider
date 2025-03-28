using UnityEditor;

namespace Asteroider.Manager.Editor
{
    [CustomEditor(typeof(Screen�ݒ�))]
    public class GameState�ݒ�Editor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty screenByScreenType
                = serializedObject.FindProperty("screenByScreenType");

            EditorGUILayout.PropertyField(screenByScreenType, true);

            SerializedProperty contrast�F = serializedObject.FindProperty("Contrast�F");

            EditorGUILayout.PropertyField(contrast�F);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

