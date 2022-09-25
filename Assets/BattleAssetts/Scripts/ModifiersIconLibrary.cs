using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ModifiersIconLibrary
{
    Dictionary<string, Sprite> modifierIcons;

    public ModifiersIconLibrary()
    {
        modifierIcons = new Dictionary<string, Sprite>();
    }

    public void GetIcon(string iconName, System.Action<Sprite> callBack)
    {
        if (modifierIcons.ContainsKey(iconName))
            callBack.Invoke(modifierIcons[iconName]);
        else
        {
            Addressables.LoadAssetAsync<Sprite>("ModifiersIcons/" + iconName).Completed += h => {
                if(modifierIcons.ContainsKey(iconName))
                {
                    callBack.Invoke(modifierIcons[iconName]);
                }
                else
                {
                    modifierIcons.Add(iconName, h.Result);
                    callBack.Invoke(h.Result);
                }
            };
        }
    }
    public Dictionary<string, Sprite> ModifierIcons { get => modifierIcons; set => modifierIcons = value; }
}
