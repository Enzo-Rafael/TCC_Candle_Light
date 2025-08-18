using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace CandleLight.Editor
{
    public static class EditorUIUtils
    {
        public static string AlignFieldClassName => BaseField<bool>.alignedFieldUssClassName;
        public static float SingleLineHeight => EditorGUIUtility.singleLineHeight;

        public static void SetVisible(this VisualElement e, bool show)
            => e.style.display = show ? StyleKeyword.Null : DisplayStyle.None;

        public static bool IsVisible(this VisualElement e) => e.style.display != DisplayStyle.None;

        public static T AddChild<T>(this VisualElement e, T child) where T : VisualElement
        {
            e.Add(child);
            return child;
        }

        public static void SafeSetIsDelayed(this VisualElement e, string name = null)
        {
            var f = e.Q<FloatField>(name);
            if (f != null)
                f.isDelayed = true;
            var i = e.Q<IntegerField>(name);
            if (i != null)
                i.isDelayed = true;
            var t = e.Q<TextField>(name);
            if (t != null)
                t.isDelayed = true;
        }

        public static void AddHeader(this VisualElement ux, string text, string tooltip = "")
        {
            ux.AddChild(new Label($"<b>{text}</b>")
            {
                tooltip = tooltip,
                style =
                {
                    marginTop = SingleLineHeight / 2,
                    marginBottom = EditorGUIUtility.standardVerticalSpacing / 2,
                    marginLeft = 3
                }
            });
        }
        public static void ContinuousUpdate(
            this VisualElement owner, EditorApplication.CallbackFunction callback)
        {
            owner.RegisterCallback<AttachToPanelEvent>(_ =>
            {
                owner.OnInitialGeometry(callback); 
                EditorApplication.update += callback;
                owner.RegisterCallback<DetachFromPanelEvent>(_ => EditorApplication.update -= callback);
            });
        }
        public static void OnInitialGeometry(
            this VisualElement owner, EditorApplication.CallbackFunction callback)
        {
            owner.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            void OnGeometryChanged(GeometryChangedEvent _)
            {
                owner.UnregisterCallback<GeometryChangedEvent>(OnGeometryChanged); // call only once
                callback();
            }
        }
        
        public static void AddSpace(this VisualElement ux)
        {
            ux.Add(new VisualElement { style = { height = SingleLineHeight / 2 } });
        }

        public static Label MiniHelpIcon(string tooltip, HelpBoxMessageType iconType = HelpBoxMessageType.Warning)
        {
            string icon = iconType switch
            {
                HelpBoxMessageType.Warning => "console.warnicon.sml",
                HelpBoxMessageType.Error => "console.erroricon.sml",
                _ => "console.infoicon.sml",
            };
            return new Label
            {
                tooltip = tooltip,
                style =
                {
                    flexGrow = 0,
                    flexBasis = SingleLineHeight,
                    backgroundImage = (StyleBackground)EditorGUIUtility.IconContent(icon).image,
                    width = SingleLineHeight, height = SingleLineHeight,
                    alignSelf = Align.Center
                }
            };
        }

        public class LeftRightRow : VisualElement
        {
            public VisualElement Left;
            public VisualElement Right;

            public LeftRightRow(VisualElement left = null, VisualElement right = null)
            {
                Add(new AlignFieldSizer { OnLabelWidthChanged = (w) => Left.style.width = w });

                var Row = AddChild(this, new VisualElement { style = { marginLeft = 3, flexDirection = FlexDirection.Row } });

                left ??= new VisualElement();
                Left = Row.AddChild(left);
                Left.style.flexDirection = FlexDirection.Row;

                right ??= new VisualElement();
                Right = Row.AddChild(right);
                Right.style.flexGrow = 1;
            }

            private class AlignFieldSizer : BaseField<bool>
            {
                public Action<float> OnLabelWidthChanged;
                public AlignFieldSizer() : base(" ", null)
                {
                    AddToClassList(AlignFieldClassName);
                    style.height = 0; 
                    style.marginTop = -EditorGUIUtility.standardVerticalSpacing; 
                    labelElement.RegisterCallback<GeometryChangedEvent>((evt) => OnLabelWidthChanged?.Invoke(labelElement.resolvedStyle.width));
                }
            }
        }

        public class LabeledRow : LeftRightRow
        {
            public Label Label { get; private set; }
            public VisualElement Contents { get; private set; }

            public LabeledRow(string label, string tooltip = "", VisualElement contents = null)
                : base(new Label(label) { tooltip = tooltip, style = { alignSelf = Align.Center } }, contents)
            {
                Label = Left as Label;
                Contents = Right;
                Contents.tooltip = tooltip;
            }
        }
        public static void AddRemainingProperties(VisualElement ux, SerializedProperty property)
        {
            if (property != null)
            {
                var p = property.Copy();
                do
                {
                    if (p.name != "m_Script")
                        ux.Add(new PropertyField(p));
                }
                while (p.NextVisible(false));
            }
        }
    }
}