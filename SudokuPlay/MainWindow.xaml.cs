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

namespace SudokuPlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int>[,] leftItems = new List<int>[9, 9];

        TextBox[,] txtList = new TextBox[9, 9];
        int[,] resultValues = new int[9, 9];

        public MainWindow()
        {
            InitializeComponent();

            GenerateTextBoxes();
        }

        private void GenerateTextBoxes()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    leftItems[i, j] = GenerateDefaultSet();

                    TextBox txt = new TextBox();
                    txt.TextAlignment = TextAlignment.Center;
                    txt.Width = 30;
                    txt.Height = 30;
                    if (j % 3 == 0)
                        txt.HorizontalAlignment = HorizontalAlignment.Right;

                    if (j % 3 == 2)
                        txt.HorizontalAlignment = HorizontalAlignment.Left;

                    if (i % 3 == 0)
                        txt.VerticalAlignment = VerticalAlignment.Bottom;
                    if (i % 3 == 2)
                        txt.VerticalAlignment = VerticalAlignment.Top;


                    Grid.SetRow(txt, i);
                    Grid.SetColumn(txt, j);
                    grdSudoku.Children.Add(txt);

                    txtList[i, j] = txt;

                    resultValues[i, j] = 0;
                }
            }
        }

        List<int> GenerateDefaultSet()
        {
            List<int> var = new List<int>();
            var.Add(1);
            var.Add(2);
            var.Add(3);
            var.Add(4);
            var.Add(5);
            var.Add(6);
            var.Add(7);
            var.Add(8);
            var.Add(9);
            return var;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            btnStart.IsEnabled = false;
            btnSolve.IsEnabled = true;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!String.IsNullOrEmpty(txtList[i, j].Text))
                    {
                        int value = int.Parse(txtList[i, j].Text);

                        txtList[i, j].IsEnabled = false;

                        resultValues[i, j] = value;

                        int areaRow = -1;
                        int areaColumn = -1;
                        if (i >= 0 && i <= 2)
                            areaRow = 0;
                        else if (i >= 3 && i <= 5)
                            areaRow = 1;
                        else
                            areaRow = 2;

                        if (j >= 0 && j <= 2)
                            areaColumn = 0;
                        else if (j >= 3 && j <= 5)
                            areaColumn = 1;
                        else
                            areaColumn = 2;


                        txtList[i, j].Background = Brushes.AliceBlue;
                    }
                }
            }
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
        Start:
            for (int i = 0; i < 9; i++)//row
            {
                for (int j = 0; j < 9; j++)//column
                {
                    if (resultValues[i, j] != 0)
                        leftItems[i, j].Clear();
                    else
                    {
                        //Check for Rows
                        for (int a = 0; a < 9; a++)
                            leftItems[i, j].Remove(resultValues[i, a]);

                        //Check for Columns
                        for (int a = 0; a < 9; a++)
                            leftItems[i, j].Remove(resultValues[a, j]);

                        //Check for Sets
                        for (int x = ((i / 3) * 3); x <= ((i / 3) * 3) + 2; x++)
                        {
                            for (int y = ((j / 3) * 3); y <= ((j / 3) * 3) + 2; y++)
                            {
                                leftItems[i, j].Remove(resultValues[x, y]);
                            }
                        }

                        if (leftItems[i, j].Count() == 1)
                        {
                            resultValues[i, j] = leftItems[i, j].First();
                            goto Start;
                        }
                    }
                }
            }

            //Show Results
            for (int i = 0; i < 9; i++)//row
            {
                for (int j = 0; j < 9; j++)//column
                {
                    txtList[i, j].Text = resultValues[i, j].ToString();
                }
            }
        }
        
    }
}
