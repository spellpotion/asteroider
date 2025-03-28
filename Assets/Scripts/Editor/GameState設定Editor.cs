using UnityEditor;

namespace Asteroider.Manager.Editor
{
    [CustomEditor(typeof(Screenê›íË))]
    public class GameStateê›íËEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty screenByScreenType
                = serializedObject.FindProperty("screenByScreenType");

            EditorGUILayout.PropertyField(screenByScreenType, true);

            SerializedProperty contrastêF = serializedObject.FindProperty("ContrastêF");

            EditorGUILayout.PropertyField(contrastêF);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

