using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
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
using Microsoft.Win32;

namespace Calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        public string Display
        {
            get => _display;

            set
            {
                if (_display == value) return;
                _display = value;
                OnPropertyChanged();
            }
        }

        private string _display = "";

        public RelayCommand<string> ObrabotchikKlavish { get; }

        public ObservableCollection<MemoryValue> MemoryValues { get; set; }

        public RelayCommand<MemoryValue> OnRemoveItem { get; }
        public RelayCommand<MemoryValue> OnGetItem { get; }
        public ViewModel()
        {
            MemoryValues = LoadFromFile();

            OnRemoveItem = new RelayCommand<MemoryValue>(mv =>
            {
                MemoryValues.Remove(mv);
                SaveToFile();
            });

            OnGetItem = new RelayCommand<MemoryValue>(mv =>
            {
                Display += mv.Value;
            });

            ObrabotchikKlavish = new RelayCommand<string>((arg) =>
            {
                switch (arg)
                {
                    case "=":
                        try
                        {
                            Display = Evaluate(Display).ToString();
                        }
                        catch (InvalidOperationException)
                        {
                            Display = "Unknown cmd";
                        }
                        catch (DivideByZeroException)
                        {
                            Display = "Div by zero";
                        }
                        break;
                    case "C":
                        Display = "";
                        break;
                    case "MC":
                        MemoryValues.Clear();
                        SaveToFile();
                        break;
                    case "MS":
                        MemoryValues.Add(new MemoryValue { ID = MemoryValues.Count, Value = Display });
                        SaveToFile();
                        break;
                    default:
                        Display += arg;
                        break;
                }
            });
        }



        
        private decimal Evaluate(string primer)
        {
            var exp = new MathExpression();
            return exp.ParsePolishNotation(primer);
        }


        private const string CFilePath = "memory.json";
        private void SaveToFile()
        {
            File.WriteAllText(CFilePath, JsonSerializer.Serialize(MemoryValues));
        }

        private ObservableCollection<MemoryValue> LoadFromFile()
        {
            
            try
            {
                return JsonSerializer.Deserialize<ObservableCollection<MemoryValue>>
                    (File.ReadAllText(CFilePath));
            }
            
            catch
            {
                return new ObservableCollection<MemoryValue>();
            }

        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MemoryValue
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }
}
