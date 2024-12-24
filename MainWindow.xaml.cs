using System.Windows;
using System.Windows.Controls;

namespace UchebnayaPraktika;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<int> _values = [];
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void tbValues_TextChanged(object sender, TextChangedEventArgs e)
    {
        txtAreValuesPrime.Text = "";
        canvas.Children.Clear();
        gridDashedLineValues.Children.Clear();
        gridDashedLineValues.RowDefinitions.Clear();

        string[] values = tbValues.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        _values = [];

        // проверка на правильность введенных данных
        for (int i = 0; i < values.Length; i++)
        {
            if (!int.TryParse(values[i], out var n))
            {
                _values.Clear();
                txtAreValuesPrime.Text = "Введите только целые числа";
                break;
            }

            _values.Add(n);
        }

        if (VarTwo.AreNumbersPrime(_values))
            txtAreValuesPrime.Text = "Все числа в введенной последовательности простые";

        if (_values.Count > 1)
            VarTwo.DrawGraph(_values, canvas, gridDashedLineValues);
    }
}
