using Microsoft.UI.Xaml.Controls;
using System;

namespace TextBoxIME;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();

        InputBox.GotFocus += (_, _) => StatusText.Text = "Focus: Uno TextBox";
        InputBox.TextChanged += (_, _) => StatusText.Text = $"TextBox length: {InputBox.Text.Length}";

        StatusText.Text = "Ready";
    }
}
