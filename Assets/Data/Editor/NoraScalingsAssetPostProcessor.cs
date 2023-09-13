using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class NoraScalingsAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/BattleAssetts/FightersLogic/Scalings/GorathScalings/GorathScalings.xlsx";
    private static readonly string assetFilePath = "Assets/BattleAssetts/FightersLogic/Scalings/GorathScalings/NoraScalings.asset";
    private static readonly string sheetName = "NoraScalings";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            NoraScalings data = (NoraScalings)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(NoraScalings));
            if (data == null) {
                data = ScriptableObject.CreateInstance<NoraScalings> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<NoraScalingsData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<NoraScalingsData>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
