using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
[CustomEditor(typeof(EnumDataSO))]
public class EnumGeneratorEditor : Editor {

    private string _enumCode;
    private string _enumFilePath;

    private void OnEnable() {
        EnumDataSO enumDataSO = checkTarget(target);
        if (enumDataSO == null) return;

        _enumCode = GenerateEnumCode(enumDataSO);
        _enumFilePath = GetEnumFilePath(enumDataSO);
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Enum")) {
            GenerateEnumFile();
        }
    }

    public static void GenerateEnumMenu() {
        string[] assetGuids = Selection.assetGUIDs;

        if (assetGuids.Length == 0) return;

        string assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[0]);
        EnumDataSO enumDataSOBase = AssetDatabase.LoadAssetAtPath<EnumDataSO>(assetPath);

        EnumDataSO enumDataSO = checkTarget(enumDataSOBase);
        if (enumDataSO == null) return;
        
        EnumGeneratorEditor editor = CreateInstance<EnumGeneratorEditor>();
        editor._enumCode = GenerateEnumCode(enumDataSO);
        editor._enumFilePath = editor.GetEnumFilePath(enumDataSO);
        editor.GenerateEnumFile();
    }


    private static string GenerateEnumHeader(string enumName) {
        return "public enum " + enumName + " {\n";
    }

    private static string GenerateEnumValues(List<string> values) {
        if (values.Count == 0) {
            return "";
        }
        return "\t" + string.Join(",\n\t", values) + "\n";
    }

    private static string GenerateEnumCode(EnumDataSO enumDataSO) {
        string enumHeader = GenerateEnumHeader(enumDataSO.enumName);
        string enumValues = GenerateEnumValues(enumDataSO.enumValues);
        return enumHeader + enumValues + "}";
    }



    private string GetEnumFilePath(EnumDataSO enumDataSO) { 
        string assetPath = AssetDatabase.GetAssetPath(target);
        string folderPath = Path.GetDirectoryName(assetPath);
        return Path.Combine(folderPath, enumDataSO.enumName+".cs");
    }

    private void GenerateEnumFile() {
        File.WriteAllText(_enumFilePath, _enumCode);
        AssetDatabase.ImportAsset(_enumFilePath);
    }

    private static EnumDataSO checkTarget(Object target) {
        if (target == null) return null;
        if (target is not EnumDataSO) return null;
        EnumDataSO enumDataSO = (EnumDataSO)target;
        if (enumDataSO == null) return null;
        if (enumDataSO.enumName == null) return null;
        enumDataSO.enumValues ??= new List<string>();
        return enumDataSO;
    }
}
