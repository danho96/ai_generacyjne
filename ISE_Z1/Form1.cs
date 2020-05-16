using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;
namespace ISE_Z1
{
    public partial class Form1 : Form
    {
        //lista z opisem wspó³czynników funkcji dopasowywanych
        public List<string> population = new List<string>();
        //lista z przebiegiem funkcji dopasowywanych
        public List<PointPairList> populationList = new List<PointPairList>();
        //kolory poszczególnych przebiegów
        public List<Color> populationColor = new List<Color>();

        //iloœæ punktów na osi x
        int pointAmount = 1000;
        //generator liczb pseudolosowych
        Random rand = new Random(1000);
        //lista punktów przebiegu do którego dopasowujemy
        PointPairList ppl = new PointPairList();

        public Form1()
        {
            InitializeComponent();
            graph.GraphPane.Title.IsVisible = false;
            InitializePopulation(population, populationColor);
        }

        /// <summary>
        /// Inicjalizuje funkcje
        /// </summary>
        private void InitializePopulation(List<string> population, List<Color> colors)
        {
            AddPopulation(population);
            AddColors(colors);
        }

        //tworzy funkcjê do której dopasowujemy
        private void buttonCreateFunction_Click(object sender, EventArgs e)
        {
            ppl.Clear();
            double previousValue = 0;
            double multip = 3000000000;
            int direction = 1;
            for (int i = 0; i < pointAmount; i++)
            {
                direction = (rand.NextDouble() < 0.5) ? 1 : -1;
                previousValue += direction * multip * rand.NextDouble();
                PointPair pp = new PointPair(i, previousValue);
                ppl.Add(pp);
            }
            graph.GraphPane.CurveList.Clear();
            graph.GraphPane.AddCurve("Funkcja", ppl, Color.Red, SymbolType.None);
            RefreshGraph();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                backgroundWorker.CancelAsync();
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //wyczyœæ wykres
            graph.GraphPane.CurveList.Clear();
            //dodaj przebieg funkcji do której dopasowujemy
            graph.GraphPane.AddCurve("Wykres", ppl, Color.Red, SymbolType.None);
            CreateGeneration();
            //dodaj wykresy fukcji dopasowywanych

            for (int i = 0; i < population.Count; i++)
            {
                //pobierz wspó³czynniki funkji
                var coefficient = population[i]
                    .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => Convert.ToDouble(x))
                    .ToList();

                PointPairList popPairList = new PointPairList();
                //stwórz przebieg
                for (int j = 0; j < pointAmount; j++)
                {
                    popPairList.Add(j, GetYValue(coefficient, j));
                }
                //dodaj do listy
                graph.GraphPane.AddCurve(i.ToString(), popPairList, populationColor[i], SymbolType.None);
            }

            if (((BackgroundWorker)sender).CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            //odœwie¿
            RefreshGraph();
        }

