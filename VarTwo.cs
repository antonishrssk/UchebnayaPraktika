using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace UchebnayaPraktika;

internal class VarTwo
{
    /// <summary>
    /// Определяет, является ли последовательность чисел последовательностью простых чисел.
    /// </summary>
    /// <param name="numbers">Последовательность чисел.</param>
    /// <returns>true, если в numbers только простые числа; иначе, false.</returns>
    internal static bool AreNumbersPrime(List<int> numbers)
    {
        // если в последовательности нет чисел, она не может быть последовательностью чисел
        if (numbers.Count == 0)
            return false;

        foreach (int number in numbers)
        {
            // простыми числами могут быть только положительные числа, кроме числа 1
            if (number <= 1)
                return false;

            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                    return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Рисует график изменения последовательности чисел в главном окне.
    /// </summary>
    /// <param name="values">Последовательность чисел.</param>
    /// <param name="canvas">Контейнер для графика.</param>
    /// <param name="dashedLineValuesContainer">Контейнер для значений пунктирных линий на графике.</param>
    internal static void DrawGraph(List<int> values, Canvas canvas, Grid? dashedLineValuesContainer = null)
    {
        SolidColorBrush graphColor = Brushes.Black;
        int graphThickness = 1;
        int maxValue = values.Max();
        double pointSpacing = canvas.ActualWidth / (values.Count - 1);

        canvas.Children.Add(new Line
        {
            X1 = 0,
            Y1 = canvas.ActualHeight,
            X2 = canvas.ActualWidth,
            Y2 = canvas.ActualHeight,
            Stroke = graphColor,
            StrokeThickness = graphThickness
        }); // ось X
        canvas.Children.Add(new Line
        {
            X1 = 0,
            Y1 = 0,
            X2 = 0,
            Y2 = canvas.ActualHeight,
            Stroke = graphColor,
            StrokeThickness = graphThickness
        }); // ось Y

        // пунктирные линии и их значения
        int dashedLinesCount = 10;
        for (int i = 0; i < dashedLinesCount; i++)
        {
            canvas.Children.Add(new Line
            {
                X1 = 0,
                Y1 = canvas.ActualHeight / dashedLinesCount * i,
                X2 = canvas.ActualWidth,
                Y2 = canvas.ActualHeight / dashedLinesCount * i,
                Stroke = graphColor,
                StrokeThickness = graphThickness,
                StrokeDashArray = [5, 3]
            });

            if (dashedLineValuesContainer == null)
                continue;

            dashedLineValuesContainer.RowDefinitions.Add(new RowDefinition());
            var label = new Label
            {
                Content = maxValue * (i + decimal.One) / dashedLinesCount,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, -13, 0, 0) // чтобы числа появлялись напротив пунктирных линий
            };
            Grid.SetRow(label, dashedLinesCount - i - 1);
            dashedLineValuesContainer.Children.Add(label);
        }

        // график
        var graphLine = new Polyline
        {
            Stroke = graphColor,
            StrokeThickness = graphThickness
        };
        for (int i = 0; i < values.Count; i++)
        {
            double pointX = pointSpacing * i;
            double pointY = canvas.ActualHeight - canvas.ActualHeight * values[i] / maxValue;
            graphLine.Points.Add(new Point(pointX, pointY));
        }
        canvas.Children.Add(graphLine);
    }
}
