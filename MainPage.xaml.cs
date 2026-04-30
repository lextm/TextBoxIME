using Microsoft.UI.Xaml.Controls;
using ICSharpCode.AvalonEdit.Document;
using UnoEdit.Skia.Desktop.Controls;

namespace TextBoxIME;

public sealed partial class MainPage : Page
{
    private readonly TextDocument _document;

    public MainPage()
    {
        this.InitializeComponent();

        _document = new TextDocument("""
Type here second.

This is the UnoEdit editor loaded from the local UnoEdit project reference.
中文输入测试
""");

        Editor.Document = _document;
        Editor.Theme = TextEditorTheme.Dark;
        Editor.ShowLineNumbers = false;
        Editor.WordWrap = true;

        InputBox.GotFocus += (_, _) => StatusText.Text = "Focus: Uno TextBox";
        InputBox.TextChanged += (_, _) => StatusText.Text = $"TextBox length: {InputBox.Text.Length}";
        Editor.GotFocus += (_, _) => StatusText.Text = "Focus: UnoEdit TextEditor";
        Editor.TextArea.GotFocus += (_, _) => StatusText.Text = "Focus: UnoEdit TextArea";
        _document.TextChanged += (_, _) => StatusText.Text = $"UnoEdit length: {_document.TextLength}";

        StatusText.Text = "Ready";
    }
}
