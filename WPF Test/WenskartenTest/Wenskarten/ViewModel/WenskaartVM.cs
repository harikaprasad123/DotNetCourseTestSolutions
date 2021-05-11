using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wenskarten.Model;

namespace Wenskarten.ViewModel
{
    public class WenskaartVM : ViewModelBase
    {
        private Wenskaart wenskaart;
        private string colorSelected;
        private List<string> colorList = new List<string>();
        private List<string> wensFontFamilyList = new List<string>();
        private string status;
        private Visibility showGridData = Visibility.Hidden;
        private Boolean opslaanEnabled;
        private ObservableCollection<BalVM> ballen;

        public WenskaartVM(Wenskaart wenskaart)
        {
            this.wenskaart = wenskaart;
        }

        public Boolean OpslaanEnabled
        {
            get
            {
                return opslaanEnabled;
            }
            set
            {
                opslaanEnabled = value;
                RaisePropertyChanged("OpslaanEnabled");
            }
        }
        private void ClearWensKaart()
        {
            OpslaanEnabled = false;
            ShowGridData = Visibility.Hidden;
            ColorSelected = ColorList.FirstOrDefault();
            CanvasAchtergrond = null;
            WensText = string.Empty;
            WensFontFamily = WensFontFamilyList.FirstOrDefault();
            WensFontSize = 10;
            Ballen = new ObservableCollection<BalVM>();
            Status = "Nieuw";
        }
       
        public RelayCommand NieuwCommand
        {
            get
            {
                return new RelayCommand(ClearWensKaart);
            }
        }
       
        public RelayCommand OpenenCommand
        {
            get
            {
                return new RelayCommand(Openen);
            }
        }
        private void Openen()
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog
                {
                    FileName = "",
                    DefaultExt = ".txt",
                    Filter = "Wenskaarten |*.txt"
                };
                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        CanvasAchtergrond = new ImageBrush
                        {
                            ImageSource = new BitmapImage(new Uri($"pack://application:,,,{bestand.ReadLine()}", UriKind.Absolute))
                        };

