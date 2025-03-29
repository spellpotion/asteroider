using UnityEditor;

namespace Asteroider.Manager.Editor
{
    [CustomEditor(typeof(Screen設定))]
    public class GameState設定Editor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty screenByScreenType
                = serializedObject.FindProperty("screenByScreenType");

            EditorGUILayout.PropertyField(screenByScreenType, true);

            SerializedProperty contrast色1 = serializedObject.FindProperty("Contrast色1");

            EditorGUILayout.PropertyField(contrast色1);

            SerializedProperty contrast色2 = serializedObject.FindProperty("Contrast色2");

            EditorGUILayout.PropertyField(contrast色2);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

