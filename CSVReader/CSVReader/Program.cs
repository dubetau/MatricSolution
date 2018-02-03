using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSVReader
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("2016_Matric_Schools_Report.csv");
            int count = 0;
            while (!reader.EndOfStream)
            {
                string record = reader.ReadLine();
                if (count > 0)
                {

                    if (record != null)
                    {
                        string[] records = record.Split(',');

                        PostRecord(records[0], records[1], records[2], records[3], records[4], records[5],
                            records[6], records[7], records[8]);
                    }
                }
                count++;
            }

            reader.Close();

            Console.ReadLine();
        }

        private static readonly HttpClient client = new HttpClient();

        private static void PostRecord(string emis, string centreNo, string name, string wrote2014,
            string passed2014, string wrote2015, string passed2015, string wrote2016, string passed2016)
        {
            string requestData = emis + centreNo + name + wrote2014 + passed2014 + wrote2015 + passed2015 +
                wrote2016 + passed2016;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:1779/api/Home");

            // If the post method is to be used, then add the request body.


            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                byte[] postDataBytes = Encoding.UTF8.GetBytes(requestData);
                stream.Write(postDataBytes, 0, postDataBytes.Length);
            }
            var response = (HttpWebResponse)httpWebRequest.GetResponse();
        }
    }
}
