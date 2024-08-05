using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
            Addressables.LoadAssetAsync<DataColorList>("DataColorList").Completed += (op) =>
            {
                colorList = op.Result;
                ShowColors();

            };

        }

        private void ShowColors()
        {
            chosenColor = PlayerPrefs.HasKey("Color") ? PlayerPrefs.GetInt("Color") : 0;
            colorViews = new List<ColorView>();
            for(int i = 0; i < colorList.dataColors.Count; i++)
            {
                ColorView c = Instantiate(colorViewPrefab, content).GetComponent<ColorView>();
                c.SetUp(colorList.dataColors[i], i);
                c.UpdateChosen(i ==  chosenColor);
                colorViews.Add(c);
            }
        }

        public void ChooseColor(int id)
        {
            if (chosenColor == id) return;
            if(chosenColor != -1)colorViews[chosenColor].UpdateChosen(false);
            colorViews[id].UpdateChosen(true);
            chosenColor = id;
            PlayerPrefs.SetInt("Color", id);
        }
    }
}
