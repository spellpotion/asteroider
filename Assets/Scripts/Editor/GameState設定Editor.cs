using UnityEditor;

namespace Asteroider.Manager.Editor
{
    [CustomEditor(typeof(Layout�ݒ�))]
    public class GameState�ݒ�Editor : UnityEditor.Editor
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

