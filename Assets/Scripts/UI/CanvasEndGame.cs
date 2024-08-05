using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace UI
{
    public class CanvasEndGame : UICanvas
    {
        [SerializeField] RawImage title;

        public void SetTitle(bool isWin)
        {
            string s = isWin ? "win" : "lose";
            Addressables.LoadAssetAsync<Sprite>(s).Completed += (op) =>
            {
                title.texture = op.Result.texture;
            };
        }
        public void Replay()
        {
            Addressables.LoadSceneAsync("Level1");

        }

        public void OpenMenu()
        {
            Addressables.LoadSceneAsync("MainMenu");
        }
    }
}
