using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Cash_Register_Simulator___20._03._2018
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //PRODUCT CLASS
        public class Product
        {
            [XmlElement("title")]
            public string Title { get; set; }
            [XmlElement("name")]
            public string Name { get; set; }
            [XmlElement("price")]
            public string Price { get; set; }
        }

        //INITIALIZING THE LIST
        public List<Product> AList = new List<Product>()
        {
            
        };



        public List<Product> ShoppingCart = new List<Product>();
        public string Calculator;
        public int SUM;
        public int Cash = 100;


        public MainWindow()
        {
            InitializeComponent();
            ProductsAndPrices.ItemsSource = AList;
            ShoppingCartListBox.ItemsSource = ShoppingCart;
            CashDisplay.Text = Cash + " €";

            //// XML FILE READ/SAVE



            string fileName = "../../resources/products.xml";
            FileInfo f = new FileInfo(fileName);
            string fullname = f.FullName;


            var doc = new XDocument();

            var serializer = new XmlSerializer(typeof(List<Product>));

            XmlSerializer myDeserializer = new XmlSerializer(typeof(List<Product>));
            FileStream myFileStream = new FileStream("../../resources/products.xml", FileMode.Open);
            var XmlData = (List<Product>)myDeserializer.Deserialize(myFileStream);

            AList = XmlData;

            myFileStream.Close();

            ProductsAndPrices.ItemsSource = AList;
            ProductsAndPrices.Items.Refresh();

            //// FINISHED LOADING XML FILE TO THE PRODUCT LIST
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            Cash = Cash - SUM;
            CashDisplay.Text = Cash + " €";
            SUM = 0;
            ShoppingCart.Clear();
            ShoppingCartListBox.Items.Refresh();
        }

        private void AddCash_Click(object sender, RoutedEventArgs e)
        {
            Cash = Cash + 100;
            CashDisplay.Text = Cash + " €";
        }

        //ADD A PRODUCT TO THE CART
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            SUM = 0;
            ShoppingCart.Add((Product)ProductsAndPrices.SelectedItem);
            foreach(Product item in ShoppingCart)
            {
                var ItemSum = Int32.Parse(item.Price);
                SUM =  SUM + ItemSum;
                TotalSum.Text = SUM.ToString() + " €";
            }
            if (SUM <= 0)
            {
                TotalSum.Clear();
            }
            ShoppingCartListBox.Items.Refresh();
        }

        //REMOVE A PRODUCT FROM THE CART
        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            SUM = 0;
            ShoppingCart.Remove((Product)ShoppingCartListBox.SelectedItem);
            foreach (Product item in ShoppingCart)
            {
                var ItemSum = Int32.Parse(item.Price);
                SUM = SUM + ItemSum;
                TotalSum.Text = SUM.ToString() + " €";
            }
            if( SUM <= 0)
            {
                TotalSum.Clear();
            }
            ShoppingCartListBox.Items.Refresh();
        }

        #region The Product List

        //ADD TO PRODUCT LIST
        private void AddToList_Click(object sender, RoutedEventArgs e)
        {
            AList.Add(new Product() { Name = InsertName.Text, Price = InsertPrice.Text});
            
            ProductsAndPrices.ItemsSource = AList;
            ProductsAndPrices.Items.Refresh();
            InsertName.Clear();
            InsertPrice.Clear();
        }
        
        public List<Product> SelectionList = new List<Product>();

        //SCROLL UP IN PRODUCT LIST
        private void ButtonGoUp_Click(object sender, RoutedEventArgs e)
        {
            int nextIndex = 0;
            if ((ProductsAndPrices.SelectedIndex >= 0) && (ProductsAndPrices.SelectedIndex < (ProductsAndPrices.Items.Count - 1)))
            {
                nextIndex = ProductsAndPrices.SelectedIndex - 1;
            }
            ProductsAndPrices.SelectedIndex = nextIndex;
        }

        //SCROLL DOWN IN PRODUCT LIST
        private void ButtonGoDown_Click(object sender, RoutedEventArgs e)
        {
            int nextIndex = 0;
            if ((ProductsAndPrices.SelectedIndex >= 0) && (ProductsAndPrices.SelectedIndex < (ProductsAndPrices.Items.Count - 1)))
            {
                nextIndex = ProductsAndPrices.SelectedIndex + 1;
            }
            ProductsAndPrices.SelectedIndex = nextIndex;
        }

        //DELETE ITEM FROM PRODUCT LIST
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            AList.Remove((Product)ProductsAndPrices.SelectedItem);
            ProductsAndPrices.Items.Refresh();
        }

        

        #endregion

        #region Calculator Functions

        //EXECUTE THE ENTERED EQUATION
        private void CalculatorExecute_Click(object sender, RoutedEventArgs e)
        {
            double result = Convert.ToDouble(new DataTable().Compute(CalculatorScreen.Text, null));

            var FinalResult = result;
            var Answer = Math.Round(FinalResult, 2);

            string FinalAnswer = Answer.ToString();
            FinalAnswer = FinalAnswer.Replace(',', '.');

            CalculatorScreen.Text = FinalAnswer;
            Calculator = FinalAnswer;
        }

        //CLEAR THE CALCULATOR
        private void CalculatorClear_Click(object sender, RoutedEventArgs e)
        {
            Calculator = "";
            CalculatorScreen.Clear();
        }

        //
        //NUMBER CLICKS
        //
        private void ButtonOne_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "1";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonTwo_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "2";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonThree_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "3";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonFour_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "4";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonFive_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "5";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonSix_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "6";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonSeven_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "7";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonEight_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "8";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonNine_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "9";
            CalculatorScreen.Text = Calculator;
        }

        private void ButtonZero_Click(object sender, RoutedEventArgs e)
        {
            Calculator += "0";
            CalculatorScreen.Text = Calculator;
        }

        //
        //SYMBOLS//
        //

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Calculator.EndsWith(" + ") || Calculator.EndsWith(" - ") || Calculator.EndsWith(" / ") || Calculator.EndsWith(" * ") || Calculator.EndsWith(" "))
            {

            }
            else
            {
                Calculator += " + ";
                CalculatorScreen.Text = Calculator;
            }
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            if (Calculator.EndsWith(" + ") || Calculator.EndsWith(" - ") || Calculator.EndsWith(" / ") || Calculator.EndsWith(" * ") || Calculator.EndsWith(" "))
            {
                
            }
            else
            {
                Calculator += " - ";
                CalculatorScreen.Text = Calculator;
            }
        }

        private void ButtonDivide_Click(object sender, RoutedEventArgs e)
        {
            if (Calculator.EndsWith(" + ") || Calculator.EndsWith(" - ") || Calculator.EndsWith(" / ") || Calculator.EndsWith(" * ") || Calculator.EndsWith(" "))
            {

            }
            else
            {
                Calculator += " / ";
                CalculatorScreen.Text = Calculator;
            }
        }

        private void ButtonMultiply_Click(object sender, RoutedEventArgs e)
        {
            if (Calculator.EndsWith(" + ") || Calculator.EndsWith(" - ") || Calculator.EndsWith(" / ") || Calculator.EndsWith(" * ") || Calculator.EndsWith(" "))
            {

            }
            else
            {
                Calculator += " * ";
                CalculatorScreen.Text = Calculator;
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
                SUM = Int32.Parse(Calculator);
                TotalSum.Text = SUM.ToString() + " €";
        }

        #endregion

        private void AddCartSum_Click(object sender, RoutedEventArgs e)
        {
            Calculator += SUM.ToString();
            CalculatorScreen.Text = Calculator;
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            if (Calculator.Length > 0 & Calculator.Contains(""))
            {
                if (Calculator.EndsWith(" + ") || Calculator.EndsWith(" - ") || Calculator.EndsWith(" / ") || Calculator.EndsWith(" * ") || Calculator.EndsWith(" "))
                {
                    Calculator = Calculator.Substring(0, Calculator.Length - 3);
                    CalculatorScreen.Text = Calculator;
                }
                else
                {
                    Calculator = Calculator.Substring(0, Calculator.Length - 1);
                    CalculatorScreen.Text = Calculator;
                }
            }
            else if (Calculator.Length == 0 & Calculator != null)
            {

            }
        }

        private void OnOff_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            string fileName = "../../resources/products.xml";
            FileInfo f = new FileInfo(fileName);
            string fullname = f.FullName;

            var doc = new XDocument();
            using (var writer = doc.CreateWriter())
            {
                var serializer1 = new XmlSerializer(typeof(List<Product>));

                serializer1.Serialize(writer, AList);
            }
            doc.Save(fullname);
        }

        
    }
}
