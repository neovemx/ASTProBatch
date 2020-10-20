using System;
using System.Collections.Generic;
using System.Windows.Input;
using AST_ProBatch_Mobile.Models.Service;
using AST_ProBatch_Mobile.Utilities;
using GalaSoft.MvvmLight.Command;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace AST_ProBatch_Mobile.ViewModels
{
    public class LotAndCommandChartViewModel : BaseViewModel
    {
        #region Atributes
        private BarChart barchart;
        private LineChart linechart;
        private PointChart pointchart;
        private RadialGaugeChart radialgaugechart;
        private DonutChart donutchart;
        private RadarChart radarchart;
        private bool isvisiblebarchart;
        private bool isvisiblelinechart;
        private bool isvisiblepointchart;
        private bool isvisibleradialgaugechart;
        private bool isvisibledonutchart;
        private bool isvisibleradarchart;
        #endregion

        #region Properties
        public BarChart BarChart
        {
            get { return barchart; }
            set { SetValue(ref barchart, value); }
        }
        public LineChart LineChart
        {
            get { return linechart; }
            set { SetValue(ref linechart, value); }
        }
        public PointChart PointChart
        {
            get { return pointchart; }
            set { SetValue(ref pointchart, value); }
        }
        public RadialGaugeChart RadialGaugeChart
        {
            get { return radialgaugechart; }
            set { SetValue(ref radialgaugechart, value); }
        }
        public DonutChart DonutChart
        {
            get { return donutchart; }
            set { SetValue(ref donutchart, value); }
        }
        public RadarChart RadarChart
        {
            get { return radarchart; }
            set { SetValue(ref radarchart, value); }
        }
        public bool IsVisibleBarChart
        {
            get { return isvisiblebarchart; }
            set { SetValue(ref isvisiblebarchart, value); }
        }
        public bool IsVisibleLineChart
        {
            get { return isvisiblelinechart; }
            set { SetValue(ref isvisiblelinechart, value); }
        }
        public bool IsVisiblePointChart
        {
            get { return isvisiblepointchart; }
            set { SetValue(ref isvisiblepointchart, value); }
        }
        public bool IsVisibleRadialGaugeChart
        {
            get { return isvisibleradialgaugechart; }
            set { SetValue(ref isvisibleradialgaugechart, value); }
        }
        public bool IsVisibleDonutChart
        {
            get { return isvisibledonutchart; }
            set { SetValue(ref isvisibledonutchart, value); }
        }
        public bool IsVisibleRadarChart
        {
            get { return isvisibleradarchart; }
            set { SetValue(ref isvisibleradarchart, value); }
        }
        #endregion

        #region Constructor
        public LotAndCommandChartViewModel(bool IsReload, List<LotAndCommandResult> LotAndCommandResult)
        {
            if (IsReload)
            {
                int colorPos = 1;
                List<ChartEntry> entries = new List<ChartEntry>();
                foreach (LotAndCommandResult lotAndCommandResults in LotAndCommandResult)
                {
                    TimeSpan.TryParse(lotAndCommandResults.ExecutionTime, out TimeSpan time);
                    entries.Add(new ChartEntry((float)time.TotalMilliseconds)
                    {
                        Label = lotAndCommandResults.Command,
                        ValueLabel = lotAndCommandResults.ExecutionTime,
                        Color = SKColor.Parse(GetColor(colorPos))
                    });
                    if (colorPos == 2)
                    {
                        colorPos = 1;
                    }
                    else
                    {
                        colorPos += 1;
                    }
                }
                this.BarChart = new BarChart() { LabelTextSize = 30, Entries = entries };
                this.IsVisibleBarChart = true;
                this.LineChart = new LineChart() { LabelTextSize = 30, Entries = entries };
                this.IsVisibleLineChart = false;
                this.PointChart = new PointChart() { LabelTextSize = 30, Entries = entries };
                this.IsVisiblePointChart = false;
                this.RadialGaugeChart = new RadialGaugeChart() { LabelTextSize = 30, Entries = entries };
                this.IsVisibleRadialGaugeChart = false;
                this.DonutChart = new DonutChart() { LabelTextSize = 30, Entries = entries };
                this.IsVisibleDonutChart = false;
                this.RadarChart = new RadarChart() { LabelTextSize = 30, Entries = entries };
                this.IsVisibleRadarChart = false;
            }
        }
        #endregion

        #region Commands
        public ICommand CloseCommand
        {
            get
            {
                return new RelayCommand(Close);
            }
        }

        private async void Close()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public ICommand ViewCommand
        {
            get
            {
                return new RelayCommand(View);
            }
        }

        private async void View()
        {
            if (IsVisibleBarChart)
            {
                IsVisibleBarChart = false;
                IsVisibleLineChart = true;
                IsVisiblePointChart = false;
                IsVisibleRadialGaugeChart = false;
                IsVisibleDonutChart = false;
                IsVisibleRadarChart = false;
                return;
            }
            if (IsVisibleLineChart)
            {
                IsVisibleBarChart = false;
                IsVisibleLineChart = false;
                IsVisiblePointChart = true;
                IsVisibleRadialGaugeChart = false;
                IsVisibleDonutChart = false;
                IsVisibleRadarChart = false;
                return;
            }
            if (IsVisiblePointChart)
            {
                IsVisibleBarChart = false;
                IsVisibleLineChart = false;
                IsVisiblePointChart = false;
                IsVisibleRadialGaugeChart = true;
                IsVisibleDonutChart = false;
                IsVisibleRadarChart = false;
                return;
            }
            if (IsVisibleRadialGaugeChart)
            {
                IsVisibleBarChart = false;
                IsVisibleLineChart = false;
                IsVisiblePointChart = false;
                IsVisibleRadialGaugeChart = false;
                IsVisibleDonutChart = true;
                IsVisibleRadarChart = false;
                return;
            }
            if (IsVisibleDonutChart)
            {
                IsVisibleBarChart = false;
                IsVisibleLineChart = false;
                IsVisiblePointChart = false;
                IsVisibleRadialGaugeChart = false;
                IsVisibleDonutChart = false;
                IsVisibleRadarChart = true;
                return;
            }
            if (IsVisibleRadarChart)
            {
                IsVisibleBarChart = true;
                IsVisibleLineChart = false;
                IsVisiblePointChart = false;
                IsVisibleRadialGaugeChart = false;
                IsVisibleDonutChart = false;
                IsVisibleRadarChart = false;
                return;
            }
        }
        #endregion

        #region Helpers
        private string GetColor(int pos)
        {
            string color = string.Empty;
            switch (pos)
            {
                case 1:
                    color = "#2255AA";
                    break;
                case 2:
                    color = "#2EABBD";
                    break;
                    //case 1:
                    //    color = StatusColor.Green;
                    //    break;
                    //case 2:
                    //    color = StatusColor.Red;
                    //    break;
                    //case 3:
                    //    color = StatusColor.Orange;
                    //    break;
                    //case 4:
                    //    color = StatusColor.DarkBlue;
                    //    break;
                    //case 5:
                    //    color = StatusColor.Black;
                    //    break;
                    //case 6:
                    //    color = StatusColor.Grey;
                    //    break;
                    //case 7:
                    //    color = StatusColor.Purple;
                    //    break;
                    //case 8:
                    //    color = StatusColor.Blue;
                    //    break;
                    //case 9:
                    //    color = StatusColor.White;
                    //    break;
                    //case 10:
                    //    color = StatusColor.Yellow;
                    //    break;
            }
            return color;
        }
        #endregion

        //private List<ChartEntry> GetFixedData()
        //{
        //    List<ChartEntry> entries = new List<ChartEntry>();
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(445)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(76)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(22)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(30)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(90)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    }); entries.Add(new ChartEntry(70)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(550)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(120)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(90)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(80)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(10)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(20)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(40)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(30)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(10)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    entries.Add(new ChartEntry(90)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2EABBD")
        //    });
        //    entries.Add(new ChartEntry(50)
        //    {
        //        Label = "Valor 1",
        //        ValueLabel = "40",
        //        Color = SKColor.Parse("#2255AA")
        //    });
        //    return entries;
        //}
    }
}
