using System.Numerics;
using System.Text;
using Raylib_cs;
namespace Network_Drawing_Test;
public class Button{
    public StringBuilder buttonString;
    public int stringHeight;
    public Color colorString {get;}
    private Vector2 stringPosition = new(); // TODO: do the calculations
    private Vector2 stringSize;
    
    public Rectangle buttonRect {get;}
    public Color colorRect {get;}

    public event Action onClick;
    public int padding {get;}
    
    public Button(StringBuilder buttonString, int stringHeight, Rectangle buttonRect, Action onClick, int padding, Color colorRect, Color colorString)
    {
        this.buttonString = buttonString;
        this.stringHeight = stringHeight;
        this.buttonRect = buttonRect;
        this.onClick = onClick;
        this.padding = padding;
        this.colorRect = colorRect;
        this.colorString = colorString;
        // TODO: do the string calculations here
    }


    private bool wasClicked = false;
    public void Update()
    {
        Vector2 mousePos = Raylib.GetMousePosition();
        if(Raylib.CheckCollisionPointRec(mousePos, buttonRect)) // hover
        {
            if(wasClicked && Raylib.IsMouseButtonReleased(MouseButton.Left)) // action
            {
                wasClicked = false;
                onClick?.Invoke();
            }
            if(Raylib.IsMouseButtonPressed(MouseButton.Left)) // clicked
            {
                if(!wasClicked) wasClicked = true;
            }
        }
    }
    public void Draw()
    {
        Raylib.DrawRectangleRec(buttonRect, colorRect);
        // TODO: add string drawing
    }
}
