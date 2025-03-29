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

            SerializedProperty contrast�F1 = serializedObject.FindProperty("Contrast�F1");

            EditorGUILayout.PropertyField(contrast�F1);

            SerializedProperty contrast�F2 = serializedObject.FindProperty("Contrast�F2");

            EditorGUILayout.PropertyField(contrast�F2);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

