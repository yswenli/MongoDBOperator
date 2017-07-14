/*****************************************************************************************************
* 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
*****************************************************************************************************
* CLR版本：4.0.30319.42000
* 唯一标识：63fcdf18-8930-4a86-93ca-f99f5a020844
* 机器名称：WENLI-PC
* 联系人邮箱：wenguoli_520@qq.com
*****************************************************************************************************
* 项目名称：$projectname$
* 命名空间：MongoDBOperator.Test
* 类名称：Program
* 创建时间：2017/7/13 16:00:44
* 创建人：wenli
* 创建说明：
*****************************************************************************************************/

using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDBOperator.Interface;
using MongoDBOperator.Test.Model;

namespace MongoDBOperator.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "MongoDBOperator.Test";

            IOperator<Account> customerOperator = new MongoOperator<Account>();


            var account = new Account();
            account.FirstName = "li";
            account.LastName = "wen";
            account.Phone = "13800138000";
            account.Email = "wenguoli_520@qq.com";
            account.HomeAddress = new Address
            {
                Address1 = "上海",
                Address2 = "徐汇",
                PostCode = "210001",
                City = "上海",
                Country = "中国"
            };

            Console.WriteLine("Create");

            customerOperator.Add(account);

           

            Console.WriteLine("Read");

            var c = customerOperator.Where(b => b.FirstName == "li").FirstOrDefault();

            Console.WriteLine("Update");

            c.FirstName = "guo li";

            customerOperator.Update(c);

            Console.WriteLine("Delete");

            customerOperator.Delete(c);

            customerOperator.DeleteAll();

            Console.ReadLine();

        }
    }
}