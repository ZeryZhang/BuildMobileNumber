using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;



namespace BuildNumber
{
    class Program
    {
        static void Main(string[] args)
        {

            //Post();

            /*
            移动的号段：134(0-8)、135、136、137、138、139、147（预计用于TD上网卡）、150、151、152、157（TD专用）、158、159、187（未启用）、188（TD专用）
            联通的号段：130、131、132、155、156（世界风专用）、185（未启用）、186（3g）
            电信的号段：133、153、180（未启用）、189
             */
            //1{3{0-9},5{0,1,2,3,5,6,7,8,9},8{0,5,6,7,8,9}},{XXXX},{XXXX}

            int[] haoduan = new int[] {3, 5, 8};
            int value = DateTime.Now.Millisecond;
            Random rd = new Random(value);

            for (int i = 0; i < 100; i++)
            {
                var num = haoduan[rd.Next(0, 3)].ToString();
                var number = GetNumSection(num);
                var other = GetOtherNumber();
                Console.WriteLine(string.Format("{0}--{1}", number, other));
            }
            

            Console.Read();
        }

        /// <summary>
        /// 生成号段
        /// </summary>
        public static string GetNumSection(string number)
        {
            int value = DateTime.Now.Millisecond;
            var random = new Random(value);
            string result = string.Empty;
            int n = 0;
            //133-139段
            int[] _3duan = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            //150-159段
            int[] _5duan = new[] {0, 1, 2, 3, 5, 6, 7, 8, 9};
            //180-189段
            int[] _8duan = new[] {0, 5, 6, 7, 8, 9};
            
            switch (number)
            {
                case "3":
                     n = _3duan[random.Next(0, 10)];
                    result = string.Format("1{0}{1}", number, n);
                    break;
                case "5":
                     n = _5duan[random.Next(0, 9)];
                    result = string.Format("1{0}{1}", number, n);
                    break;
                case "8":
                    n = _8duan[random.Next(0, 6)];
                    result = string.Format("1{0}{1}", number, n);   
                    break;
            }

            return result;  

        }

        /// <summary>
        /// 生成中间段与尾段
        /// </summary>
        /// <returns></returns>
        public static string GetOtherNumber()
        {
            
            var random = new Random(DateTime.Now.Millisecond);
            //中间4位
            var center = random.Next(1000, 9999);
            //线程暂停1秒 否则两次随机值会一致
            Thread.Sleep(1);
            //后4位
            var tail = random.Next(1000, 9999);

            var result = string.Format("{0}--{1}", center, tail);
            return result;
        }

        /// <summary>
        /// POST Data
        /// </summary>
        /// <param name="number"></param>
        public static void Post(string number="18218805466")
        {
            string url = "http://xclxhd.hk515.com/pullnew/sendCode";
            RestRequest request = new RestRequest(Method.POST);
            var restClient = new RestClient(url);
            request.AddParameter("mobile", number);

            var response = restClient.Execute(request);

            string body = response.Content;

        }
    }
}
