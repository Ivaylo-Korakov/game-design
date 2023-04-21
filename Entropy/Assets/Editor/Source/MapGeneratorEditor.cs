using Assets.Source.World_Generation;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor.Source
{
    /// <summary>
    /// This class is a only for the Unity editor
    /// and this one specifically is for the MapGenerator Editor
    /// in the inspector window
    /// You can add new functionality to the ediotor for a specific
    /// component
    /// </summary>
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            MapGenerator mapGen = (MapGenerator)target;

            if (DrawDefaultInspector())
            {
                if (mapGen.DoesAutoGenerate)
                {
                    mapGen.DrawMapInEditor();
                }
            }

            if (GUILayout.Button("Generate"))
            {
                mapGen.DrawMapInEditor();
            }
        }
    }
}
