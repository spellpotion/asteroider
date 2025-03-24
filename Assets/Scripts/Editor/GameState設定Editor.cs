using UnityEditor;

namespace Asteroider.Manager.Editor
{
    [CustomEditor(typeof(Screenê›íË))]
    public class GameStateê›íËEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty layoutByGameState
                = serializedObject.FindProperty("layoutByLayoutType");

            EditorGUILayout.PropertyField(layoutByGameState, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

