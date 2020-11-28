using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace MZTATest
{
    [ExecuteInEditMode]
    public class LayoutElementVisualizer : MonoBehaviour
    {
        private ILayoutElement _layoutElement;

        [SerializeField] [ReadOnly] private float MinWidth;
        [SerializeField] [ReadOnly] private float PreferredWidth;
        [SerializeField] [ReadOnly] private float FlexibleWidth;
        [SerializeField] [ReadOnly] private float MinHeight;
        [SerializeField] [ReadOnly] private float PreferredHeight;
        [SerializeField] [ReadOnly] private float FlexibleHeight;
        [SerializeField] [ReadOnly] private int LayoutPriority;

        private void Awake()
        {
            CheckLayout();
        }

        private void Update()
        {
            if (CheckLayout())
            {
                MinWidth = _layoutElement.minWidth;
                PreferredWidth = _layoutElement.preferredWidth;
                FlexibleWidth = _layoutElement.flexibleWidth;
                MinHeight = _layoutElement.minHeight;
                PreferredHeight = _layoutElement.preferredHeight;
                FlexibleHeight = _layoutElement.flexibleHeight;
                LayoutPriority = _layoutElement.layoutPriority;
            }
        }

        private bool CheckLayout()
        {
            if (_layoutElement == null)
                _layoutElement = GetComponents<Component>().FirstOrDefault(c => typeof(ILayoutElement).IsAssignableFrom(c.GetType())) as ILayoutElement;
            return _layoutElement != null;
        }
    }
}
