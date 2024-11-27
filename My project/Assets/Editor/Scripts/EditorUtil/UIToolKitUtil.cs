using System;
using UnityEngine;
using UnityEngine.UIElements;

public static class UIToolKitUtil
{
    public const int DEFAULT_TITTLE_FONT_SIZE = 20;
    public const int DEFAULT_ITEM_FONT_SIZE = 15;

    public static VisualElement CreateBox()
    {
        return new VisualElement
        {
            style =
            {
                flexDirection = FlexDirection.Column,
                alignItems = Align.Center,
                width = Length.Percent(90)
            }
        };
    }

    public static VisualElement CreateContainer()
    {
        return new VisualElement
        {
            style =
            {
                flexDirection = FlexDirection.Row,
                alignItems = Align.Center
            }
        };
    }

    public static Label CreateTittle(string content)
    {
        return new Label
        {
            text = content,
            style =
            {
                fontSize = DEFAULT_TITTLE_FONT_SIZE,
                color = Color.white
            }
        };
    }

    public static Label CreateItemName(string name)
    {
        return new Label
        {
            text = name,
            style =
            {
                fontSize = DEFAULT_ITEM_FONT_SIZE,
                backgroundColor = Color.white,
                color = Color.black
            }
        };
    }

    public static Button CreateItemButton(string content, Action callback)
    {
        return new Button(() => callback?.Invoke())
        {
            text = content,
            style =
            {
                width = 50,
                height = 20,
            }
        };
    }

    public static VisualElement CreateUnderLine()
    {
        return new VisualElement()
        {
            style =
            {
                height = 2,
                backgroundColor = Color.white,
                marginTop = 3,
                marginBottom = 3,
                flexGrow = 1,
                width = Length.Percent(100)
            }
        };
    }
}