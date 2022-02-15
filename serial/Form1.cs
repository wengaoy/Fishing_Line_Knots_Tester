using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace serial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SerialPort mySerialPort = new SerialPort();
        private bool _continue_reader, _continue_writer;
        private bool portOpened;
        private object COMmsgToWriteLocked = new object();
        private StringBuilder myStringbuilder = new StringBuilder();
        private List<string> COMmsgToWrite = new List<string>();

        private List<string> datacaptured = new List<string>();
        private List<double> Weight_data = new List<double>();



        #region drawing

        private Thread GraphThread;

        // Draw a graph until stopped.
        private void DrawGraph1()
        {
            try
            {                         
                for (; ; )
                {
                    //PlotValue1(0, 0);
                    FishingLineTest_Draw();
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Thread] " + ex.Message);
            }
        }


        // Draw a graph until stopped.
       
        private float P_Width, P_Height;//for pictureBox1
                                        // private static float P_topMargin=10, P_botMargin=10, P_leftMargin=10, P_rightMargin=10;
        private static float P_topMargin = 30, P_botMargin = 30, P_leftMargin = 30, P_rightMargin = 30;
        // Define a delegate type that takes two int parameters.
        private delegate void PlotValueDelegate(int old_y, int new_y);
        private delegate void PlotValueDelegate1(int old_y, int new_y);

        private Font fnt = new Font("Arial", 10);
        private Font fnt_consolas = new Font("Consolas", 14);
        string oldStr = "";

        private int x1, y1, x2, y2;
        private int y_offset = 0;
        private double y_max = 0;
        // Plot a new value.
        private void PlotValue1(int old_y, int new_y)
        {

            //// See if we're on the worker thread and thus
            //// need to invoke the main UI thread.
            //if (this.InvokeRequired)
            //{
            //    // Make arguments for the delegate.
            //    object[] args = new object[] { old_y, new_y };

            //    // Make the delegate.
            //    PlotValueDelegate plot_value_delegate1 = PlotValue1;

            //    // Invoke the delegate on the main UI thread.
            //    this.Invoke(plot_value_delegate1, args);

            //    // We're done.
            //    return;
            //}
            P_Width = pictureBox1.Width;
            P_Height = pictureBox1.Height;

            y_offset = (int)(pictureBox1.Height * 0.2);

            Bitmap bmp;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //if (pictureBox1.Image == null)
            //{
            //    bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //}
            //else
            //{
            //    bmp = (Bitmap)pictureBox1.Image;
            //}

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);

                //draw gride
                // Create a new pen.
                Pen YellowPen = new Pen(Brushes.GreenYellow);//.LightGreen);//.Green);//.Yellow);

                // Set the pen's width.
                YellowPen.Width = 1.0F;
                //YellowPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                // Set the DashCap to round.     
               // YellowPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;



                g.DrawString("Data from ESP32:", fnt, System.Drawing.Brushes.Yellow, new Point(5, 30));
                
                if ( datacaptured.Count > 0)
                {
                    g.DrawString(datacaptured[datacaptured.Count - 1], fnt, System.Drawing.Brushes.Yellow, new Point(120, 30));

                   // g.DrawLine(YellowPen, 0, 100, 200, 120);
                    oldStr = datacaptured[datacaptured.Count-1];
                  //  datacaptured.RemoveAt(0); don't remove, need built a draw list
                }
                else
                {
                    g.DrawString(oldStr, fnt, System.Drawing.Brushes.Yellow, new Point(120, 30));
                }

                //draw line get points from the list datacaptured
                if(datacaptured.Count>2)
                {
                    for (int i = 0; i < datacaptured.Count-1; i++)
                    {
                        x1 = i;
                        if (Convert.ToInt32(datacaptured[i]) < 8388607)
                        {
                            y1 = (pictureBox1.Height - Convert.ToInt32(datacaptured[i]) / 2296) + 200;// y_offset;
                            if(y_max< Convert.ToInt32(datacaptured[i]))
                            {
                                y_max = Convert.ToInt32(datacaptured[i]);
                            }
                        }
                        //y1 = (pictureBox1.Height - Convert.ToInt32(datacaptured[i]) / 2296) + 200;// y_offset;
                        x2 = x1 + 1;

                        if (Convert.ToInt32(datacaptured[i + 1]) < 8388607) //0x7FFFFF)
                        {
                            y2 = (pictureBox1.Height - Convert.ToInt32(datacaptured[i + 1]) / 2296) + 200;// y_offset;
                            if (y_max < Convert.ToInt32(datacaptured[i+1]))
                            {
                                y_max = Convert.ToInt32(datacaptured[i+1]);
                            }
                        }

                      //  y2 = (pictureBox1.Height - Convert.ToInt32(datacaptured[i + 1]) / 2296) + 200;// y_offset;
                        g.DrawLine(YellowPen, x1, y1, x2, y2);

                        g.DrawString("Max:" + y_max.ToString(), fnt, System.Drawing.Brushes.Yellow, new Point(200, 30));
                    }
                    
                    Console.WriteLine("Drawed");
                }
               

             //   g.DrawLine(YellowPen,)

                ////start Horizontal lines 
                //for (int a = 0; a < 11; a++)//11 lines
                //{
                //    //grayPen.DashPattern = new float[] { 1.0F, 9.0F, 1.0F, 9.0F };//dot 1, space 9, total 10. 500W, 50dot/div.
                //    grayPen.DashPattern = new float[] { 1.0F, 3.0F };
                //    // g.DrawLine(grayPen, 0.0F, (48.25F * (float)a), P_Width, (48.25F * (float)a));\
                //    float H_div = (P_Height - P_topMargin - P_botMargin) / 10.0F;
                //    g.DrawLine(grayPen, P_leftMargin, (H_div * (float)a + P_topMargin), P_Width - P_rightMargin, (H_div * (float)a) + P_topMargin);
                //}

                ////then vertical lines
                //for (int b = 0; b < 11; b++)//11 lines, 10 div
                //{
                //    //grayPen.DashPattern = new float[] { 1.0F, 8.65F, 1.0F, 8.65F };//dot 1, space 9, total 10. 500W, 50dot/div.
                //    //g.DrawLine(grayPen, ((P_Width / 10) * (float)b), 0.0F, ((P_Width / 10) * (float)b), P_Height);
                //    // grayPen.DashPattern = new float[] { 1.0F, 9.0F, 1.0F, 9.0F };//dot 1, space 9, total 10. 500W, 50dot/div.
                //    grayPen.DashPattern = new float[] { 1.0F, 3.0F };
                //    float V_div = (P_Width - P_leftMargin - P_rightMargin) / 10.0F;

                //    g.DrawLine(grayPen, (V_div * (float)b + P_leftMargin), P_topMargin, (V_div * (float)b + P_leftMargin), P_Height - P_botMargin);
                //}

                ////draw data
                //Pen pen_CH1 = new Pen(Color.Green);
                //Pen pen_CH2 = new Pen(Color.Yellow);
                //Pen pen_CH1_FFT = new Pen(Color.Red);
                //Pen pen_CH2_FFT = new Pen(Color.Purple);

                //float x1, x2, y1, y2;
                //float xf1, xf2, yf1, yf2;//fft

                ////float Hscale = (float)Pinned[0].Target.Length / (float)pictureBox1.Width;
                //float Hscale = (float)Pinned[0].Target.Length / (float)(pictureBox1.Width - P_leftMargin - P_rightMargin);

                //float Box_width = (float)(pictureBox1.Width - P_leftMargin - P_rightMargin);
                //float Box_height = (float)(P_Height - P_topMargin - P_botMargin);
                //float Yscale = (float)(P_Height - P_topMargin - P_botMargin) / 2;

                //if (timeDomain_mode)
                //{
                //    //CHA
                //    if (CHA_enabled == 1)
                //    {
                //        for (int iPixel = 0; iPixel < Pinned[0].Target.Length - 1; iPixel++)
                //        {

                //            // float Hscale = (float)(Pinned[0].Target.Length / pictureBox1.Width);
                //            //x1 = (float)iPixel / Hscale + P_leftMargin;
                //            //y1 = (float)(((adc_to_mv(Pinned[0].Target[iPixel], (int)_channelSettings[0].range)) / (float)(inputRanges[(int)(_channelSettings[0].range)]) * (P_Height / 2) / 2));
                //            //y1 = y1 + (float)(P_Height * (float)(Convert.ToDouble(CHA_pos.Value) / 100));
                //            //x2 = (float)(iPixel + 1) / Hscale + P_leftMargin;
                //            // y2 = (float)(((adc_to_mv(Pinned[0].Target[iPixel + 1], (int)_channelSettings[0].range)) / (float)(inputRanges[(int)(_channelSettings[0].range)]) * (P_Height / 2) /2));
                //            //y2 = y2 + (float)(P_Height * (float)(Convert.ToDouble(CHA_pos.Value) / 100));
                //            // g.DrawLine(pen_CH1, x1, ((P_Height / 2) - y1), x2, ((P_Height / 2) - y2));

                //            x1 = (float)iPixel / Hscale + P_leftMargin;
                //            y1 = P_Height / 2 - (adc_to_mv(Pinned[0].Target[iPixel], (int)_channelSettings[0].range) / inputRanges[(int)(_channelSettings[0].range)]) * Yscale;
                //            y1 = y1 + (float)(Box_height * (float)(Convert.ToDouble(CHA_pos.Value) / 100));
                //            x2 = (float)(iPixel + 1) / Hscale + P_leftMargin;
                //            y2 = P_Height / 2 - (adc_to_mv(Pinned[0].Target[iPixel + 1], (int)_channelSettings[0].range) / inputRanges[(int)(_channelSettings[0].range)]) * Yscale;
                //            y2 = y2 + (float)(Box_height * (float)(Convert.ToDouble(CHA_pos.Value) / 100));
                //            g.DrawLine(pen_CH1, x1, y1, x2, y2);

                //        }
                //    }


                //    //CHB
                //    if (CHB_enabled == 1)
                //    {

                //        for (int iPixel = 0; iPixel < Pinned[1].Target.Length - 1; iPixel++)
                //        {
                //            //x1 = (float)iPixel / Hscale + P_leftMargin;
                //            //y1 = (float)(((adc_to_mv(Pinned[1].Target[iPixel], (int)_channelSettings[1].range)) / (float)(inputRanges[(int)(_channelSettings[1].range)]) * (P_Height / 2) / 2));
                //            //y1 = y1 + (float)(P_Height * (float)(Convert.ToDouble(CHB_pos.Value) / 100));

                //            //x2 = (float)(iPixel + 1) / Hscale + P_leftMargin;
                //            //y2 = (float)(((adc_to_mv(Pinned[1].Target[iPixel + 1], (int)_channelSettings[1].range)) / (float)(inputRanges[(int)(_channelSettings[1].range)]) * (P_Height / 2) / 2));
                //            //y2 = y2 + (float)(P_Height * (float)(Convert.ToDouble(CHB_pos.Value) / 100));
                //            //g.DrawLine(pen_CH2, x1, ((P_Height / 2) - y1), x2, ((P_Height / 2) - y2));
                //            x1 = (float)iPixel / Hscale + P_leftMargin;
                //            y1 = P_Height / 2 - (adc_to_mv(Pinned[1].Target[iPixel], (int)_channelSettings[1].range) / inputRanges[(int)(_channelSettings[1].range)]) * Yscale;
                //            y1 = y1 + (float)(Box_height * (float)(Convert.ToDouble(CHB_pos.Value) / 100));
                //            x2 = (float)(iPixel + 1) / Hscale + P_leftMargin;
                //            y2 = P_Height / 2 - (adc_to_mv(Pinned[1].Target[iPixel + 1], (int)_channelSettings[1].range) / inputRanges[(int)(_channelSettings[1].range)]) * Yscale;
                //            y2 = y2 + (float)(Box_height * (float)(Convert.ToDouble(CHB_pos.Value) / 100));
                //            g.DrawLine(pen_CH2, x1, y1, x2, y2);

                //        }

                //    }
                //}


                ////show waveform info            
                //CH1Vpp = adc_to_mv((int)Pinned[0].Target.Max(), (int)_channelSettings[0].range) - adc_to_mv((int)Pinned[0].Target.Min(), (int)_channelSettings[0].range);
                //CH2Vpp = adc_to_mv((int)Pinned[1].Target.Max(), (int)_channelSettings[1].range) - adc_to_mv((int)Pinned[1].Target.Min(), (int)_channelSettings[1].range);
                //lblInfo.Text = "CH1Vp-p: " + CH1Vpp.ToString() + " mV" + "\r\n";
                //lblCH2Vpp.Text = "CH2Vp-p: " + CH2Vpp.ToString() + " mV" + "\r\n";

                //CH1Vrms = (float)getChRMS2(Pinned[0].Target, (int)_channelSettings[0].range);
                //lblCH1rms.Text = "CH1Vrms: " + CH1Vrms.ToString() + " mV" + "\r\n";
                //CH2Vrms = (float)getChRMS2(Pinned[1].Target, (int)_channelSettings[1].range);
                //lblCH2rms.Text = "CH2Vrms: " + CH2Vrms.ToString() + " mV" + "\r\n";


                //txtinfo.Text = "sampleCount: " + sampleCount.ToString() + "\r\n" +
                //                "intervel: " + timeInterval.ToString() + " ns" + "\r\n" +
                //                "timeIndisposed: " + timeIndisposed.ToString();

                ////Get Freq
                //CH1Freq = getChFreq1(Pinned[0].Target);
                //CH2Freq = getChFreq1(Pinned[1].Target);
                //FreqUnit(CH1Freq, CH2Freq);

                ////FFT

                //System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
                //System.Drawing.SolidBrush drawBrush1 = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                //System.Drawing.SolidBrush drawBrush2 = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
                //if (FFT_mode)
                //{
                //    try
                //    {
                //        processFFT1();
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message + " FFT handle error!");
                //    }


                //    float FFT_Hscale = (float)(CH1freqBinIndex.Length / 2) / (float)(pictureBox1.Width - P_leftMargin - P_rightMargin);

                //    if (CHA_enabled == 1)
                //    {
                //        for (int iPixel = 0; iPixel < CH1freqBinIndex.Length / 2; iPixel++)
                //        {

                //            xf1 = (float)iPixel / FFT_Hscale + P_leftMargin;
                //            // yf1 = (float)(((CH1FFTlogDrawData[iPixel] - 10 ) / (-120.0)) * (P_Height));
                //            yf1 = (float)(((CH1FFTlogDrawData[iPixel] - 10) / (-120.0)) * (P_Height - P_topMargin - P_botMargin)) + (float)CHA_FFT_pos.Value;

                //            xf2 = (float)(iPixel + 1) / FFT_Hscale + P_leftMargin;
                //            //yf2 = (float)(((CH1FFTlogDrawData[iPixel + 1] - 10 ) / (-120.0)) * (P_Height));
                //            yf2 = (float)(((CH1FFTlogDrawData[iPixel + 1] - 10) / (-120.0)) * (P_Height - P_topMargin - P_botMargin)) + (float)CHA_FFT_pos.Value;

                //            g.DrawLine(pen_CH1_FFT, xf1, yf1, xf2, yf2);

                //        }



                //        float x = P_Width * 10 / 15;
                //        float y = P_Height / 10;
                //        string drawStr1 = "CH1 THD: " + THD1 + "%";
                //        g.DrawString(drawStr1, drawFont, drawBrush1, x, y);
                //        g.DrawString("Freq_CH1: " + F1_fundamental + " Hz", drawFont, drawBrush1, x, y + 20);

                //    }
                //    if (CHB_enabled == 1)
                //    {
                //        try
                //        {
                //            for (int iPixel = 0; iPixel < CH2freqBinIndex.Length / 2; iPixel++)
                //            {

                //                xf1 = (float)iPixel / FFT_Hscale + P_leftMargin;
                //                // yf1 = (float)(((CH1FFTlogDrawData[iPixel] - 10 ) / (-120.0)) * (P_Height));
                //                yf1 = (float)(((CH2FFTlogDrawData[iPixel] - 10) / (-120.0)) * (P_Height - P_topMargin - P_botMargin)) + (float)CHB_FFT_pos.Value;

                //                xf2 = (float)(iPixel + 1) / FFT_Hscale + P_leftMargin;
                //                //yf2 = (float)(((CH1FFTlogDrawData[iPixel + 1] - 10 ) / (-120.0)) * (P_Height));
                //                yf2 = (float)(((CH2FFTlogDrawData[iPixel + 1] - 10) / (-120.0)) * (P_Height - P_topMargin - P_botMargin)) + (float)CHB_FFT_pos.Value;

                //                g.DrawLine(pen_CH2_FFT, xf1, yf1, xf2, yf2);

                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //            Console.WriteLine(ex.Message);
                //        }

                //        float x = P_Width * 10 / 15;
                //        float y = P_Height / 10;
                //        string drawStr2 = "CH2 THD: " + THD2 + "%";
                //        g.DrawString(drawStr2, drawFont, drawBrush2, x, y + 40);
                //        g.DrawString("Freq_CH2: " + F2_fundamental + " Hz", drawFont, drawBrush2, x, y + 60);

                //    }


                //}


                // the following is caculate RMS from FFT data
                //float testRms=0;
                //    for(int b=0;b<CH1FFTDrawData.Length;b++)
                //    {
                //        testRms = testRms + (float)Math.Pow(CH1FFTDrawData[b],2);
                //    }
                //    testRms = (float)Math.Sqrt(testRms);


                YellowPen.Dispose();
             
                pictureBox1.Image = bmp;
              
            }
        }

        //for fishing line test
        private void FishingLineTest_Draw()
        {
           
            P_Width = pictureBox1.Width;
            P_Height = pictureBox1.Height;
            float V_div = (P_Width - P_leftMargin - P_rightMargin) / 10.0F;
            y_offset = (int)(pictureBox1.Height * 0.2);

            Bitmap bmp;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);


            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Black);

           
                // Create a new pen.
                Pen YellowPen = new Pen(Brushes.GreenYellow);//.LightGreen);//.Green);//.Yellow);

                // Set the pen's width.
                YellowPen.Width = 1.0F;
             

                // Create a new pen.
                Pen grayPen = new Pen(Brushes.LightGray);

                // Set the pen's width.
                grayPen.Width = 1.0F;
                grayPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

                // Set the DashCap to round.     
                grayPen.DashCap = System.Drawing.Drawing2D.DashCap.Round;

                //draw grids
                //start Horizontal lines 
                for (int a = 0; a < 11; a++)//11 lines
                {
                    //grayPen.DashPattern = new float[] { 1.0F, 9.0F, 1.0F, 9.0F };//dot 1, space 9, total 10. 500W, 50dot/div.
                    grayPen.DashPattern = new float[] { 1.0F, 3.0F };
                    // g.DrawLine(grayPen, 0.0F, (48.25F * (float)a), P_Width, (48.25F * (float)a));\
                    float H_div = (P_Height - P_topMargin - P_botMargin) / 10.0F;
                    g.DrawLine(grayPen, P_leftMargin, (H_div * (float)a + P_topMargin), P_Width - P_rightMargin, (H_div * (float)a) + P_topMargin);
                }

                //then vertical lines
                for (int b = 0; b < 11; b++)//11 lines, 10 div
                {
                    //grayPen.DashPattern = new float[] { 1.0F, 8.65F, 1.0F, 8.65F };//dot 1, space 9, total 10. 500W, 50dot/div.
                    //g.DrawLine(grayPen, ((P_Width / 10) * (float)b), 0.0F, ((P_Width / 10) * (float)b), P_Height);
                    // grayPen.DashPattern = new float[] { 1.0F, 9.0F, 1.0F, 9.0F };//dot 1, space 9, total 10. 500W, 50dot/div.
                    grayPen.DashPattern = new float[] { 1.0F, 3.0F };
                    //float V_div = (P_Width - P_leftMargin - P_rightMargin) / 10.0F;

                    g.DrawLine(grayPen, (V_div * (float)b + P_leftMargin), P_topMargin, (V_div * (float)b + P_leftMargin), P_Height - P_botMargin);

                    //draw a lbs mark
                    float scaleFact = (pictureBox1.Height - P_topMargin - P_botMargin) / 10;
                    g.DrawString((b*4).ToString(), fnt, System.Drawing.Brushes.Gray, new Point(10, (int)(pictureBox1.Height - P_topMargin - b* scaleFact) - 10 ));//4 lbs pre div
                }
                //end draw grids



               // g.DrawString("Data from ESP32:", fnt, System.Drawing.Brushes.Yellow, new Point(30, 1));//string 1

                //if (datacaptured.Count > 0)
                //{
                //    g.DrawString("Data from ESP32:" + datacaptured[datacaptured.Count - 1], fnt, System.Drawing.Brushes.Yellow, new Point(30, 5));//string 2                
                //    oldStr = datacaptured[datacaptured.Count - 1];
                //    //  datacaptured.RemoveAt(0); don't remove, need built a draw list
                //}
                //else
                //{
                //    g.DrawString(oldStr, fnt, System.Drawing.Brushes.Yellow, new Point(145, 1));
                //}
                //display current lbs
                if (Weight_data.Count > 0)
                {
                    g.DrawString("Current:" + Weight_data[Weight_data.Count - 1].ToString() +" lbs", fnt_consolas, System.Drawing.Brushes.LawnGreen, new Point(30, 5));//string 2                
                    oldStr = Weight_data[Weight_data.Count - 1].ToString();
                    //  datacaptured.RemoveAt(0); don't remove, need built a draw list
                }
                else
                {
                    g.DrawString(oldStr, fnt_consolas, System.Drawing.Brushes.LawnGreen, new Point(145, 1));
                }

                //display lbs lines and Max lbs
             
                if (Weight_data.Count > 2)
                {
                    for (int i = 0; i < Weight_data.Count - 1; i++)
                    {
                        x1 = i+30;//P_leftMargin=30
                        if (Weight_data[i] < 40)//40 lbs max
                        {
                          
                            y1 = (int)(pictureBox1.Height - P_topMargin  - Weight_data[i] * (pictureBox1.Height - P_topMargin - P_botMargin)/40);
                            if (y_max < (int)(Weight_data[i]))
                            {
                                y_max = Weight_data[i];
                            }
                        }
                    
                        x2 = x1 + 1;

                        if (Weight_data[i+1] < 40)//40 lbs max
                        {                           
                            y2 = (int)(pictureBox1.Height - P_topMargin - Weight_data[i + 1] * (pictureBox1.Height - P_topMargin - P_botMargin) / 40);
                            if (y_max < (int)(Weight_data[i+1]))
                            {
                                y_max = Weight_data[i+1];
                              
                            }
                            g.DrawString("Max:" + y_max.ToString() + " lbs", fnt_consolas, System.Drawing.Brushes.Red, new Point(250, 5));//string 3
                        }

                        g.DrawLine(YellowPen, x1, y1, x2, y2);

                      //  g.DrawString("Max:" + y_max.ToString() + " lbs", fnt_consolas, System.Drawing.Brushes.Red, new Point(250, 1));//string 3
                    }

                    Console.WriteLine("Drawed");
                }

              
            
                YellowPen.Dispose();
                grayPen.Dispose();
                pictureBox1.Image = bmp;
            }
        }
        #endregion


        private void tsbtnCalibrationReading_Click(object sender, EventArgs e)
        {
            int average = 0;


            if(!portOpened)
            {
                MessageBox.Show("Connect COM Port First!");
                return;
            }

            //CLEAR OLD DATA
            datacaptured.Clear();

            //then try to get 30 reading
            while (datacaptured.Count < 30)
            {
                Thread.Sleep(1000);
            }

            for (int i = 0; i < 30; i++)
            {
                average = average + Convert.ToInt32(datacaptured[i]);
            }

            average = average / 30;
            lblCal_average.Text = average.ToString();
         //   Console.WriteLine("Average: " + average);
        }

        private void tsbtnTare_Click(object sender, EventArgs e)
        {
            try 
            {
                Properties.Settings.Default.ZeroAdc = Convert.ToInt32(lblCal_average.Text);
                Properties.Settings.Default.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show("You need get 30 times calibration reading first!\r\n" + "Click the button on the right cornor to calibraion.\r\n" + ex.Message );
            }
          
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           lblSavedZeroADC.Text = Properties.Settings.Default.ZeroAdc.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

          if(portOpened)
            {
                COMmsgToWrite.Add("A:6,0\r");//stop the motor
            }
            

          if (GraphThread != null)
            {
                GraphThread.Abort();
                GraphThread = null;
            }
         
            closeport();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            int waitTime = 3;

            if (portOpened)
            {
                COMmsgToWrite.Add("A:6,0\r");//stop the motor
            }

            while (COMmsgToWrite.Count != 0 | waitTime==0)
            {
                Thread.Sleep(500);
                waitTime--;
            }

            closeport();
            Thread.Sleep(1000);

            if(GraphThread!=null)
            {
                GraphThread.Abort();
                GraphThread = null;
            }
            
            Application.Exit();
        }

        private void closeport()
        {    
            try
            {
                if (mySerialPort.IsOpen)
                {
                    mySerialPort.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("mySerialPort.Close()" + ex.Message);
            }

            _continue_reader = false;
            _continue_writer = false;
            portOpened = false;         
            lblPort.Text = "Port Closed";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!portOpened)
            {
                MessageBox.Show("Connect COM Port First!");
                return;
            }

            datacaptured.Clear();
            Weight_data.Clear();
            x1 = x2 = y1 = y2 = 0;
            y_max = 0;

            if (GraphThread == null) //Thread not start yet and Device was opened
            {
                Console.WriteLine("Starting capture thread...");


                GraphThread = new Thread(DrawGraph1);
                GraphThread.Priority = ThreadPriority.BelowNormal;
                GraphThread.IsBackground = true;
                GraphThread.Start();
                //tslblcaptureStatus.Text = "Started";
                tsbtnStart_Stop.Image = Properties.Resources.stop48;
                COMmsgToWrite.Add("A:6,1\r");//start the motor

                tsbtnCalibrationReading.Enabled = false;
            }
            else
            {
                Console.WriteLine("Stop capture thread...");
                GraphThread.Abort();
                GraphThread = null;
               // tslblcaptureStatus.Text = "Stoped";
                tsbtnStart_Stop.Image = Properties.Resources.play48;
                COMmsgToWrite.Add("A:6,0\r");//stop the motor

                tsbtnCalibrationReading.Enabled = true;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            
        }

        private void openport()
        {
            //tmrPinStatus.Start();
            try
            {
                if (tscbCOM.SelectedItem == null)
                {
                    MessageBox.Show("Please select a COM Port");
                    return;
                }
                else
                {
                    mySerialPort.PortName = tscbCOM.SelectedItem.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            mySerialPort.BaudRate = Convert.ToInt32(cboBDRate.Text);
            mySerialPort.Parity = Parity.None;
            mySerialPort.DataBits = 8;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.Handshake = Handshake.None;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            try
            {
                mySerialPort.Open();
                _continue_reader = true;
                _continue_writer = true;
                portOpened = true;

                Thread tWriter = new Thread(COMmsgWriteThread);
                tWriter.Name = @"Write COM Port Threader!";
                try
                {
                    tWriter.Start();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

                lblPort.Text = "Ports: " + mySerialPort.PortName + ": " + mySerialPort.BaudRate.ToString() + "-" + mySerialPort.DataBits.ToString()
                              + "-" + mySerialPort.Parity.ToString() + "-" + mySerialPort.StopBits.ToString() + "-" + mySerialPort.Handshake.ToString()
                              + " Opened";
            }
            catch (Exception ex1)
            {
               
                Console.WriteLine(ex1.Message);
            }

            Console.WriteLine("COM port opened");
        }

        public void COMmsgWriteThread()
        {
            while (_continue_writer)
            {
                try
                {
                    lock (COMmsgToWriteLocked)
                    {
                        if (COMmsgToWrite.Count() > 0)
                        {
                      
                            mySerialPort.Write(COMmsgToWrite[0]);
                            COMmsgToWrite.RemoveAt(0);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Thread.Sleep(5);//reduce CPU usage
            }
            
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            int wData = 0;
            double actualW = 0.0;
            try
            {
                       
                    myStringbuilder.Append(mySerialPort.ReadExisting());
                    myStringbuilder = myStringbuilder.Replace("\r", "").Replace("\n","");      
                
                if(myStringbuilder!=null)
                {
                    datacaptured.Add(myStringbuilder.ToString());
                    //convert to actual weight
                    try 
                    {
                        wData = Convert.ToInt32(myStringbuilder.ToString());
                        if(wData<639650)//0~1.4 lbs
                        {
                            actualW = (double)((wData - Properties.Settings.Default.ZeroAdc) / 46856 + 0.0);
                        }
                        else if(wData<1052035 & wData>639650)//1.4~10.2 lbs
                        {
                            actualW = (double)((wData - 639650) / 46915 + 1.4125);
                        }
                        else if(wData>1052035 & wData<2460475)//10.2~40lbs
                        {
                            actualW = (double)((wData - 1052035) / 46648 + 10.2);
                        }
                        else if(wData>2460475)
                        {
                            Console.WriteLine("overflow");
                        }
                        Weight_data.Add(actualW);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                // SetText(myStringbuilder.ToString());
                Console.WriteLine(datacaptured.Count);
               // if(datacaptured.Count>1500)//when drawing make sure in the box
                if (datacaptured.Count == P_Width-P_leftMargin-P_rightMargin)//when drawing make sure in the box
                {
                    datacaptured.Clear();
                    Weight_data.Clear();
                }
                 Console.WriteLine(myStringbuilder.ToString());
                    myStringbuilder.Length = 0;
                    myStringbuilder.Capacity = 0;
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("DataReceivedHandler error: " + ex.Message);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int portSelectIndex = 0, buadrateSelectIndex = 0;
            
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string i in ports)
            {
                tscbCOM.Items.Add(i);                              
            }
            //find saved com index, 0 for the first time
            for(int i=0; i< tscbCOM.Items.Count; i++)
            {
                if(tscbCOM.Items[i].ToString() == Properties.Settings.Default.savedPortName)
                {
                    portSelectIndex = i;
                }             
            }
            tscbCOM.SelectedIndex = portSelectIndex;

            for(int i=0;i< cboBDRate.Items.Count; i++)
            {
                if(cboBDRate.Items[i].ToString()==Properties.Settings.Default.saveBuadRateNmae)
                {
                    buadrateSelectIndex=i;
                }
            }

            cboBDRate.SelectedIndex = buadrateSelectIndex;


        }

        private void btnconn_Click(object sender, EventArgs e)
        {
            if(btnconn.Text=="Disconnected")
            {
                openport();
                btnconn.Text = "Connected";
                btnconn.Image = Properties.Resources.connected_48;
            }
            else if (btnconn.Text=="Connected")
            {
                closeport();
                btnconn.Text = "Disconnected";
                btnconn.Image = Properties.Resources.disconnected_48;
            }

            Properties.Settings.Default.savedPortName = tscbCOM.SelectedItem.ToString();
            Properties.Settings.Default.saveBuadRateNmae=cboBDRate.SelectedItem.ToString();
            Properties.Settings.Default.Save();

        }
    }
}
