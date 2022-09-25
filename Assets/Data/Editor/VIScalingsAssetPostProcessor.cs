using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class VIScalingsAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/BattleAssetts/FightersLogic/Scalings/VI/scaling.xlsx";
    private static readonly string assetFilePath = "Assets/BattleAssetts/FightersLogic/Scalings/VI/VIScalings.asset";
    private static readonly string sheetName = "VIScalings";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            VIScalings data = (VIScalings)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(VIScalings));
            if (data == null) {
                data = ScriptableObject.CreateInstance<VIScalings> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<VIScalingsData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<VIScalingsData>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
