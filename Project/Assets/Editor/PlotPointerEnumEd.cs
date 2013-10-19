using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlotBehaviour))]
[CanEditMultipleObjects]
class PlotPointerEnumEd : Editor
{
    SerializedProperty _propMessage;

    PlotPointer _content;

    void OnEnable()
    {
        _propMessage = serializedObject.FindProperty("PlotLinks");
        if (!_propMessage.isArray)
        {
            // You shouldn't expect to see this.
            Debug.LogError("Property is not an array!");
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.DrawDefaultInspector();

        bool enlarge = false;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField(string.Format("Message count = {0}", _propMessage.arraySize));
        _content = (PlotPointer)EditorGUILayout.EnumPopup("Plot Points", _content);
       /* if (!string.IsNullOrEmpty(_content) && GUILayout.Button("Add"))
        {
            enlarge = true;
        }*/

        EditorGUILayout.EndVertical();

        if (enlarge)
        {
            EnlargeArray();
            serializedObject.ApplyModifiedProperties();
            // I'm not in love with setting the array value this way.
            // Can't see an appropriate property method though.
            PlotBehaviour t = target as PlotBehaviour;
            t.PlotLinks[t.PlotLinks.Length - 1] = _content;
            _content =PlotPointer.Begin;
        }
    }

    void EnlargeArray()
    {
        int enlarged = _propMessage.arraySize;
        _propMessage.InsertArrayElementAtIndex(enlarged);
    }
}