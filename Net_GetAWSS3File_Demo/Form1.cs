using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Net_GetAWSS3File_Demo
{
    public partial class Form1 : Form
    {

        //Exsample Url
        //https://*******.s3-ap-southeast-1.amazonaws.com/******/*******/*******.png
        private static readonly RegionEndpoint bucketRegion;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Get_Click(object sender, EventArgs e)
        {
            string AcccessKey = "";
            string SecretKey = "";
            Bitmap GetImg = Method_GetAWSS3File.GetBitmap(Text_ImageUrl.Text, AcccessKey, SecretKey); ;
            pictureBox1.BackgroundImage = GetImg;
        }

    }
}