                        int aantalBallen = int.Parse(bestand.ReadLine());
                        string[] alleDetailsVanDeBallen = bestand.ReadLine().Split('/');
                        Ballen = new ObservableCollection<BalVM>();
                        foreach (string balDetailString in alleDetailsVanDeBallen)
                        {
                            string[] balDetails = balDetailString.Split('_');
                            BalVM bal = new BalVM
                            {
                                Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(balDetails[0]),
                                X = double.Parse(balDetails[1]),
                                Y = double.Parse(balDetails[2]),
                                Tag = balDetails[3],
                                BalMoveCommand = BalMoveCommand
                            };
                            Ballen.Add(bal);
                        }
                        WensText = bestand.ReadLine();
                        WensFontFamily = bestand.ReadLine();
                        WensFontSize = int.Parse(bestand.ReadLine());
                    }
                    ShowGridData = Visibility.Visible;
                    OpslaanEnabled = true;
                    Status = dlg.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Openen mislukt: {ex.Message}");
            }
        }
        
         public RelayCommand OpslaanCommand
        {
            get
            {
                return new RelayCommand(Opslaan);
            }
        }
        private void Opslaan()
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = "Wenskaart.txt",
                    DefaultExt = ".txt",
                    Filter = "Wenskaarten |*.txt"
                };

                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        string path = ((BitmapImage)CanvasAchtergrond.ImageSource).UriSource.AbsolutePath;
                        bestand.WriteLine(path);
                        bestand.WriteLine(Ballen.Count);
                        bestand.WriteLine(String.Join("/", Ballen));
                        bestand.WriteLine(WensText);
                        bestand.WriteLine(WensFontFamily);
                        bestand.WriteLine(WensFontSize);
                    }
                    Status = dlg.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Opslaan mislukt: {ex.Message}");
            }
        }

        public RelayCommand AfsluitenCommand
        {
            get
            {
                return new RelayCommand(Afsluiten);
            }
        }
        private void Afsluiten()
        {
            Application.Current.MainWindow.Close();
        }

        public RelayCommand KerstkaartOpenCommand
        {
            get
            {
                return new RelayCommand(KerstkaartOpen);
            }
        }
        private void KerstkaartOpen()
        {
            ClearWensKaart();
            ShowGridData = Visibility.Visible;
            OpslaanEnabled = true;
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/kerstkaart.jpg", UriKind.Absolute))
            };
            CanvasAchtergrond = imageBrush;
        }
        public RelayCommand GeboorteKaartOpenCommand
        {
            get
            {
                return new RelayCommand(GeboortekaartOpen);
            }
        }
        private void GeboortekaartOpen()
        {
            ClearWensKaart();
            ShowGridData = Visibility.Visible;
            OpslaanEnabled = true;
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/geboortekaart.jpg", UriKind.Absolute))
            };
            CanvasAchtergrond = imageBrush;
        }

        public Visibility ShowGridData
        {
            get
            {
                return showGridData;
            }
            set
            {
                showGridData = value;
                RaisePropertyChanged("ShowGridData");
            }
        }

        public ImageBrush CanvasAchtergrond
        {
            get
            {
                return wenskaart.CanvasAchtergrond;
            }
            set
            {
                wenskaart.CanvasAchtergrond = value;
                RaisePropertyChanged("CanvasAchtergrond");
            }
        }
        public string ColorSelected
        {
            get => colorSelected;
            set
            {
                colorSelected = value;
                RaisePropertyChanged("ColorSelected");
            }
        }
        public List<string> ColorList
        {
            get
            {
                foreach (PropertyInfo info in typeof(Colors).GetProperties())
                {
                    colorList.Add(info.Name);
                }

                return colorList;
            }
        }
        public string WensFontFamily
        {
            get
            {
                return wenskaart.WensFontFamily;
            }
            set
            {
                wenskaart.WensFontFamily = value;
                RaisePropertyChanged("WensFontFamily");
            }
        }
        public List<string> WensFontFamilyList
        {
            get
            {
                foreach (FontFamily font in Fonts.SystemFontFamilies)
                {
                    wensFontFamilyList.Add(font.Source);
                }
                wensFontFamilyList.Sort();

                return wensFontFamilyList;
            }
        }
        public int WensFontSize
        {
            get
            {
                return wenskaart.WensFontSize;
            }
            set
            {
                wenskaart.WensFontSize = value;
                RaisePropertyChanged("WensFontSize");
            }
        }
        public RelayCommand VergrootFontSize
        {
            get
            {
                return new RelayCommand(VergrootDeFontSize);
            }
        }
        private void VergrootDeFontSize()
        {
            if (WensFontSize < 40)
                WensFontSize++;
        }
        public RelayCommand VerlaagFontSize
        {
            get
            {
                return new RelayCommand(VerlaagDeFontSize);
            }
        }
        private void VerlaagDeFontSize()
        {
            if (WensFontSize > 10)
                WensFontSize--;
        }

        public string WensText
        {
            get
            {
                return wenskaart.WensText;
            }
            set
            {
                wenskaart.WensText = value;
                RaisePropertyChanged("WensText");
            }
        }
          
        public string Status
        {
            get 
            {
                return status;
            }
             set
            {
                status = value;
                RaisePropertyChanged("Status");
            }
        }     
       public ObservableCollection<BalVM> Ballen
        {
            get 
            {
                return ballen;
            }
            set
            {
                ballen = value;
                RaisePropertyChanged("Ballen");
            }
        }

        public RelayCommand<MouseEventArgs> BalMoveCommand
        {
            get
            {
                return new RelayCommand<MouseEventArgs>(BalMove);
            }
        }
        private void BalMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Ellipse gesleeptebal = (Ellipse)e.OriginalSource;
                DataObject dataobject = new DataObject("deBal", gesleeptebal);
                DragDrop.DoDragDrop(gesleeptebal, dataobject, DragDropEffects.Move);
            }
        }
        public RelayCommand<DragEventArgs> BalDropCommand
        {
            get
            {
                return new RelayCommand<DragEventArgs>(BalDrop);
            }
        }
        private void BalDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("deBal"))
            {
                Ellipse selectedBalData = (Ellipse)e.Data.GetData("deBal");
                BalVM ball = Ballen.FirstOrDefault(bal => bal.Tag == selectedBalData.Tag);

                if (e.OriginalSource is Canvas canvas)
                {
                    if (ball is null)
                        AddBal(selectedBalData, e, canvas);
                    else
                        GetPositionOfBal(ball, e, canvas);
                }
                if (e.OriginalSource is Image && ball != null)
                {
                    Ballen.Remove(ball);
                }
            }
        }

        private void AddBal(Ellipse selectedBalData, DragEventArgs e, Canvas canvas)
        {
            BalVM selectedBal = new BalVM
            {
                Fill = selectedBalData.Fill,
                X = e.GetPosition(canvas).X - 20,
                Y = e.GetPosition(canvas).Y - 20,
                BalMoveCommand = BalMoveCommand,
                Tag = Ballen.Count
            };
            Ballen.Add(selectedBal);
        }

        private void GetPositionOfBal(BalVM ball, DragEventArgs e, Canvas canvas)
        {
            ball.X = e.GetPosition(canvas).X - 20;
            ball.Y = e.GetPosition(canvas).Y - 20;
        }
        public RelayCommand<CancelEventArgs> ClosingCommand 
        { 
            get 
            {
                return new RelayCommand<CancelEventArgs>(OnWindowClosing);
            } 
        }
        public void OnWindowClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("Sluit het programma", "Afsluiten",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
                MessageBoxResult.No)
                e.Cancel = true;
        }     
                
    }
}
