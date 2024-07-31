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

        public void Select()
        {
            UIManager.Instance.GetUI<CanvasColorList>().ChooseColor(id);
        }

        public void UpdateChosen(bool v)
        {
            if(choosenIcon != null) choosenIcon.enabled = v;
            if(unChoosenIcon != null) unChoosenIcon.enabled = !v;
            Debug.Log(id + ": " + v);
        }
    }
}