        private void CreateGeneration()
        {
            List<string> newPopulationList = new List<string>();
            //TODO wype³niæ
            Random rand = new Random();

            List<double> tablicaBledow = new List<double>();

            for (int k = 0; k < 10; k++)
            {
                var firstPopulation = GetRandomPopulation();
                var secondPopulation = GetRandomPopulation();
                var size = firstPopulation.Length > secondPopulation.Length ? firstPopulation.Length : secondPopulation.Length;

                string[] child = new string[size];
                Double a;
                List<double> coeficients = new List<double>();
                for (int j = 0; j < size; j++)
                {
                    int dzialanie = rand.Next(0, 5);
                    double m;
                    switch (dzialanie)
                    {
                        case 0:
                            a = (firstPopulation[j % firstPopulation.Length] + secondPopulation[j % secondPopulation.Length]) / 2;
                            if ((m = rand.Next()) < 0.1)
                            {
                                a += m;
                            }
                            child[j] = a.ToString();
                            coeficients.Add(a);
                            break;
                        case 1:
                            a = firstPopulation[j % firstPopulation.Length] + secondPopulation[j % secondPopulation.Length];
                            if ((m = rand.Next()) < 0.1)
                            {
                                a += m;
                            }
                            child[j] = a.ToString();
                            coeficients.Add(a);
                            break;
                        case 2:
                            a = 0.01;
                            if ((m = rand.Next()) < 0.1)
                            {
                                a += m;
                            }
                            child[j] = a.ToString();
                            coeficients.Add(a);
                            break;
                        case 3:
                            a = firstPopulation[j % firstPopulation.Length] - secondPopulation[j % secondPopulation.Length];
                            if ((m = rand.Next()) < 0.1)
                            {
                                a += m;
                            }
                            child[j] = a.ToString();
                            coeficients.Add(a);
                            break;
                        case 4:
                            a = firstPopulation[j % firstPopulation.Length];
                            if ((m = rand.Next()) < 0.1)
                            {
                                a += m;
                            }
                            child[j] = a.ToString();
                            coeficients.Add(a);
                            break;
                        case 5:
                            a = secondPopulation[j % secondPopulation.Length];
                            if ((m = rand.Next()) < 0.1)
                            {
                                a += m;
                            }
                            child[j] = a.ToString();
                            coeficients.Add(a);
                            break;

                    }
                }
                double sum = 0;
                for (int i = 0; i < ppl.Count; i++)
                {
                    sum += Math.Abs(GetYValue(coeficients, i) - ppl[i].Y);
                }

                tablicaBledow.Add(sum);

                SortedDictionary<double, String> mapaDoWyboruNajlepszych = new SortedDictionary<double, String>();
                mapaDoWyboruNajlepszych.Add(sum, String.Join(" ", child));

                foreach (KeyValuePair<double, String> pair in mapaDoWyboruNajlepszych)
                {
                    newPopulationList.Add(pair.Value);
                }
            }

            newPopulationList.RemoveRange(6, 4);
            population = newPopulationList;
        }

        /// <summary>
        /// pobieranie wartoœci funkcji dla danego punktu, 
        /// </summary>
        /// <param name="coefficient">lista wspó³czynników funkcji</param>
        /// <param name="j">punkt</param>
        /// <returns></returns>
        private double GetYValue(List<double> coefficient, int j)
        {
            double temp = 0;
            for (int i = 0; i < coefficient.Count; i++)
            {
                temp += Math.Pow(j, coefficient.Count - i) * coefficient[i];
            }
            return temp;
        }

        //odœwie¿anie wykresu
        private void RefreshGraph()
        {
            if (graph.InvokeRequired)
            {
                graph.Invoke(new RefreshGraphDelegate(RefreshGraph));
            }
            else
            {
                graph.AxisChange();
                graph.Invalidate();
            }
        }
        private delegate void RefreshGraphDelegate();

        private void AddColors(List<Color> populationColor)
        {
            populationColor.Add(Color.HotPink);
            populationColor.Add(Color.IndianRed);
            populationColor.Add(Color.Khaki);
            populationColor.Add(Color.LightBlue);
            populationColor.Add(Color.Gray);
            populationColor.Add(Color.Gold);
            populationColor.Add(Color.DarkGreen);
            populationColor.Add(Color.Blue);
            populationColor.Add(Color.DarkSlateBlue);
            populationColor.Add(Color.Orange);
        }

        private void AddPopulation(List<string> population)
        {
            population.Add("0,05");
            population.Add("0,010 0,0014 0,0003 0,0006");
            population.Add("0,004 0,00002 0,000008 0,00000055");
            population.Add("0,00003 0,02 0,008 0,009 0,00003 ");
            population.Add("0,0000000006 0,000000001 0,008 0,009 0,00003 0,55");
            population.Add("0,0000000003 0,0000000002 0,008 0,009 0,00003 0,1 ");
        }

        private double[] GetRandomPopulation()
        {
            return population[rand.Next() % population.Count]
                .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => Convert.ToDouble(x))
                .ToArray();
        }
    }
}