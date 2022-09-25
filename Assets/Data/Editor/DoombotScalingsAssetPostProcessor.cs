using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class DoombotScalingsAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/BattleAssetts/FightersLogic/Scalings/Doombot/scaling.xlsx";
    private static readonly string assetFilePath = "Assets/BattleAssetts/FightersLogic/Scalings/Doombot/DoombotScalings.asset";
    private static readonly string sheetName = "DoombotScalings";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            DoombotScalings data = (DoombotScalings)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(DoombotScalings));
            if (data == null) {
                data = ScriptableObject.CreateInstance<DoombotScalings> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<DoombotScalingsData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<DoombotScalingsData>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
