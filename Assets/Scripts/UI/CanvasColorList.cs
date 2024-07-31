using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace UI
{
    class CanvasColorList : UICanvas
    {
        [SerializeField] DataColorList colorList;
        [SerializeField] Transform content;
        [SerializeField] GameObject colorViewPrefab;
        List<ColorView> colorViews;
        int chosenColor;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            ShowColors();
            chosenColor = -1;
        }

        private void ShowColors()
        {
            colorViews = new List<ColorView>();
            for(int i = 0; i < colorList.dataColors.Count; i++)
            {
                ColorView c = Instantiate(colorViewPrefab, content).GetComponent<ColorView>();
                c.SetUp(colorList.dataColors[i], i);
                c.UpdateChosen(false);
                colorViews.Add(c);
            }
            colorViews[0].UpdateChosen(true);
        }

        public void ChooseColor(int id)
        {
            if (chosenColor == id) return;
            if(chosenColor != -1)colorViews[chosenColor].UpdateChosen(false);
            colorViews[id].UpdateChosen(true);
            chosenColor = id;
        }
    }
}
