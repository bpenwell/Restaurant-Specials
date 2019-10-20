using UnityEngine;
using UnityEngine.UI;

public class ColorPallete : AppConstants
{
    //Currently not readonly because we may allow custom color palletes for premium users
    static public Color CameraBackground;
    static public Color Text_Primary;
    static public Color Text_Secondary;
    static public Color Background;
    static public Color Header;
    static public Color ButtonDefault;
    static public Color ButtonSelected;
    static public Color ButtonText;

    public void Start()
    {
        //Set relevant color pallete
        //  This is referenced throughout the project, enabling you to only need to set and change colors in 1 place
        CameraBackground = Color.white;
        Text_Primary = Color.black;
        Text_Secondary = Color.grey;
        Background = new Color(5,5,5);
        Header = Color.red;
        ButtonDefault = Color.white;
        ButtonSelected = new Color(200, 200, 200);
        ButtonText = Color.black;

        if (TryGetComponent<Camera>(out Camera camera))
        {
            camera.backgroundColor = CameraBackground;
        }
        if (TryGetComponent<Text>(out Text text))
        {
            if (text.tag == AppConstants.TextPrimaryConst)
            {
                text.color = Text_Primary;
            }
            else if (text.tag == AppConstants.TextSecondaryConst)
            {
                text.color = Text_Secondary;
            }
        }
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite))
        {
            if (sprite.tag == AppConstants.BackgroundConst)
            {
                sprite.color = Background;
            }
        }
        if (TryGetComponent<Image>(out Image header))
        {
            if (header.tag == AppConstants.HeaderConst)
            {
                header.color = Header;
            }
        }
        if (TryGetComponent<Image>(out Image image))
        {
            if (image.tag == AppConstants.ButtonConst)
            {
                image.color = ButtonDefault;
                if (TryGetComponent<Button>(out Button button))
                {
                    ColorBlock cb = button.colors;
                    cb.highlightedColor = ButtonSelected;
                    button.colors = cb;
                }
                Text tryText = gameObject.GetComponentInChildren<Text>();
                if (tryText != default)
                {
                    tryText.color = ButtonText;
                }
            }
        }
    }
}
