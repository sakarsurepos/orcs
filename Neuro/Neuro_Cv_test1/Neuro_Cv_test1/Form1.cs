using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NeuronDotNet.Core; //
using NeuronDotNet.Core.Backpropagation;  //

namespace Neuro_Cv_test1
{
    public partial class Form1 : Form
    {

        //Neuro
        private BackpropagationNetwork xorNetwork; //
        private double[] errorList; // 
        private int cycles = 5000;  //
        private int neuronCount = 3; // 
        private double learningRate = 0.25d; //

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorList = new double[cycles]; //

            LinearLayer inputLayer = new LinearLayer(2); //
            SigmoidLayer hiddenLayer = new SigmoidLayer(neuronCount); //
            SigmoidLayer outputLayer = new SigmoidLayer(1); //
            new BackpropagationConnector(inputLayer, hiddenLayer); //
            new BackpropagationConnector(hiddenLayer, outputLayer); //
            xorNetwork = new BackpropagationNetwork(inputLayer, outputLayer); //
            xorNetwork.SetLearningRate(learningRate); //

            TrainingSet trainingSet = new TrainingSet(2, 1);
            trainingSet.Add(new TrainingSample(new double[2] { 0d, 0d }, new double[1] { 0d }));
            trainingSet.Add(new TrainingSample(new double[2] { 0d, 1d }, new double[1] { 1d }));
            trainingSet.Add(new TrainingSample(new double[2] { 1d, 0d }, new double[1] { 1d }));
            trainingSet.Add(new TrainingSample(new double[2] { 1d, 1d }, new double[1] { 0d }));

            double max = 0d;

            //Ucenie
			xorNetwork.Learn(trainingSet, cycles);
			double[] indices = new double[cycles];
			for (int i = 0; i < cycles; i++) { indices[i] = i; }
            MessageBox.Show("Training Finish");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (xorNetwork != null)
            {
                //TEST 
                lblTestOutput.Text = xorNetwork.Run(new double[] { double.Parse(txtTestInput1.Text), double.Parse(txtTestInput2.Text) })[0].ToString("0.000000");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp1 = new Bitmap(pictureBox2.ClientSize.Width, pictureBox2.ClientSize.Height);
            Rectangle rect1 = new Rectangle(0, 0, 20, 20);

            Bitmap DrawArea;
            DrawArea = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
            pictureBox2.Image = DrawArea;
            Graphics g;
            g = Graphics.FromImage(DrawArea);
            g.FillRectangle(Brushes.Black, 5, 5, 10, 10);
            pictureBox2.DrawToBitmap(bmp1, pictureBox2.ClientRectangle);

            Color pixelColor = bmp1.GetPixel(8, 8);
            if (pixelColor.Name.ToString() == "ffffffff") MessageBox.Show("White");
            if (pixelColor.Name.ToString() == "ff000000") MessageBox.Show("Black");
            bmp1.Save("C:\\test.png", ImageFormat.Png);
            int test;
        }
    }
}
