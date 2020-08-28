using System.IO;
using System.Drawing;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
namespace Net_GetAWSS3File_Demo
{
    static class Method_GetAWSS3File
    {
        
        static public Bitmap GetBitmap(string Url,string AccessKey,string SecretKey)
        {
            string Bucket;
            string ServerRegion;
            string Name;
            AnalysisUrl(Url,out Bucket, out ServerRegion, out Name);
            //與S3建立連線
            AmazonS3Client Client = new AmazonS3Client(AccessKey, SecretKey, RegionEndpoint.GetBySystemName(ServerRegion));
            //建立請求
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = Bucket,
                Key = Name,
            };
            //對S3的連線發送請求，並且等待回應
            GetObjectResponse response = Client.GetObject(request);
            //取得回應
            Stream responseStream = response.ResponseStream;
            //宣告記憶體空間
            MemoryStream Memory = new MemoryStream();
            //將回應的byte陣列複製給記憶體
            responseStream.CopyTo(Memory);
            //將記憶體內的資料轉換成bitmap
            Bitmap TmpBitmap = new Bitmap(Memory);
            Memory.Close();//關閉正在讀寫的檔案
            return new Bitmap(TmpBitmap);
        }

        static private void AnalysisUrl(string Url, out string bucket, out string Region, out string FileName)
        {
            bucket = ""; //透過Url解析出儲存桶的名稱
            Region = "";//透過Url解析出儲存桶的位置
            FileName = "";//透過Url解析出檔案的名稱(包含位址)
            int GetStartIndex = Url.IndexOf("//") + 2;
            int GetEndIndex = Url.IndexOf('.');
            bucket = Url.Substring(GetStartIndex, GetEndIndex - GetStartIndex);

            GetStartIndex = Url.IndexOf("s3-") + 3;
            GetEndIndex = Url.IndexOf(".", GetStartIndex);
            Region = Url.Substring(GetStartIndex, GetEndIndex - GetStartIndex);

            GetStartIndex = Url.IndexOf("com/") + 4;
            FileName = Url.Substring(GetStartIndex, Url.Length - GetStartIndex);
        }
    }

}
