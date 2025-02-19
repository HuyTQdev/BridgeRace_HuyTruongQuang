using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI {
    public class ColorView : MonoBehaviour
    {
        [SerializeField] Image img;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Image choosenIcon, unChoosenIcon;
        int id;

        public void SetUp(DataColor dataColor, int id)
        {
            img.color = dataColor.renderColor;
            if(text != null) text.text = dataColor.name;
            this.id = id;
        }

        public async void Select()
        {
            var colorListCanvas = await UIManager.Instance.GetUI<CanvasColorList>();
            colorListCanvas.ChooseColor(id);
        }

        public void UpdateChosen(bool v)
        {
            if(choosenIcon != null) choosenIcon.enabled = v;
            if(unChoosenIcon != null) unChoosenIcon.enabled = !v;
        }
    }
}
