namespace LaLuz.Controls;

public partial class MyEntry : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(MyEntry), string.Empty, BindingMode.TwoWay);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }


    public MyEntry()
	{
		InitializeComponent();
        entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), source: this)); // Vincula el Entry interno

    }


    public string Placeholder
    {
        get => entry.Placeholder;
        set => entry.Placeholder = value;
    }

    public Keyboard Keyboard
    {
        get => entry.Keyboard;
        set => entry.Keyboard = value;
    }
}
