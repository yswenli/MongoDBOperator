﻿/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：3b6fff97-bbb0-4a7c-8877-5ba5cbdce7a3
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：MongoDBOperator.Test.Model
 * 类名称：Account
 * 创建时间：2017/7/13 15:33:36
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDBOperator.Interface;
using MongoDBOperator.Model;

namespace MongoDBOperator.Test.Model
{
    [CollectionName("CustomersTest")]
    public class Account : MongoEntity
    {
        public Account()
        {
        }

        [BsonElement("fname")]
        public string FirstName
        {
            get; set;
        }

        [BsonElement("lname")]
        public string LastName
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string Phone
        {
            get; set;
        }

        public Address HomeAddress
        {
            get; set;
        }

        public IList<Order> Orders
        {
            get; set;
        }
    }

    public class Order
    {
        public DateTime PurchaseDate
        {
            get; set;
        }

        public IList<OrderItem> Items;
    }

    public class OrderItem
    {
        public int Quantity
        {
            get; set;
        }
    }

    public class Address
    {
        public string Address1
        {
            get; set;
        }

        public string Address2
        {
            get; set;
        }

        public string City
        {
            get; set;
        }

        public string PostCode
        {
            get; set;
        }

        [BsonIgnoreIfNull]
        public string Country
        {
            get; set;
        }
    }

    public class IntAccount : IMongoEntity<int>
    {
        public int Id
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
    }
}