using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.U2D.Animation;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HeroesInventory : MonoBehaviour
{

    private SpriteLibrary portraits;

    public AsyncOperationHandle<IList<Sprite>> opHandle;

    private IEnumerator Start()
    {
        portraits = gameObject.AddComponent<SpriteLibrary>();
        portraits.spriteLibraryAsset = ScriptableObject.CreateInstance<SpriteLibraryAsset>();


        opHandle = Addressables.LoadAssetsAsync<Sprite>("Portrait", null);
        yield return opHandle;


        foreach (Sprite s in opHandle.Result)
        {
            portraits.spriteLibraryAsset.AddCategoryLabel(s, "Portraits", s.name);
        }
        gameObject.transform.GetComponent<HeroSelection>().LoadPortraits();
    }

    public Sprite GetPortrait(string name)
    {
        return portraits.spriteLibraryAsset.GetSprite("Portraits", name);
    }
}
