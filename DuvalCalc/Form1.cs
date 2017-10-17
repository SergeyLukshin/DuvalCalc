using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DuvalCalc
{
    public partial class Form1 : Form
    {
        class FigureInfo
        {
            public FigureInfo(PointF p1_, PointF p2_, PointF pName_, string strName_)
            {
                p1 = p1_;
                p2 = p2_;
                pName = pName_;
                strName = strName_;
            }

            public PointF p1;
            public PointF p2;
            public PointF pName;
            public string strName;
        };

        class PentagonPartInfo
        {
            public List<PointF> listPoints = new List<PointF>();
            public string strName;
        }

        List<FigureInfo> listTriangle = new List<FigureInfo>();
        List<FigureInfo> listPentagon = new List<FigureInfo>();
        List<FigureInfo> listTriangleEx = new List<FigureInfo>();
        List<float> listNormogramValues = new List<float>();
        List<List<KeyValuePair<PointF, PointF>>> listTriangleModels = new List<List<KeyValuePair<PointF, PointF>>>();
        List<List<PentagonPartInfo>> listPentagonModels = new List<List<PentagonPartInfo>>();
        PointF ptRes = new PointF(-1, -1);
        bool bPressDown = false;
        bool bUpdateValue = false;
        float triangle_zoom = 4;
        float pentagon_zoom = 4.3f;
        float triangle_shift_x = 0;
        float triangle_shift_y = 0;
        float pentagon_shift_x = 0;
        float pentagon_shift_y = 0;
        float DpiXRel = 0;
        float DpiYRel = 0;
        int m_iSelectNormogramIndex = -1;
        Point m_oldMousePoint = new Point(0, 0);

        Bitmap bmp_pb;
        Bitmap bmp_pb_normogram;

        bool bPaintValueNormogram = false;
        string strSeparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;



        public Form1()
        {
            InitializeComponent();
        }

        private void DrawModel(Graphics gr, int type)
        {
            Pen pen = new Pen(Color.Black);
            //Pen pen_b = new Pen(Color.Blue);
            Pen pen_g = new Pen(Color.LightGray);
            pen_g.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            if (cbMethod.SelectedIndex <= 9)
            {
                for (int i = 0; i < listTriangleEx.Count; i++)
                {
                    gr.DrawLine(pen_g, listTriangleEx[i].p1, listTriangleEx[i].p2);
                }

                for (int i = 0; i < listTriangle.Count; i++)
                {
                    gr.DrawLine(pen, listTriangle[i].p1, listTriangle[i].p2);
                    gr.DrawString(listTriangle[i].strName, new Font("Times New Roman", 12), new SolidBrush(Color.Black), listTriangle[i].pName);
                }

                for (int i = 0; i < listTriangleModels[type].Count; i++)
                {
                    gr.DrawLine(pen, listTriangleModels[type][i].Key, listTriangleModels[type][i].Value);
                }
            }
            else
            {
                for (int i = 0; i < listPentagon.Count; i++)
                {
                    gr.DrawString(listPentagon[i].strName, new Font("Times New Roman", 12), new SolidBrush(Color.Black), listPentagon[i].pName);
                }

                for (int i = 0; i < listPentagonModels[type - 10].Count; i++)
                {
                    for (int j = 0; j < listPentagonModels[type - 10][i].listPoints.Count; j++)
                    {
                        if (j < listPentagonModels[type - 10][i].listPoints.Count - 1)
                            gr.DrawLine(pen, listPentagonModels[type - 10][i].listPoints[j], listPentagonModels[type - 10][i].listPoints[j + 1]);
                        else
                            gr.DrawLine(pen, listPentagonModels[type - 10][i].listPoints[j], listPentagonModels[type - 10][i].listPoints[0]);
                    }
                }
            }

            if (ptRes.X >= 0)
            {
                gr.FillEllipse(new SolidBrush(Color.Red), ptRes.X - 3, ptRes.Y - 3, 6, 6);
            }
        }

        private void DrawOXY(Graphics gr)
        {
            Pen pen = new Pen(Color.Black);
            Pen pen_b = new Pen(Color.Blue);
            Pen pen_g = new Pen(Color.LightGray);
            pen_g.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            float x_beg = 40 * DpiXRel;
            float y_beg = 40 * DpiYRel;
            float width = pbNormogram.Width - x_beg * 2;
            float height = pbNormogram.Height - y_beg * 2;
            float radius = 3 * DpiXRel;
            float x_diff = 5 * DpiXRel;
            float y_diff = 5 * DpiYRel;

            SizeF s;
            for (int i = 1; i <= 5; i++)
            {
                gr.DrawLine(pen_g, new PointF(x_beg + i * width / 5, y_beg), new PointF(x_beg + i * width / 5, y_beg + height + y_diff));
                switch(i)
                {
                    case 1:
                        s = gr.MeasureString("H2", new Font("Times New Roman", 12));
                        gr.DrawString("H2", new Font("Times New Roman", 12), new SolidBrush(Color.Black), new PointF(x_beg + i * width / 5 - width / 10 - s.Width / 2, y_beg + height + y_diff));
                        break;
                    case 2:
                        s = gr.MeasureString("CH4", new Font("Times New Roman", 12));
                        gr.DrawString("CH4", new Font("Times New Roman", 12), new SolidBrush(Color.Black), new PointF(x_beg + i * width / 5 - width / 10 - s.Width / 2, y_beg + height + y_diff));
                        break;
                    case 3:
                        s = gr.MeasureString("C2H6", new Font("Times New Roman", 12));
                        gr.DrawString("C2H6", new Font("Times New Roman", 12), new SolidBrush(Color.Black), new PointF(x_beg + i * width / 5 - width / 10 - s.Width / 2, y_beg + height + y_diff));
                        break;
                    case 4:
                        s = gr.MeasureString("C2H4", new Font("Times New Roman", 12));
                        gr.DrawString("C2H4", new Font("Times New Roman", 12), new SolidBrush(Color.Black), new PointF(x_beg + i * width / 5 - width / 10 - s.Width / 2, y_beg + height + y_diff));
                        break;
                    case 5:
                        s = gr.MeasureString("C2H2", new Font("Times New Roman", 12));
                        gr.DrawString("C2H2", new Font("Times New Roman", 12), new SolidBrush(Color.Black), new PointF(x_beg + i * width / 5 - width / 10 - s.Width / 2, y_beg + height + y_diff));
                        break;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                s = gr.MeasureString(((10 - i) / 10.0).ToString("0.0"), new Font("Times New Roman", 12));
                gr.DrawLine(pen_g, new PointF(x_beg - x_diff, y_beg + i * height / 10), new PointF(x_beg + width, y_beg + i * height / 10));
                gr.DrawString(((10 - i) / 10.0).ToString("0.0"), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new PointF(x_beg - x_diff - s.Width, y_beg + i * height / 10 - s.Height / 2));
            }

            for (int i = 0; i < listNormogramValues.Count; i++)
            {
                float y = listNormogramValues[i];
                y *= 100;
                y = height - height * y / 100;

                if (i < listNormogramValues.Count - 1)
                {
                    float y_ = listNormogramValues[i + 1];
                    y_ *= 100;
                    y_ = height - height * y_ / 100;

                    gr.DrawLine(pen_b, new PointF(x_beg + (i + 1) * width / 5 - width / 10, y_beg + y), new PointF(x_beg + (i + 1 + 1) * width / 5 - width / 10, y_beg + y_));
                }

                s = gr.MeasureString(listNormogramValues[i].ToString("0.000"), new Font("Times New Roman", 10));
                if (i == m_iSelectNormogramIndex)
                    gr.FillEllipse(new SolidBrush(Color.Red), x_beg + (i + 1) * width / 5 - width / 10 - radius, y_beg + y - radius, 2 * radius, 2 * radius);
                else
                    gr.FillEllipse(new SolidBrush(Color.Green), x_beg + (i + 1) * width / 5 - width / 10 - radius, y_beg + y - radius, 2 * radius, 2 * radius);
                gr.DrawString(listNormogramValues[i].ToString("0.000"), new Font("Times New Roman", 10), new SolidBrush(Color.Black), new PointF(x_beg + (i + 1) * width / 5 - width / 10 - s.Width / 2, y_beg + y - s.Height));
            }

            gr.DrawLine(pen, new PointF(x_beg - x_diff, y_beg + height), new PointF(x_beg + width, y_beg + height));
            gr.DrawLine(pen, new PointF(x_beg, y_beg), new PointF(x_beg, y_beg + height + y_diff));
        }

        private void pbNormogram_Paint()
        {
            Graphics gr = Graphics.FromImage(bmp_pb_normogram);// e.Graphics;
            gr.FillRectangle(new SolidBrush(Color.White), pbNormogram.ClientRectangle);
            DrawOXY(gr);
            pbNormogram.Invalidate();
        }

        private void pb_Paint()
        {
            Graphics gr = Graphics.FromImage(bmp_pb);// e.Graphics;
            gr.FillRectangle(new SolidBrush(Color.White), pb.ClientRectangle);

            DrawModel(gr, cbMethod.SelectedIndex);
            pb.Invalidate();
            //e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Red)), 10, 10, 40, 40);
        }

        private float COS (float degree)
        {
            return (float)Math.Cos(degree * Math.PI / 180);
        }

        private float SIN (float degree)
        {
            return (float)Math.Sin(degree * Math.PI / 180);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp_pb = new Bitmap(pb.Width, pb.Height);
            pb.Image = bmp_pb;

            bmp_pb_normogram = new Bitmap(pbNormogram.Width, pbNormogram.Height);
            pbNormogram.Image = bmp_pb_normogram;

            listTriangle.Add(new FigureInfo(new PointF(0, 0), new PointF(100, 0), new PointF(50 - 5, -2), "C2H2"));
            listTriangle.Add(new FigureInfo(new PointF(100 * COS(60), 100 * SIN(60)), new PointF(0, 0), new PointF((50 - 25) * COS(60), (50) * SIN(60)), "CH4"));
            listTriangle.Add(new FigureInfo(new PointF(100, 0), new PointF(100 * COS(60), 100 * SIN(60)), new PointF((150 + 5) * COS(60), (50) * SIN(60)), "C2H4"));

            listPentagon.Add(new FigureInfo(new PointF(0, 40), new PointF(-38.04f, 12.36f), new PointF(-3, 45), "H2"));
            listPentagon.Add(new FigureInfo(new PointF(-38.04f, 12.36f), new PointF(-23.51f, -32.36f), new PointF(-48, 14), "C2H6"));
            listPentagon.Add(new FigureInfo(new PointF(-23.51f, -32.36f), new PointF(23.51f, -32.36f), new PointF(-30, -33), "CH4"));
            listPentagon.Add(new FigureInfo(new PointF(23.51f, -32.36f), new PointF(38.04f, 12.36f), new PointF(20, -33), "C2H4"));
            listPentagon.Add(new FigureInfo(new PointF(38.04f, 12.36f), new PointF(0, 40), new PointF(38, 14), "C2H2"));

            for (int i = 1; i < 10; i++)
            {
                listTriangleEx.Add(new FigureInfo(new PointF(i * 10 * COS(60), i * 10 * SIN(60)), new PointF(100 - i * 10 * COS(60), i * 10 * SIN(60)), new PointF(), ""));
                listTriangleEx.Add(new FigureInfo(new PointF(i * 10, 0), new PointF(i * 10 * COS(60), i * 10 * SIN(60)), new PointF(), ""));
                listTriangleEx.Add(new FigureInfo(new PointF(i * 10, 0), new PointF((100 + i * 10) * COS(60), (10 - i) * 10 * SIN(60)), new PointF(), ""));
            }

            listNormogramValues.Add(0);
            listNormogramValues.Add(0);
            listNormogramValues.Add(0);
            listNormogramValues.Add(0);
            listNormogramValues.Add(0);

            /*listTriangle2.Add(new PointF(0, 0));
            listTriangle2.Add(new PointF(100 * COS(60), 100 * SIN(60)));
            listTriangle2.Add(new PointF(100, 0));*/

            // Triangle 1 Mineral Oils

            listTriangleModels.Add(new List<KeyValuePair<PointF,PointF>>());

            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF(98 * COS(60), 98 * SIN(60)), new PointF(102 * COS(60), 98 * SIN(60))));

            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF(96 * COS(60), 96 * SIN(60)), new PointF((120 - 4) * COS(60), (80 - 4) * SIN(60))));
            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((120 - 4) * COS(60), (80 - 4) * SIN(60)), new PointF(120 * COS(60), 80 * SIN(60))));

            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((120 - 4) * COS(60), (80 - 4) * SIN(60)), new PointF((150 - 4) * COS(60), (50 - 4) * SIN(60))));
            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((150 - 4) * COS(60), (50 - 4) * SIN(60)), new PointF(150 * COS(60), 50 * SIN(60))));

            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((150 - 15) * COS(60), (50 - 15) * SIN(60)), new PointF(100 - 15, 0)));
            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((150 - 15) * COS(60), (50 - 15) * SIN(60)), new PointF(150 * COS(60), 50 * SIN(60))));

            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 13) * COS(60), (100 - 13) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));
            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF(100 - 29, 0)));
            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));

            listTriangleModels[0].Add(new KeyValuePair<PointF, PointF>(new PointF((123 - 13) * COS(60), (77 - 13) * SIN(60)), new PointF(23, 0)));

            // Triangle 2 LTC

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());

            listTriangleModels[1].Add(new KeyValuePair<PointF, PointF>(new PointF(23, 0), new PointF((100 - 23) * COS(60) + 23, (100 - 23) * SIN(60))));

            listTriangleModels[1].Add(new KeyValuePair<PointF, PointF>(new PointF((123 - 15) * COS(60), (50 + 50 - 23 - 15) * SIN(60)), new PointF((150 - 15) * COS(60), (50 - 15) * SIN(60))));

            listTriangleModels[1].Add(new KeyValuePair<PointF, PointF>(new PointF((150 - 15) * COS(60), (50 - 15) * SIN(60)), new PointF(100 - 15, 0)));
            listTriangleModels[1].Add(new KeyValuePair<PointF, PointF>(new PointF((150 - 15) * COS(60), (50 - 15) * SIN(60)), new PointF(150 * COS(60), 50 * SIN(60))));

            listTriangleModels[1].Add(new KeyValuePair<PointF, PointF>(new PointF(19 * COS(60), 19 * SIN(60)), new PointF(19 * COS(60) + 23, 19 * SIN(60))));

            listTriangleModels[1].Add(new KeyValuePair<PointF, PointF>(new PointF(2 * COS(60) + 6, 2 * SIN(60)), new PointF(2 * COS(60) + 23, 2 * SIN(60))));

            listTriangleModels[1].Add(new KeyValuePair<PointF, PointF>(new PointF(2 * COS(60) + 6, 2 * SIN(60)), new PointF(19 * COS(60) + 6, 19 * SIN(60))));


            // Triangle 3 BioTemp

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());
            
            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF(98 * COS(60), 98 * SIN(60)), new PointF(102 * COS(60), 98 * SIN(60))));

            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF(96 * COS(60), 96 * SIN(60)), new PointF((152 - 4) * COS(60), (100 - 52 - 4) * SIN(60))));
            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF((152 - 4) * COS(60), (100 - 52 - 4) * SIN(60)), new PointF(152 * COS(60), (100 - 52) * SIN(60))));

            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF((152 - 4) * COS(60), (100 - 52 - 4) * SIN(60)), new PointF((182 - 4) * COS(60), (100 - 82 - 4) * SIN(60))));
            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF((182 - 4) * COS(60), (100 - 82 - 4) * SIN(60)), new PointF(182 * COS(60), (100 - 82) * SIN(60))));

            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 13) * COS(60), (100 - 13) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));
            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF(100 - 29, 0)));
            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));

            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF((120 - 13) * COS(60), (100 - 20 - 13) * SIN(60)), new PointF(20, 0)));

            listTriangleModels[2].Add(new KeyValuePair<PointF, PointF>(new PointF(82, 0), new PointF(82 + (100 - 82) * COS(60), (100 - 82) * SIN(60))));

            // Triangle 3 FR3

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());

            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF(98 * COS(60), 98 * SIN(60)), new PointF(102 * COS(60), 98 * SIN(60))));

            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF(96 * COS(60), 96 * SIN(60)), new PointF((100 + 43 - 4) * COS(60), (100 - 43 - 4) * SIN(60))));
            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 43 - 4) * COS(60), (100 - 43 - 4) * SIN(60)), new PointF((100 + 43) * COS(60), (100 - 43) * SIN(60))));

            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 43 - 4) * COS(60), (100 - 43 - 4) * SIN(60)), new PointF((100 + 63 - 4) * COS(60), (100 - 63 - 4) * SIN(60))));
            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 63 - 4) * COS(60), (100 - 63 - 4) * SIN(60)), new PointF((100 + 63) * COS(60), (100 - 63) * SIN(60))));

            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 63 - 15) * COS(60), (100 - 63 - 15) * SIN(60)), new PointF(100 - 15, 0)));
            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 63 - 15) * COS(60), (100 - 63 - 15) * SIN(60)), new PointF((100 + 63) * COS(60), (100 - 63) * SIN(60))));

            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 13) * COS(60), (100 - 13) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));
            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF(100 - 29, 0)));
            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));

            listTriangleModels[3].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 25 - 13) * COS(60), (100 - 25 - 13) * SIN(60)), new PointF(25, 0)));

            // Triangle 3 Midel

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());

            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 2) * COS(60), (100 - 2) * SIN(60)), new PointF((100 + 2) * COS(60), (100 - 2) * SIN(60))));

            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 4) * COS(60), 96 * SIN(60)), new PointF((100 + 39 - 4) * COS(60), (100 - 39 - 4) * SIN(60))));
            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 39 - 4) * COS(60), (100 - 39 - 4) * SIN(60)), new PointF((100 + 39) * COS(60), (100 - 39) * SIN(60))));

            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 39 - 4) * COS(60), (100 - 39 - 4) * SIN(60)), new PointF((100 + 68 - 4) * COS(60), (100 - 68 - 4) * SIN(60))));
            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 68 - 4) * COS(60), (100 - 68 - 4) * SIN(60)), new PointF((100 + 68) * COS(60), (100 - 68) * SIN(60))));

            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 68 - 15) * COS(60), (100 - 68 - 15) * SIN(60)), new PointF(100 - 15, 0)));
            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 68 - 15) * COS(60), (100 - 68 - 15) * SIN(60)), new PointF((100 + 68) * COS(60), (100 - 68) * SIN(60))));

            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 13) * COS(60), (100 - 13) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));
            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF(100 - 29, 0)));
            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));

            listTriangleModels[4].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 26 - 13) * COS(60), (100 - 26 - 13) * SIN(60)), new PointF(26, 0)));

            // Triangle 3 Silicone

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());

            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 2) * COS(60), (100 - 2) * SIN(60)), new PointF((100 + 2) * COS(60), (100 - 2) * SIN(60))));

            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 4) * COS(60), 96 * SIN(60)), new PointF((100 + 16 - 4) * COS(60), (100 - 16 - 4) * SIN(60))));
            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 16 - 4) * COS(60), (100 - 16 - 4) * SIN(60)), new PointF((100 + 16) * COS(60), (100 - 16) * SIN(60))));

            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 16 - 4) * COS(60), (100 - 16 - 4) * SIN(60)), new PointF((100 + 46 - 4) * COS(60), (100 - 46 - 4) * SIN(60))));
            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 46 - 4) * COS(60), (100 - 46 - 4) * SIN(60)), new PointF((100 + 46) * COS(60), (100 - 46) * SIN(60))));

            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 46 - 15) * COS(60), (100 - 46 - 15) * SIN(60)), new PointF(100 - 15, 0)));
            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 46 - 15) * COS(60), (100 - 46 - 15) * SIN(60)), new PointF((100 + 46) * COS(60), (100 - 46) * SIN(60))));

            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 13) * COS(60), (100 - 13) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));
            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF(100 - 29, 0)));
            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((140 - 29) * COS(60), (60 - 29) * SIN(60)), new PointF((140 - 13) * COS(60), (60 - 13) * SIN(60))));

            listTriangleModels[5].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 9 - 13) * COS(60), (100 - 9 - 13) * SIN(60)), new PointF(9, 0)));

            // Triangle 4 LTF Mineral Oils

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());

            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 2) * COS(60), (100 - 2) * SIN(60)), new PointF((100 + 2 - 1) * COS(60), (100 - 2 - 1) * SIN(60))));
            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 2 - 1) * COS(60), (100 - 2 - 1) * SIN(60)), new PointF((100 + 15 - 1) * COS(60), (100 - 15 - 1) * SIN(60))));
            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 15 - 1) * COS(60), (100 - 15 - 1) * SIN(60)), new PointF((100 + 15) * COS(60), (100 - 15) * SIN(60))));

            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 36) * COS(60), (100 - 36) * SIN(60)), new PointF((100 + 36 - 24) * COS(60), (100 - 36 - 24) * SIN(60))));
            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 36 - 24) * COS(60), (100 - 36 - 24) * SIN(60)), new PointF(100 - 24 - 15 * COS(60), 15 * SIN(60))));
            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF(100 - 24 - 15 * COS(60), 15 * SIN(60)), new PointF(100 - 30 - 15 * COS(60), 15 * SIN(60))));
            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF(100 - 30 - 15 * COS(60), 15 * SIN(60)), new PointF(100 - 30 - 9 * COS(60), 9 * SIN(60))));

            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF(9 * COS(60), 9 * SIN(60)), new PointF(100 - 30 - 9 * COS(60), 9 * SIN(60))));
            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF(100 - 30 - 9 * COS(60), 9 * SIN(60)), new PointF(100 - 30, 0)));

            listTriangleModels[6].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 46) * COS(60), (100 - 46) * SIN(60)), new PointF((100 - 46) - 9 * COS(60), 9 * SIN(60))));


            // Triangle 5 LTF Mineral Oils

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());

            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF(10, 0), new PointF((100 + 10) * COS(60), (100 - 10) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 54) * COS(60), (100 - 54) * SIN(60)), new PointF((100 - 54 + 10) * COS(60), (100 - 54 - 10) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 15) * COS(60), (100 - 15) * SIN(60)), new PointF((100 - 15 + 10) * COS(60), (100 - 15 - 10) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 30 + 10) * COS(60), (100 - 30 - 10) * SIN(60)), new PointF(100 - 30, 0)));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF(35, 0), new PointF(35 + (100 - 30 - 35) * COS(60), (100 - 30 - 35) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF(100 - 30, 0), new PointF(100 - 30 + (30 - 14) * COS(60), (30 - 14) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 12 + 10) * COS(60), (100 - 12 - 10) * SIN(60)), new PointF((100 - 12 + 49) * COS(60), (100 - 12 - 49) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 12 + 49) * COS(60), (100 - 12 - 49) * SIN(60)), new PointF((100 - 12 + 49 - 2) * COS(60), (100 - 12 - 49 - 2) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 12 + 49 - 2) * COS(60), (100 - 12 - 49 - 2) * SIN(60)), new PointF(100 - 30 + (30 - 14) * COS(60), (30 - 14) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 35 - 12) * COS(60), (100 - 35 - 12) * SIN(60)), new PointF((100 + 35) * COS(60), (100 - 35) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 15 + 1) * COS(60), (100 - 15 - 1) * SIN(60)), new PointF((100 - 2 + 1) * COS(60), (100 - 2 - 1) * SIN(60))));
            listTriangleModels[7].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 2 + 1) * COS(60), (100 - 2 - 1) * SIN(60)), new PointF((100 - 2) * COS(60), (100 - 2) * SIN(60))));

            // Triangle 6 LTF FR3 Oils

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());

            listTriangleModels[8].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 2) * COS(60), (100 - 2) * SIN(60)), new PointF((100 + 2 - 1) * COS(60), (100 - 2 - 1) * SIN(60))));
            listTriangleModels[8].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 2 - 1) * COS(60), (100 - 2 - 1) * SIN(60)), new PointF((100 + 15 - 1) * COS(60), (100 - 15 - 1) * SIN(60))));
            listTriangleModels[8].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 15 - 1) * COS(60), (100 - 15 - 1) * SIN(60)), new PointF((100 + 15) * COS(60), (100 - 15) * SIN(60))));

            listTriangleModels[8].Add(new KeyValuePair<PointF, PointF>(new PointF(12, 0), new PointF((100 + 12) * COS(60), (100 - 12) * SIN(60))));
            listTriangleModels[8].Add(new KeyValuePair<PointF, PointF>(new PointF(12 + 9 * COS(60), 9 * SIN (60)), new PointF(100 - 24 - 9 * COS(60), 9* SIN(60))));

            listTriangleModels[8].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 36 - 24) * COS(60), (100 - 36 - 24) * SIN(60)), new PointF(100 - 24, 0)));
            listTriangleModels[8].Add(new KeyValuePair<PointF, PointF>(new PointF((100 + 36 - 24) * COS(60), (100 - 36 - 24) * SIN(60)), new PointF((100 + 36) * COS(60), (100 - 36) * SIN(60))));

            // Triangle 7 LTF FR3 Oils

            listTriangleModels.Add(new List<KeyValuePair<PointF, PointF>>());

            listTriangleModels[9].Add(new KeyValuePair<PointF, PointF>(new PointF(35, 0), new PointF((100 + 35) * COS(60), (100 - 35) * SIN(60))));
            listTriangleModels[9].Add(new KeyValuePair<PointF, PointF>(new PointF(13, 0), new PointF((100 - 63 + 13) * COS(60), (100 - 63 - 13) * SIN(60))));
            listTriangleModels[9].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 63 + 13) * COS(60), (100 - 63 - 13) * SIN(60)), new PointF((100 - 63) * COS(60), (100 - 63) * SIN(60))));
            listTriangleModels[9].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 63 + 10) * COS(60), (100 - 63 - 10) * SIN(60)), new PointF((100 + 10) * COS(60), (100 - 10) * SIN(60))));
            listTriangleModels[9].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 30 + 10) * COS(60), (100 - 30 - 10) * SIN(60)), new PointF((100 - 30 + 35) * COS(60), (100 - 30 - 35) * SIN(60))));
            listTriangleModels[9].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 15) * COS(60), (100 - 15) * SIN(60)), new PointF((100 - 15 + 1) * COS(60), (100 - 15 - 1) * SIN(60))));
            listTriangleModels[9].Add(new KeyValuePair<PointF, PointF>(new PointF((100 - 15 + 1) * COS(60), (100 - 15 - 1) * SIN(60)), new PointF((100 + 1) * COS(60), (100 - 1) * SIN(60))));

            // Pentagon 1

            listPentagonModels.Add(new List<PentagonPartInfo>());

            PentagonPartInfo ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(1, -32.36f));
            ppi.listPoints.Add(new PointF(-6, -4));
            ppi.listPoints.Add(new PointF(-23.51f, -32.36f));
            //ppi.listPoints.Add(new PointF(1, -32));
            ppi.strName = "T2 = Thermal Fault  300°C - 700°C";
            listPentagonModels[0].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(23.51f, -32.36f));
            ppi.listPoints.Add(new PointF(24, -30));
            ppi.listPoints.Add(new PointF(0, -3));
            ppi.listPoints.Add(new PointF(-6, -4));
            ppi.listPoints.Add(new PointF(1, -32.36f));
            //ppi.listPoints.Add(new PointF(23.2f, -32.4f));
            ppi.strName = "T3 = Thermal Fault > 300°C";
            listPentagonModels[0].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(4, 16));
            ppi.listPoints.Add(new PointF(32, -6));
            ppi.listPoints.Add(new PointF(24, -30));
            ppi.listPoints.Add(new PointF(0, -3));
            ppi.listPoints.Add(new PointF(0, 1.5f));
            //ppi.listPoints.Add(new PointF(4, 16));
            ppi.strName = "D2 = Discharges of High Energuy";
            listPentagonModels[0].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(0, 40));
            ppi.listPoints.Add(new PointF(38.04f, 12.36f));
            ppi.listPoints.Add(new PointF(32, -6));
            ppi.listPoints.Add(new PointF(4, 16));
            ppi.listPoints.Add(new PointF(0, 1.5f));
            //ppi.listPoints.Add(new PointF(0, 40));
            ppi.strName = "D1 = Discharges of Low Energy";
            listPentagonModels[0].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(0, 24.5f));
            ppi.listPoints.Add(new PointF(-1, 24.5f));
            ppi.listPoints.Add(new PointF(-1, 33));
            ppi.listPoints.Add(new PointF(0, 33));
            //ppi.listPoints.Add(new PointF(0, 24.5f));
            ppi.strName = "PD = Corona Partial Discharge";
            listPentagonModels[0].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(0, 40));
            ppi.listPoints.Add(new PointF(-38.04f, 12.36f));
            ppi.listPoints.Add(new PointF(-35, 3));
            ppi.listPoints.Add(new PointF(0, 1.5f));
            ppi.listPoints.Add(new PointF(0, 24.5f));
            ppi.listPoints.Add(new PointF(-1, 24.5f));
            ppi.listPoints.Add(new PointF(-1, 33));
            ppi.listPoints.Add(new PointF(0, 33));
            //ppi.listPoints.Add(new PointF(0, 40));
            ppi.strName = "S = Stray Gasing of Oil < 200°C";
            listPentagonModels[0].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(-35, 3));
            ppi.listPoints.Add(new PointF(-23.51f, -32.36f));
            ppi.listPoints.Add(new PointF(-6, -4));
            ppi.listPoints.Add(new PointF(0, -3));
            ppi.listPoints.Add(new PointF(0, 1.5f));
            //ppi.listPoints.Add(new PointF(-35, 3));
            ppi.strName = "T1 = Thermal Fault < 300°C";
            listPentagonModels[0].Add(ppi);

            // Pentagon 2

            listPentagonModels.Add(new List<PentagonPartInfo>());

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(2.5f, -32.36f));
            ppi.listPoints.Add(new PointF(-3.5f, -3));
            ppi.listPoints.Add(new PointF(-11, -8));
            ppi.listPoints.Add(new PointF(-21.5f, -32.36f));
            ppi.strName = "C = Possible Carbonization of Paper";
            listPentagonModels[1].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(24, -30));
            ppi.listPoints.Add(new PointF(0, -3));
            ppi.listPoints.Add(new PointF(-3.5f, -3));
            ppi.listPoints.Add(new PointF(-3.5f, -3));
            ppi.listPoints.Add(new PointF(2.5f, -32.36f));
            ppi.listPoints.Add(new PointF(23.51f, -32.36f));
            //ppi.listPoints.Add(new PointF(23.2f, -32.4f));
            ppi.strName = "T3-H = T3 in Oil only";
            listPentagonModels[1].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(4, 16));
            ppi.listPoints.Add(new PointF(32, -6));
            ppi.listPoints.Add(new PointF(24, -30));
            ppi.listPoints.Add(new PointF(0, -3));
            ppi.listPoints.Add(new PointF(0, 1.5f));
            //ppi.listPoints.Add(new PointF(4, 16));
            ppi.strName = "D2 = Discharges of High Energuy";
            listPentagonModels[1].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(0, 40));
            ppi.listPoints.Add(new PointF(38.04f, 12.36f));
            ppi.listPoints.Add(new PointF(32, -6));
            ppi.listPoints.Add(new PointF(4, 16));
            ppi.listPoints.Add(new PointF(0, 1.5f));
            //ppi.listPoints.Add(new PointF(0, 40));
            ppi.strName = "D1 = Discharges of Low Energy";
            listPentagonModels[1].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(0, 24.5f));
            ppi.listPoints.Add(new PointF(-1, 24.5f));
            ppi.listPoints.Add(new PointF(-1, 33));
            ppi.listPoints.Add(new PointF(0, 33));
            //ppi.listPoints.Add(new PointF(0, 24.5f));
            ppi.strName = "PD = Corona Partial Discharge";
            listPentagonModels[1].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(0, 40));
            ppi.listPoints.Add(new PointF(-38.04f, 12.36f));
            ppi.listPoints.Add(new PointF(-35, 3));
            ppi.listPoints.Add(new PointF(0, 1.5f));
            ppi.listPoints.Add(new PointF(0, 24.5f));
            ppi.listPoints.Add(new PointF(-1, 24.5f));
            ppi.listPoints.Add(new PointF(-1, 33));
            ppi.listPoints.Add(new PointF(0, 33));
            //ppi.listPoints.Add(new PointF(0, 40));
            ppi.strName = "S = Stray Gasing of Oil < 200°C";
            listPentagonModels[1].Add(ppi);

            ppi = new PentagonPartInfo();
            ppi.listPoints.Add(new PointF(-21.5f, -32.36f));
            ppi.listPoints.Add(new PointF(-11, -8));
            ppi.listPoints.Add(new PointF(-3.5f, -3));
            ppi.listPoints.Add(new PointF(0, -3));
            ppi.listPoints.Add(new PointF(0, 1.5f));
            ppi.listPoints.Add(new PointF(-35, 3));
            ppi.listPoints.Add(new PointF(-23.51f, -32.36f));
            ppi.strName = "O = Overheating < 250°C";
            listPentagonModels[1].Add(ppi);

            float width = pb.ClientRectangle.Width;
            float height = pb.ClientRectangle.Height;

            Graphics gr = pb.CreateGraphics();

            DpiXRel = gr.DpiX / 96.0f;
            DpiYRel = gr.DpiY / 96.0f;

            triangle_zoom *= DpiXRel;
            pentagon_zoom *= DpiYRel;

            triangle_shift_x = (pb.ClientRectangle.Width - 100 * triangle_zoom) / 2;
            triangle_shift_y = (pb.ClientRectangle.Height - 100 * SIN(60) * triangle_zoom) / 2;

            pentagon_shift_x = (pb.ClientRectangle.Width - (2 * 40) * pentagon_zoom) / 2;
            pentagon_shift_y = (pb.ClientRectangle.Height - (2 * 40) * pentagon_zoom) / 2;

            /*triangle_shift_x *= gr.DpiX / 96.0f;
            triangle_shift_y *= gr.DpiY / 96.0f;

            pentagon_shift_x *= gr.DpiX / 96.0f;
            pentagon_shift_y *= gr.DpiY / 96.0f;*/

            for (int i = 0; i < listTriangle.Count; i++)
            {
                FigureInfo mi = listTriangle[i];
                mi.p1 = new PointF(listTriangle[i].p1.X * triangle_zoom + triangle_shift_x, height - listTriangle[i].p1.Y * triangle_zoom - triangle_shift_y);
                mi.p2 = new PointF(listTriangle[i].p2.X * triangle_zoom + triangle_shift_x, height - listTriangle[i].p2.Y * triangle_zoom - triangle_shift_y);
                mi.pName = new PointF(listTriangle[i].pName.X * triangle_zoom + triangle_shift_x, height - listTriangle[i].pName.Y * triangle_zoom - triangle_shift_y);
                listTriangle[i] = mi;
            }

            for (int i = 0; i < listPentagon.Count; i++)
            {
                FigureInfo mi = listPentagon[i];
                mi.p1 = new PointF((listPentagon[i].p1.X + 40) * pentagon_zoom + pentagon_shift_x, height - (listPentagon[i].p1.Y + 40) * pentagon_zoom - pentagon_shift_y);
                mi.p2 = new PointF((listPentagon[i].p2.X + 40) * pentagon_zoom + pentagon_shift_x, height - (listPentagon[i].p2.Y + 40) * pentagon_zoom - pentagon_shift_y);
                mi.pName = new PointF((listPentagon[i].pName.X + 40) * pentagon_zoom + pentagon_shift_x, height - (listPentagon[i].pName.Y + 40) * pentagon_zoom - pentagon_shift_y);
                listPentagon[i] = mi;
            }

            for (int i = 0; i < listTriangleEx.Count; i++)
            {
                FigureInfo mi = listTriangleEx[i];
                mi.p1 = new PointF(listTriangleEx[i].p1.X * triangle_zoom + triangle_shift_x, height - listTriangleEx[i].p1.Y * triangle_zoom - triangle_shift_y);
                mi.p2 = new PointF(listTriangleEx[i].p2.X * triangle_zoom + triangle_shift_x, height - listTriangleEx[i].p2.Y * triangle_zoom - triangle_shift_y);
                mi.pName = new PointF(listTriangleEx[i].pName.X * triangle_zoom + triangle_shift_x, height - listTriangleEx[i].pName.Y * triangle_zoom - triangle_shift_y);
                listTriangleEx[i] = mi;
            }

            for (int j = 0; j < listTriangleModels.Count; j++)
            {
                for (int i = 0; i < listTriangleModels[j].Count; i++)
                {
                    listTriangleModels[j][i] = new KeyValuePair<PointF, PointF>(new PointF(listTriangleModels[j][i].Key.X * triangle_zoom + triangle_shift_x, height - listTriangleModels[j][i].Key.Y * triangle_zoom - triangle_shift_y),
                        new PointF(listTriangleModels[j][i].Value.X * triangle_zoom + triangle_shift_x, height - listTriangleModels[j][i].Value.Y * triangle_zoom - triangle_shift_y));
                }
            }

            for (int j = 0; j < listPentagonModels.Count; j++)
            {
                for (int i = 0; i < listPentagonModels[j].Count; i++)
                {
                    ppi = listPentagonModels[j][i];
                    for (int k = 0; k < ppi.listPoints.Count; k++)
                    {
                        ppi.listPoints[k] = new PointF((ppi.listPoints[k].X + 40) * pentagon_zoom + pentagon_shift_x, height - (ppi.listPoints[k].Y + 40) * pentagon_zoom - pentagon_shift_y);
                    }

                    listPentagonModels[j][i] = ppi;
                }
            }

            cbMethod.SelectedIndex = 0;
            pb_Paint();
            pbNormogram_Paint();
        }

        private void cbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMethod.SelectedIndex >= 0 && cbMethod.SelectedIndex <= 5)
            {
                FigureInfo mi = listTriangle[0];
                mi.strName = "C2H2";
                listTriangle[0] = mi;

                mi = listTriangle[1];
                mi.strName = "CH4";
                listTriangle[1] = mi;

                mi = listTriangle[2];
                mi.strName = "C2H4";
                listTriangle[2] = mi;
            }

            if (cbMethod.SelectedIndex == 6 || cbMethod.SelectedIndex == 8)
            {
                FigureInfo mi = listTriangle[0];
                mi.strName = "C2H6";
                listTriangle[0] = mi;

                mi = listTriangle[1];
                mi.strName = "H2";
                listTriangle[1] = mi;

                mi = listTriangle[2];
                mi.strName = "CH4";
                listTriangle[2] = mi;
            }

            if (cbMethod.SelectedIndex == 7 || cbMethod.SelectedIndex == 9)
            {
                FigureInfo mi = listTriangle[0];
                mi.strName = "C2H6";
                listTriangle[0] = mi;

                mi = listTriangle[1];
                mi.strName = "CH4";
                listTriangle[1] = mi;

                mi = listTriangle[2];
                mi.strName = "C2H4";
                listTriangle[2] = mi;
            }

            /*cbInteractivGraphic.Visible = true;
            if (cbMethod.SelectedIndex > 9)
            {
                cbInteractivGraphic.Checked = false;
                cbInteractivGraphic.Visible = false;
            }*/

            FindPoint();
            //pb.Invalidate();
        }

        private PointF	CrossLine(PointF p1, PointF p2, PointF p3, PointF p4) 
        {
            PointF res = new PointF(-1, -1);
            float	dx1 = p2.X - p1.X;
            float dy1 = p2.Y - p1.Y;
            float dx2 = p4.X - p3.X;
            float dy2 = p4.Y - p3.Y;
            res.X = dy1 * dx2 - dy2 * dx1;
            if (res.X == 0 || dx2 == 0) return new PointF(-1, -1);
	        res.Y = p3.X * p4.Y - p3.Y * p4.X;
            res.X = ((p1.X * p2.Y - p1.Y * p2.X) * dx2 - res.Y * dx1) / res.X;
            res.Y = (dy2 * res.X - res.Y) / dx2;
            return res; //((x1 <= x && x2 >= x) || (x2 <= x && x1 >= x)) && ((x3 <= x && x4 >= x) || (x4 <= x && x3 >= x));
        }

        private void CalcResult()
        {
            float CH4 = 0;
            float C2H2 = 0;
            float C2H4 = 0;
            float H2 = 0;
            float C2H6 = 0;

            teRes.Text = "";

            float.TryParse(teCH4.Text.Replace(".", ","), out CH4);
            float.TryParse(teC2H2.Text.Replace(".", ","), out C2H2);
            float.TryParse(teC2H4.Text.Replace(".", ","), out C2H4);
            float.TryParse(teH2.Text.Replace(".", ","), out H2);
            float.TryParse(teC2H6.Text.Replace(".", ","), out C2H6);

            float sum = 0;

            if (cbMethod.SelectedIndex >= 0 && cbMethod.SelectedIndex <= 5)
                sum = CH4 + C2H2 + C2H4;

            if (cbMethod.SelectedIndex == 6 || cbMethod.SelectedIndex == 8)
                sum = CH4 + H2 + C2H6;

            if (cbMethod.SelectedIndex == 7 || cbMethod.SelectedIndex == 9)
                sum = CH4 + C2H6 + C2H4;

            if (cbMethod.SelectedIndex > 9)
                sum = CH4 + C2H6 + C2H4 + H2 + C2H2;

            if (sum == 0)
            {
                return;
            }

            CH4 = CH4 * 100 / sum;
            C2H2 = C2H2 * 100 / sum;
            C2H4 = C2H4 * 100 / sum;
            H2 = H2 * 100 / sum;
            C2H6 = C2H6 * 100 / sum;

            if (cbMethod.SelectedIndex == 0)
            {
                if (CH4 >= 98)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (CH4 < 98 && C2H4 <= 20 && C2H2 <= 4)
                {
                    teRes.Text = "T1 = Thermal faults of temperature T < 300 C";
                    return;
                }

                if (C2H4 > 20 && C2H4 < 50 && C2H2 <= 4)
                {
                    teRes.Text = "T2 = Thermal faults, 300 C < T < 700 C";
                    return;
                }

                if (C2H4 >= 50 && C2H2 <= 15)
                {
                    teRes.Text = "T3 = Thermal faults, T > 700 C";
                    return;
                }

                if (C2H4 <= 23 && C2H2 >= 13)
                {
                    teRes.Text = "D1 = Electrical discharges of low energy ";
                    return;
                }

                if (C2H4 > 23 && C2H4 <= 40 && C2H2 >= 13 && C2H2 < 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 > 23 && C2H2 >= 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 < 50 && C2H2 < 13 && C2H2 > 4)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H2 > 15 && C2H2 < 29)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H4 < 50 && C2H2 >= 13 && C2H2 <= 15)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 1)
            {
                if (CH4 <= 19 && CH4>= 2  && C2H4 <= 23 && C2H4 >= 6)
                {
                    teRes.Text = "N = Normal operation";
                    return;
                }

                if (CH4 <= 19 && C2H4 < 6)
                {
                    teRes.Text = "D1 = Abnormal arcing D1";
                    return;
                }

                if (CH4 < 2 && C2H4 >= 6 && C2H4 <= 23)
                {
                    teRes.Text = "D1 = Abnormal arcing D1";
                    return;
                }

                if (C2H4 <= 23 && CH4 > 19)
                {
                    teRes.Text = "X1 = Abnormal arcing D1 or thermal fault in progress";
                    return;
                }

                if (C2H4 > 23 && C2H2 > 15)
                {
                    teRes.Text = "X3 = Fault T3 or T2 in progress, or abnormal severe arcing D2";
                    return;
                }

                if (C2H4 > 23 && C2H2 <=15 && C2H4 < 50)
                {
                    teRes.Text = "T2 = Severe thermal fault T2 (300 < T < 700°C), coking";
                    return;
                }

                if (C2H4 >= 50 && C2H2 <= 15)
                {
                    teRes.Text = "T3 = Severe thermal fault T3 (T > 700°C), heavy coking";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 2)
            {
                if (CH4 >= 98)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (CH4 < 98 && C2H4 <= 52 && C2H2 <= 4)
                {
                    teRes.Text = "T1 = Thermal faults of temperature T < 300 C";
                    return;
                }

                if (C2H4 > 52 && C2H4 < 82 && C2H2 <= 4)
                {
                    teRes.Text = "T2 = Thermal faults, 300 C < T < 700 C";
                    return;
                }

                if (C2H4 >= 82 && C2H2 <= 18) /*15 error*/
                {
                    teRes.Text = "T3 = Thermal faults, T > 700 C";
                    return;
                }

                if (C2H4 <= 20 && C2H2 >= 13)
                {
                    teRes.Text = "D1 = Electrical discharges of low energy";
                    return;
                }

                if (C2H4 >20 && C2H4 <=40 && C2H2 >=13 && C2H2 < 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 >20 && C2H2 >= 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 <82 && C2H2 <13 && C2H2 > 4)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H2 >=13 && C2H2 <29 && C2H4 < 82)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 3)
            {
                if (CH4 >= 98)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (CH4 < 98 && C2H4 <= 43 && C2H2 <= 4)
                {
                    teRes.Text = "T1 = Thermal faults of temperature T < 300 C";
                    return;
                }

                if (C2H4 > 43 && C2H4 < 63 && C2H2 <= 4)
                {
                    teRes.Text = "T2 = Thermal faults, 300 C < T < 700 C";
                    return;
                }

                if (C2H4 >= 63 && C2H2 <= 15)
                {
                    teRes.Text = "T3 = Thermal faults, T > 700 C";
                    return;
                }

                if (C2H4 <= 25 && C2H2 >= 13)
                {
                    teRes.Text = "D1 = Electrical discharges of low energy ";
                    return;
                }

                if (C2H4 > 25 && C2H4 <= 40 && C2H2 >= 13 && C2H2 < 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 > 25 && C2H2 >= 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 < 63 && C2H2 < 13 && C2H2 > 4)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H2 > 15 && C2H2 < 29)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H4 < 63 && C2H2 >= 13 && C2H2 <= 15)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 4)
            {
                if (CH4 >= 98)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (CH4 < 98 && C2H4 <= 39 && C2H2 <= 4)
                {
                    teRes.Text = "T1 = Thermal faults of temperature T < 300 C";
                    return;
                }

                if (C2H4 > 39 && C2H4 < 68 && C2H2 <= 4)
                {
                    teRes.Text = "T2 = Thermal faults, 300 C < T < 700 C";
                    return;
                }

                if (C2H4 >= 68 && C2H2 <= 15)
                {
                    teRes.Text = "T3 = Thermal faults, T > 700 C";
                    return;
                }

                if (C2H4 <= 26 && C2H2 >= 13)
                {
                    teRes.Text = "D1 = Electrical discharges of low energy ";
                    return;
                }

                if (C2H4 > 26 && C2H4 <= 40 && C2H2 >= 13 && C2H2 < 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 > 26 && C2H2 >= 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 < 68 && C2H2 < 13 && C2H2 > 4)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H2 > 15 && C2H2 < 29)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H4 < 68 && C2H2 >= 13 && C2H2 <= 15)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 5)
            {
                if (CH4 >= 98)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (CH4 < 98 && C2H4 <= 16 && C2H2 <= 4)
                {
                    teRes.Text = "T1 = Thermal faults of temperature T < 300 C";
                    return;
                }

                if (C2H4 > 16 && C2H4 < 46 && C2H2 <= 4)
                {
                    teRes.Text = "T2 = Thermal faults, 300 C < T < 700 C";
                    return;
                }

                if (C2H4 >= 46 && C2H2 <= 15)
                {
                    teRes.Text = "T3 = Thermal faults, T > 700 C";
                    return;
                }

                if (C2H4 <= 9 && C2H2 >= 13)
                {
                    teRes.Text = "D1 = Electrical discharges of low energy ";
                    return;
                }

                if (C2H4 > 9 && C2H4 <= 40 && C2H2 >= 13 && C2H2 < 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 > 9 && C2H2 >= 29)
                {
                    teRes.Text = "D2 = Electrical discharges of high energy";
                    return;
                }

                if (C2H4 < 46 && C2H2 < 13 && C2H2 > 4)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H2 > 15 && C2H2 < 29)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }

                if (C2H4 > 40 && C2H4 < 46 && C2H2 >= 13 && C2H2 <= 15)
                {
                    teRes.Text = "DT = Mixtures of electrical and thermal faults";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 6)
            {
                if (CH4 >= 2 && CH4 <= 15 && C2H6 <= 1)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (C2H6 < 46 && H2 > 9 && C2H6 > 30)
                {
                    teRes.Text = "S = Stray gassing of mineral oil (T < 200°C)";
                    return;
                }

                if (C2H6 < 24 && CH4 < 36 && C2H6 > 1)
                {
                    teRes.Text = "S = Stray gassing of mineral oil (T < 200°C)";
                    return;
                }

                if (C2H6 < 1 && CH4 < 2)
                {
                    teRes.Text = "S = Stray gassing of mineral oil (T < 200°C)";
                    return;
                }

                if (C2H6 < 1 && CH4 > 15 && CH4 < 36)
                {
                    teRes.Text = "S = Stray gassing of mineral oil (T < 200°C)";
                    return;
                }

                if (C2H6 < 30 && H2 > 15 && C2H6 > 24)
                {
                    teRes.Text = "S = Stray gassing of mineral oil (T < 200°C)";
                    return;
                }

                if (C2H6 <= 24 && CH4 >= 36 && H2 >= 15)
                {
                    teRes.Text = "C = Hot spots with carbonization of paper (T > 300°C)";
                    return;
                }

                if (H2 <= 15 && C2H6 <= 30)
                {
                    teRes.Text = "C = Hot spots with carbonization of paper (T > 300°C)";
                    return;
                }

                if (H2 <= 9 && C2H6 > 30)
                {
                    teRes.Text = "O = Overheating (T < 250°C)";
                    return;
                }

                if (H2 > 9 && C2H6 >= 46)
                {
                    teRes.Text = "N/D = Not determined";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 7)
            {
                if (C2H4 <= 1 && C2H6 <= 15 && C2H6 >= 2)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (C2H4 < 1 && C2H6 < 2)
                {
                    teRes.Text = "O = Overheating (T < 250°C)";
                    return;
                }

                if (C2H4 > 1 && C2H6 <= 15 && C2H4 < 10)
                {
                    teRes.Text = "O = Overheating (T < 250°C)";
                    return;
                }

                if (C2H4 < 10 && C2H6 >= 54)
                {
                    teRes.Text = "O = Overheating (T < 250°C)";
                    return;
                }

                if (C2H4 < 10 && C2H6 > 15 && C2H6 < 54)
                {
                    teRes.Text = "S = Stray gassing of mineral oil (T< 200°C)";
                    return;
                }

                if (C2H4 >= 10 && C2H6 < 30 && C2H4 < 49 && C2H6 > 12)
                {
                    teRes.Text = "C = Hot Spot with carbonization of paper (T > 300°C)";
                    return;
                }

                if (C2H4 >= 49 && C2H6 < 30 && C2H4 <=70 && C2H6 > 14)
                {
                    teRes.Text = "C = Hot Spot with carbonization of paper (T > 300°C)";
                    return;
                }

                if (C2H4 >= 10 && C2H6 <= 12 && C2H4 <= 35)
                {
                    teRes.Text = "T2 = Thermal faults of high temperature 300 < T < 700° C";
                    return;
                }

                if (C2H4 > 35 && C2H6 <= 12 && C2H4 <= 49)
                {
                    teRes.Text = "T3 = Thermal faults of very high temperature T > 700° C";
                    return;
                }

                if (C2H4 >= 49 && C2H6 <= 14 && C2H4 <= 70)
                {
                    teRes.Text = "T3 = Thermal faults of very high temperature T > 700° C";
                    return;
                }

                if (C2H4 > 70)
                {
                    teRes.Text = "T3 = Thermal faults of very high temperature T > 700° C";
                    return;
                }

                if (C2H4 >= 35 && C2H6 >= 30)
                {
                    teRes.Text = "T3 = Thermal faults of very high temperature T > 700° C";
                    return;
                }

                if (C2H4 >= 10 && C2H6 >= 30 && C2H4 < 35)
                {
                    teRes.Text = "N/D = Not determined";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 8)
            {
                if (CH4 >= 2 && CH4 <= 15 && C2H6 <= 1)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (C2H6 >= 24 && H2 > 9 && CH4 >= 12)
                {
                    teRes.Text = "N/D = Not determined";
                    return;
                }

                if (C2H6 < 24 && CH4 > 12 && CH4 < 36)
                {
                    teRes.Text = "N/D = Not determined";
                    return;
                }

                if (CH4 <= 12)
                {
                    teRes.Text = "S = Stray gassing of FR3 oil";
                    return;
                }

                if (C2H6 < 24 && CH4 >= 36)
                {
                    teRes.Text = "C = Hot spots with carbonization of paper (T > 300°C)";
                    return;
                }

                if (H2 <= 9 && C2H6 >= 24 && CH4 > 12)
                {
                    teRes.Text = "O = Overheating (T < 250°C)";
                    return;
                }
            }

            if (cbMethod.SelectedIndex == 9)
            {
                if (C2H4 < 1 && C2H6 <= 15)
                {
                    teRes.Text = "PD = Corona partial discharges";
                    return;
                }

                if (C2H4 <= 13 && C2H6 >= 63)
                {
                    teRes.Text = "S = Stray gassing of FR3 oil";
                    return;
                }

                if (C2H4 <= 10 && C2H6 < 63 && C2H6 > 15)
                {
                    teRes.Text = "O = Overheating (T < 250°C)";
                    return;
                }

                if (C2H4 <= 10 && C2H6 <= 15 && C2H4 >= 1)
                {
                    teRes.Text = "O = Overheating (T < 250°C)";
                    return;
                }

                if (C2H4 >= 10 && C2H6 <= 30 && C2H4 <= 35)
                {
                    teRes.Text = "C = Hot Spot with carbonization of paper (T > 300°C)";
                    return;
                }

                if (C2H4 > 35)
                {
                    teRes.Text = "T3 = Thermal faults of very high temperature T > 700° C";
                    return;
                }

                if (C2H4 >10 && C2H6 > 30 && C2H4 <= 35 && C2H6 < 63)
                {
                    teRes.Text = "N/D = Not determined";
                    return;
                }

                if (C2H4 > 13 && C2H4 <= 35 && C2H6 >= 63)
                {
                    teRes.Text = "N/D = Not determined";
                    return;
                }
            }

            if (cbMethod.SelectedIndex > 9)
            {
                int type = cbMethod.SelectedIndex - 10;

                if (ptRes.X >= 0 && ptRes.Y >= 0)
                {
                    for (int i = 0; i < listPentagonModels[type].Count; i++)
                    {
                        if (pt_in_polygon2(ptRes, listPentagonModels[type][i].listPoints))
                        {
                            teRes.Text = listPentagonModels[type][i].strName;
                            return;
                        }
                    }
                 }
            }

/*
            if ()
            {
                teRes.Text = "";
                return;
            }
            */
        }

        private void CalcValueNormogram()
        {
            if (bUpdateValue) return;

            float H2 = 0;
            float CH4 = 0;
            float C2H2 = 0;
            float C2H4 = 0;
            float C2H6 = 0;

            listNormogramValues[0] = 0;
            listNormogramValues[1] = 0;
            listNormogramValues[2] = 0;
            listNormogramValues[3] = 0;
            listNormogramValues[4] = 0;

            float.TryParse(teH2.Text.Replace(".", ","), out H2);
            float.TryParse(teCH4.Text.Replace(".", ","), out CH4);
            float.TryParse(teC2H6.Text.Replace(".", ","), out C2H6);
            float.TryParse(teC2H4.Text.Replace(".", ","), out C2H4);
            float.TryParse(teC2H2.Text.Replace(".", ","), out C2H2);

            float maxVal = MaxVal(H2, CH4, C2H6, C2H4, C2H2);
            if (Math.Abs(maxVal) >= 0.00001)
            {
                listNormogramValues[0] = H2 / maxVal;
                listNormogramValues[1] = CH4 / maxVal;
                listNormogramValues[2] = C2H6 / maxVal;
                listNormogramValues[3] = C2H4 / maxVal;
                listNormogramValues[4] = C2H2 / maxVal;
            }

            //pbNormogram.Invalidate();
            pbNormogram_Paint();

        }

        static public float MaxVal(params float[] vals)
        {
            float max = float.MinValue;
            for (int i = 0; i < vals.Length; i++)
            {
                if (max < vals[i]) max = vals[i];
            }
            return max;
        }

        private bool FindNormagramValues(int index, float new_val)
        {
            float CH4 = 0;
            float C2H2 = 0;
            float C2H4 = 0;
            float H2 = 0;
            float C2H6 = 0;

            float.TryParse(teCH4.Text.Replace(".", ","), out CH4);
            float.TryParse(teC2H2.Text.Replace(".", ","), out C2H2);
            float.TryParse(teC2H4.Text.Replace(".", ","), out C2H4);
            float.TryParse(teH2.Text.Replace(".", ","), out H2);
            float.TryParse(teC2H6.Text.Replace(".", ","), out C2H6);

            float sum = CH4 + C2H6 + C2H4 + H2 + C2H2;
            float maxVal = MaxVal(H2, CH4, C2H6, C2H4, C2H2);

            if (new_val > 1.0)
            {
                for (int i = 0; i < listNormogramValues.Count; i++)
                {
                    listNormogramValues[i] /= new_val;
                }
                listNormogramValues[index] = 1.0f;

                switch (index)
                {
                    case 0:
                        teH2.Text = (maxVal * new_val).ToString();
                        break;
                    case 1:
                        teCH4.Text = (maxVal * new_val).ToString();
                        break;
                    case 2:
                        teC2H6.Text = (maxVal * new_val).ToString();
                        break;
                    case 3:
                        teC2H4.Text = (maxVal * new_val).ToString();
                        break;
                    case 4:
                        teC2H2.Text = (maxVal * new_val).ToString();
                        break;
                }

                return true;
            }
            else
            {
                if (new_val < 0) new_val = 0;
                if (new_val >= 0)
                {
                    listNormogramValues[index] = new_val;

                    switch (index)
                    {
                        case 0:
                            teH2.Text = (maxVal * new_val).ToString();
                            break;
                        case 1:
                            teCH4.Text = (maxVal * new_val).ToString();
                            break;
                        case 2:
                            teC2H6.Text = (maxVal * new_val).ToString();
                            break;
                        case 3:
                            teC2H4.Text = (maxVal * new_val).ToString();
                            break;
                        case 4:
                            teC2H2.Text = (maxVal * new_val).ToString();
                            break;
                    }
                }
                else
                {
                    /*for (int i = 0; i < listNormogramValues.Count; i++)
                    {
                        listNormogramValues[i] += Math.Abs(new_val);
                    }
                    listNormogramValues[index] = 0;

                    for (int i = 0; i < listNormogramValues.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                teH2.Text = (maxVal * listNormogramValues[i]).ToString();
                                break;
                            case 1:
                                teCH4.Text = (maxVal * listNormogramValues[i]).ToString();
                                break;
                            case 2:
                                teC2H6.Text = (maxVal * listNormogramValues[i]).ToString();
                                break;
                            case 3:
                                teC2H4.Text = (maxVal * listNormogramValues[i]).ToString();
                                break;
                            case 4:
                                teC2H2.Text = (maxVal * listNormogramValues[i]).ToString();
                                break;
                        }
                    }*/
                }
            }

            /*listNormogramValues[0] = H / maxVal;
            listNormogramValues[1] = CH4 / maxVal;
            listNormogramValues[2] = C2H6 / maxVal;
            listNormogramValues[3] = C2H4 / maxVal;
            listNormogramValues[4] = C2H2 / maxVal;


            float sum_proc = 1.0f;
            float sum_proc_new = 0;
            for (int i = 0; i < listNormogramValues.Count; i++)
            {
                if (i != index)
                    sum_proc_new += listNormogramValues[i];
                else
                    sum_proc_new += new_val;
            }*/

            CalcValueNormogram();

            return false;
        }

        private void FindValue()
        {
            float CH4 = 0;
            float C2H2 = 0;
            float C2H4 = 0;
            float H2 = 0;
            float C2H6 = 0;

            float.TryParse(teCH4.Text.Replace(".", ","), out CH4);
            float.TryParse(teC2H2.Text.Replace(".", ","), out C2H2);
            float.TryParse(teC2H4.Text.Replace(".", ","), out C2H4);
            float.TryParse(teH2.Text.Replace(".", ","), out H2);
            float.TryParse(teC2H6.Text.Replace(".", ","), out C2H6);

            float sum = 0;
            float x = 0;
            float y = 0;
            float z = 0;

            if (cbMethod.SelectedIndex >= 0 && cbMethod.SelectedIndex <= 5)
                sum = CH4 + C2H2 + C2H4;

            if (cbMethod.SelectedIndex == 6 || cbMethod.SelectedIndex == 8)
                sum = CH4 + H2 + C2H6;

            if (cbMethod.SelectedIndex == 7 || cbMethod.SelectedIndex == 9)
                sum = CH4 + C2H6 + C2H4;

            if (cbMethod.SelectedIndex > 9)
                sum = CH4 + C2H6 + C2H4 + H2 + C2H2;

            if (sum == 0) sum = 100;

            bUpdateValue = true;

            if (cbMethod.SelectedIndex <= 9)
            {
                if (ptRes.X < 0 || ptRes.Y < 0)
                {
                    if (cbMethod.SelectedIndex >= 0 && cbMethod.SelectedIndex <= 5)
                    {
                        teCH4.Text = "";
                        teC2H2.Text = "";
                        teC2H4.Text = "";
                    }
                    if (cbMethod.SelectedIndex == 6 || cbMethod.SelectedIndex == 8)
                    {
                        teH2.Text = "";
                        teC2H6.Text = "";
                        teCH4.Text = "";
                    }
                    if (cbMethod.SelectedIndex == 7 || cbMethod.SelectedIndex == 9)
                    {
                        teCH4.Text = "";
                        teC2H6.Text = "";
                        teC2H4.Text = "";
                    }

                    bUpdateValue = false;
                    //pb.Invalidate();
                    pb_Paint();
                    return;
                }

                float height = pb.ClientRectangle.Height;
                PointF res = new PointF((ptRes.X - triangle_shift_x) / triangle_zoom, (-ptRes.Y + height - triangle_shift_y) / triangle_zoom);

                y = 100 - res.X - res.Y * COS(60) / SIN(60);
                x = res.Y / SIN(60);
                z = 100 - y - x;

                y *= sum / 100;
                x *= sum / 100;
                z *= sum / 100;

                if (cbMethod.SelectedIndex >= 0 && cbMethod.SelectedIndex <= 5)
                {
                    teC2H2.Text = y.ToString();
                    teCH4.Text = x.ToString();
                    teC2H4.Text = z.ToString();
                }

                if (cbMethod.SelectedIndex == 6 || cbMethod.SelectedIndex == 8)
                {
                    teC2H6.Text = y.ToString();
                    teH2.Text = x.ToString();
                    teCH4.Text = z.ToString();
                }

                if (cbMethod.SelectedIndex == 7 || cbMethod.SelectedIndex == 9)
                {
                    teC2H6.Text = y.ToString();
                    teC2H4.Text = z.ToString();
                    teCH4.Text = x.ToString();
                }
            }
            else
            {
                if (ptRes.X < 0 || ptRes.Y < 0)
                {
                    teCH4.Text = "";
                    teC2H2.Text = "";
                    teC2H4.Text = "";
                    teC2H6.Text = "";
                    teH2.Text = "";

                    bUpdateValue = false;
                    //pb.Invalidate();
                    pb_Paint();
                    return;
                }
            }

            bUpdateValue = false;
            
            //pb.Invalidate();
            pb_Paint();

            CalcResult();
        }

        private void FindPoint()
        {
            if (bUpdateValue) return;

            ptRes = new PointF(-1, -1);

            float x = 0;
            float y = 0;
            float z = 0;

            float CH4 = 0;
            float C2H2 = 0;
            float C2H4 = 0;
            float H2 = 0;
            float C2H6 = 0;

            float.TryParse(teCH4.Text.Replace(".", ","), out CH4);
            float.TryParse(teC2H2.Text.Replace(".", ","), out C2H2);
            float.TryParse(teC2H4.Text.Replace(".", ","), out C2H4);
            float.TryParse(teC2H6.Text.Replace(".", ","), out C2H6);
            float.TryParse(teH2.Text.Replace(".", ","), out H2);

            float sum = 0;

            if (cbMethod.SelectedIndex <= 9)
            {
                if (cbMethod.SelectedIndex >= 0 && cbMethod.SelectedIndex <= 5)
                {
                    sum = CH4 + C2H2 + C2H4;

                    x = CH4 * 100 / sum;
                    y = C2H2 * 100 / sum;
                    z = C2H4 * 100 / sum;
                }

                if (cbMethod.SelectedIndex == 6 || cbMethod.SelectedIndex == 8)
                {
                    sum = CH4 + H2 + C2H6;

                    x = H2 * 100 / sum;
                    y = C2H6 * 100 / sum;
                    z = CH4 * 100 / sum;
                }

                if (cbMethod.SelectedIndex == 7 || cbMethod.SelectedIndex == 9)
                {
                    sum = CH4 + C2H6 + C2H4;

                    x = CH4 * 100 / sum;
                    y = C2H6 * 100 / sum;
                    z = C2H2 * 100 / sum;
                }

                if (sum == 0)
                {
                    //pb.Invalidate();
                    pb_Paint();
                    return;
                }

                //float x = 100 - C2H2;
                //float y = CH4 * SIN(60);

                PointF p1 = new PointF(100 - y, 0);
                PointF p2 = new PointF(100 * COS(60) - y, 100 * SIN(60));
                PointF p3 = new PointF(x * COS(60), x * SIN(60));
                PointF p4 = new PointF((x + 100) * COS(60), x * SIN(60));


                ptRes = CrossLine(p1, p2, p3, p4);

                ptRes.X = ptRes.X * triangle_zoom + triangle_shift_x;
                ptRes.Y = pb.ClientRectangle.Height - ptRes.Y * triangle_zoom - triangle_shift_y;
            }
            else
            {
                sum = CH4 + C2H2 + C2H4 + H2 + C2H6;

                if (sum == 0)
                {
                    //pb.Invalidate();
                    pb_Paint();
                    return;
                }

                CH4 = CH4 * 100 / sum;
                float CH4x = -1 * CH4 * COS(54);
                float CH4y = -1 * CH4 * SIN(54);

                C2H4 = C2H4 * 100 / sum;
                float C2H4x = C2H4 * COS(54);
                float C2H4y = -1 * C2H4 * SIN(54);

                C2H2 = C2H2 * 100 / sum;
                float C2H2x = C2H2 * COS(18);
                float C2H2y = C2H2 * SIN(18);

                H2 = H2 * 100 / sum;
                float H2x = H2 * COS(90);
                float H2y = H2 * SIN(90);

                C2H6 = C2H6 * 100 / sum;
                float C2H6x = -1 * C2H6 * COS(-18);
                float C2H6y = C2H6 * SIN(18);

                float A = ((H2x * C2H2y) - (H2y * C2H2x) + (C2H2x * C2H4y) - (C2H2y * C2H4x) + (C2H4x * CH4y) - (C2H4y * CH4x) + (CH4x * C2H6y) - (CH4y * C2H6x) + (C2H6x * H2y) - (C2H6y * H2x)) * 0.5f;

                if (Math.Abs(A) >= 0.00001)
                {
                    ptRes.X = ((H2x + C2H2x) * (H2x * C2H2y - H2y * C2H2x) + (C2H2x + C2H4x) * (C2H2x * C2H4y - C2H2y * C2H4x) + (C2H4x + CH4x) * (C2H4x * CH4y - C2H4y * CH4x) + (CH4x + C2H6x) * (CH4x * C2H6y - CH4y * C2H6x) + (C2H6x + H2x) * (C2H6x * H2y - C2H6y * H2x)) / (6 * A);
                    ptRes.Y = ((H2y + C2H2y) * (H2x * C2H2y - H2y * C2H2x) + (C2H2y + C2H4y) * (C2H2x * C2H4y - C2H2y * C2H4x) + (C2H4y + CH4y) * (C2H4x * CH4y - C2H4y * CH4x) + (CH4y + C2H6y) * (CH4x * C2H6y - CH4y * C2H6x) + (C2H6y + H2y) * (C2H6x * H2y - C2H6y * H2x)) / (6 * A);

                    ptRes.X = (ptRes.X + 40) * pentagon_zoom + pentagon_shift_x;
                    ptRes.Y = pb.ClientRectangle.Height - (ptRes.Y + 40) * pentagon_zoom - pentagon_shift_y;
                }
            }

            //pb.Invalidate();
            pb_Paint();

            CalcResult();
        }

        private void teCH4_TextChanged(object sender, EventArgs e)
        {
            FindPoint();
            CalcValueNormogram();
        }

        private void teC2H2_TextChanged(object sender, EventArgs e)
        {
            FindPoint();
            CalcValueNormogram();
        }

        private void teC2H4_TextChanged(object sender, EventArgs e)
        {
            FindPoint();
            CalcValueNormogram();
        }

        private bool pt_in_polygon2(PointF test, List<PointF> listPoints)
        {

            List<List<int>> q_patt = new List<List<int>>();
            q_patt.Add(new List<int>());
            q_patt.Add(new List<int>());

            q_patt[0].Add(0);
            q_patt[0].Add(1);

            q_patt[1].Add(3);
            q_patt[1].Add(2);

            if (listPoints.Count < 3) return false;

            PointF pred_pt = listPoints[listPoints.Count - 1];

            pred_pt.X -= test.X;
            pred_pt.Y -= test.Y;

            int i_ind = 0;
            if (pred_pt.Y < 0) i_ind = 1;
            int j_ind = 0;
            if (pred_pt.X < 0) j_ind = 1;
            int pred_q = q_patt[i_ind][j_ind];

            int w=0;

            for (int i = 0; i < listPoints.Count; i++ )
            {
                PointF cur_pt = listPoints[i];

                cur_pt.X -= test.X;
                cur_pt.Y -= test.Y;

                i_ind = 0;
                if (cur_pt.Y < 0) i_ind = 1;
                j_ind = 0;
                if (cur_pt.X < 0) j_ind = 1;
                int q = q_patt[i_ind][j_ind];

                switch (q - pred_q)
                {
                    case -3: ++w; break;
                    case 3: --w; break;
                    case -2: if (pred_pt.X * cur_pt.Y >= pred_pt.Y * cur_pt.X) ++w; break;
                    case 2: if (!(pred_pt.X * cur_pt.Y >= pred_pt.Y * cur_pt.X)) --w; break;
                }

                pred_pt = cur_pt;
                pred_q = q;
            }

            return w!=0;
        }

        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (!cbInteractivGraphic.Checked || e.Button != System.Windows.Forms.MouseButtons.Left) return;

            List<PointF> listPoint = new List<PointF>();

            if (cbMethod.SelectedIndex <= 9)
            {
                for (int i = 0; i < listTriangle.Count; i++)
                {
                    listPoint.Add(listTriangle[i].p1);
                }
            }
            else
            {
                for (int i = 0; i < listPentagon.Count; i++)
                {
                    listPoint.Add(listPentagon[i].p1);
                }
            }

            if (!pt_in_polygon2(new PointF(e.X, e.Y), listPoint)) return;

            if (cbMethod.SelectedIndex <= 9)
            {
                bPressDown = true;

                ptRes.X = e.X;
                ptRes.Y = e.Y;

                FindValue();
                CalcValueNormogram();
            }

            //pb.Invalidate();
            //pbNormogram.Invalidate();
        }

        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            if (cbInteractivGraphic.Checked)
            {
                if (cbMethod.SelectedIndex <= 9)
                    Cursor.Current = Cursors.Hand;
            }

            if (bPressDown)
            {
                List<PointF> listPoint = new List<PointF>();

                if (cbMethod.SelectedIndex <= 9)
                {
                    for (int i = 0; i < listTriangle.Count; i++)
                    {
                        listPoint.Add(listTriangle[i].p1);
                    }
                }
                else
                {
                    for (int i = 0; i < listPentagon.Count; i++)
                    {
                        listPoint.Add(listPentagon[i].p1);
                    }
                }

                if (!pt_in_polygon2(new PointF(e.X, e.Y), listPoint)) return;

                if (cbMethod.SelectedIndex <= 9)
                {
                    ptRes.X = e.X;
                    ptRes.Y = e.Y;

                    FindValue();
                    CalcValueNormogram();
                }

                //pb.Invalidate();
                //pbNormogram.Invalidate();
            }
        }

        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            bPressDown = false;
            //pb.Invalidate();
        }

        private void pb_MouseHover(object sender, EventArgs e)
        {
            //bPressDown = false;
        }

        private void teH_TextChanged(object sender, EventArgs e)
        {
            FindPoint();
            CalcValueNormogram();
        }

        private void teC2H6_TextChanged(object sender, EventArgs e)
        {
            FindPoint();
            CalcValueNormogram();
        }

        private void cbInteractivGraphic_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pbNormogram_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbInteractivGraphic.Checked && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (m_iSelectNormogramIndex >= 0)
                {
                    bPressDown = true;
                    m_oldMousePoint = new Point(e.X, e.Y);
                }

                pbNormogram_Paint();
            }
        }

        private void pbNormogram_MouseMove(object sender, MouseEventArgs e)
        {
            if (cbInteractivGraphic.Checked)
            {
                float x_beg = 40 * DpiXRel;
                float y_beg = 40 * DpiYRel;
                float width = pbNormogram.Width - x_beg * 2;
                float height = pbNormogram.Height - y_beg * 2;
                float radius = 3 * DpiXRel;

                if (!bPressDown)
                {
                    Cursor.Current = Cursors.Default;
                    m_iSelectNormogramIndex = -1;

                    for (int i = 0; i < listNormogramValues.Count; i++)
                    {
                        float y = listNormogramValues[i];
                        y *= 100;
                        y = height - height * y / 100;

                        if (e.X >= x_beg + (i + 1) * width / 5 - width / 10 - radius
                            && e.X <= x_beg + (i + 1) * width / 5 - width / 10 + radius
                            && e.Y >= y_beg + y - radius
                            && e.Y <= y_beg + y + radius)
                        {
                            Cursor.Current = Cursors.Hand;
                            m_iSelectNormogramIndex = i;
                            break;
                        }
                    }
                }
                else
                {
                    Cursor.Current = Cursors.Hand;

                    float y = listNormogramValues[m_iSelectNormogramIndex];
                    y *= 100;
                    y = height - height * y / 100;

                    int X = (int)(x_beg + (m_iSelectNormogramIndex + 1) * width / 5 - width / 10);

                    int Y = e.Y;
                    if ((height - (e.Y - y_beg)) / height > 1.0)
                    {
                        Y = (int)(y_beg + y) - m_oldMousePoint.Y + e.Y;
                    }

                    if (FindNormagramValues(m_iSelectNormogramIndex, (height - (Y - y_beg)) / height))
                    {
                        //Point pt = pbNormogram.PointToScreen(new Point(X, Y));
                        //Cursor.Position = pt;
                    }

                    //pbNormogram.Invalidate();
                    //pbNormogram_Paint();

                    m_oldMousePoint = new Point(e.X, e.Y);
                }
            }
        }

        private void pbNormogram_MouseUp(object sender, MouseEventArgs e)
        {
            bPressDown = false;
            m_iSelectNormogramIndex = -1;

            pbNormogram_Paint();
        }

        private void btnSaveDuval_Click(object sender, EventArgs e)
        {
            if (saveFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pb.Image.Save(saveFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void btnSaveNormogram_Click(object sender, EventArgs e)
        {
            if (saveFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pbNormogram.Image.Save(saveFileDlg.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void teH2_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void teH2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void IsNumber(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != strSeparator[0] && e.KeyChar != 8)
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == strSeparator[0] && ((TextBox)sender).Text.IndexOf(strSeparator) >= 0 && ((TextBox)sender).SelectedText.IndexOf(strSeparator) < 0)
            {
                e.Handled = true;
                return;
            }
        }

        private void teH2_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(sender, e);
        }

        private void teCH4_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(sender, e);
        }

        private void teC2H4_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(sender, e);
        }

        private void teC2H6_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(sender, e);
        }

        private void teC2H2_KeyPress(object sender, KeyPressEventArgs e)
        {
            IsNumber(sender, e);
        }
    }
}
