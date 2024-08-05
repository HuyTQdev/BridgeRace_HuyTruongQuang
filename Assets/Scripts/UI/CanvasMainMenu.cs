using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace UI
{
    class CanvasMainMenu: UICanvas
    {
        public void OpenShop()
        {
            UIManager.Instance.OpenUI<CanvasColorList>();
        }
        public void OpenSettings()
        {
            UIManager.Instance.OpenUI<CanvasSettings>();
        }

        public void PlayGame()
        {
            Addressables.LoadSceneAsync("Level1");
        }
    }
}
