using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calc_IMT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<String> reply = new List<String>
        {
            "Острый дефицит массы",
            "Недостаточная масса тела",
            "Норма",
            "Избыточная масса тела",
            "Ожирение первой степени",
            "Ожирение второй степени",
            "Ожирение третьей степени"
        };

        private double resBMI, height, weight;
        const ushort minHeight = 50, maxHeight = 250,
                    minWeight = 30, maxWeight = 200;

        public MainWindow()
        {
            InitializeComponent();
        }

        private Boolean CheckData(double height, double weight)
        {
            return ((height >= minHeight) && (height <= maxHeight) && 
                (weight >= minWeight) && (weight <= maxWeight)) ? true : false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            label_IMTResult.Content = "";
            texbBlock_IMTResult.Text = "";
            if ((textBox_Height.Text == "") || (textBox_Weight.Text == ""))
                return;

            try
            {
                height = Convert.ToDouble(textBox_Height.Text);
                weight = Convert.ToDouble(textBox_Weight.Text);
                if (!CheckData(height, weight))
                {
                    labelRange1.Content = "out of range! [50 .. 250]";
                    labelRange2.Content = "out of range! [30 .. 200]";
                    return;
                }
                resBMI = Math.Round(resBMI = BMI(height, weight), 2);
                label_IMTResult.Content = resBMI.ToString() + " кг/м²";
                texbBlock_IMTResult.Text = reply[Message(resBMI)];
                labelRange1.Content = "";
                labelRange2.Content = "";
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Ошибка: {exception.Message}");
            }
        }

        // Функция расчета ИМТ
        // m - масса тела (в кг)
        // h - рост (в см)
        private double BMI(double h, double m)
        {
            h /= 100; // переводим рост из см в м
            if ((m > 0) && (h > 0))
                return (m / h / h);
            else throw new Exception("Некорректные данные");
        }

        private ushort Message(double resBMI)
        {
            if (resBMI < 15)
                return 0;
            else
                if ((resBMI >= 15) && (resBMI <= 20))
                return 1;
            else
                if ((resBMI >= 20) && (resBMI <= 25))
                return 2;
            else
                if ((resBMI >= 25) && (resBMI <= 30))
                return 3;
            else if ((resBMI >= 30) && (resBMI <= 35))
                return 4;
            else if ((resBMI >= 35) && (resBMI <= 40))
                return 5;
            else return 6;
        }
    }
}
