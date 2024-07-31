using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class CanvasMainMenu: UICanvas
    {
        public void OpenShop()
        {
            UIManager.Instance.OpenUI<CanvasColorList>();
        }
        public void OpenSetting()
        {

        }
    }
}
