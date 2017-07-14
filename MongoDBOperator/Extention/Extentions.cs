﻿/*****************************************************************************************************
* 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
*****************************************************************************************************
* CLR版本：4.0.30319.42000
* 唯一标识：255ef12e-46a8-4358-b3cf-997a5aa99e8f
* 机器名称：WENLI-PC
* 联系人邮箱：wenguoli_520@qq.com
*****************************************************************************************************
* 项目名称：$projectname$
* 命名空间：MongoDBOperator.Extention
* 类名称：Extentions
* 创建时间：2017/7/13 14:23:04
* 创建人：wenli
* 创建说明：
*****************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Driver;
using MongoDBOperator.Interface;
using MongoDBOperator.Model;

namespace MongoDBOperator.Extention
{
    internal static class Extentions<U>
    {

        public static string GetConnectionString(string settingName)
        {
            return ConfigurationManager.ConnectionStrings[settingName].ConnectionString;
        }
        public static string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count - 1].ConnectionString;
        }

        private static MongoDatabase GetDatabaseFromUrl(MongoUrl url)
        {
            return new MongoClient(url).GetServer().GetDatabase(url.DatabaseName);            
        }

        /// <summary>  
        /// 取得数据库集群连接  
        /// 
        /// MongoServerAddress
        /// MongReplicaSetName
        /// 
        /// </summary>  
        /// <returns>数据库连接字符串</returns>  
        private static MongoDatabase GetDatabaseFromAppSettings(string databaseName)
        {
            List<MongoServerAddress> servers = new List<MongoServerAddress>();
            string reg = @"^(?'server'\d{1,}.\d{1,}.\d{1,}.\d{1,}):(?'port'\d{1,})$";
            string[] ServerList = ConfigurationManager.AppSettings["MongoServerAddress"].Trim().Split('|');
            foreach (string server in ServerList)
            {
                MatchCollection mc = Regex.Matches(server, reg);
                if (mc != null && mc.Count > 0)
                    servers.Add(new MongoServerAddress(mc[0].Groups["server"].ToString(), Convert.ToInt32(mc[0].Groups["port"].ToString())));
            }

            if (servers == null || servers.Count < 1)
                return null;

            MongoClientSettings set = new MongoClientSettings();

            set.Servers = servers;

            set.ReplicaSetName = ConfigurationManager.AppSettings["MongReplicaSetName"].Trim();//设置副本集名称  

            int TimeOut = ConfigurationManager.AppSettings["TimeOut"].ParseInt();//设置副本集名称  

            set.ConnectTimeout = new TimeSpan(0, 0, 0, TimeOut, 0);//设置超时时间为5秒  

            set.ReadPreference = new ReadPreference(ReadPreferenceMode.SecondaryPreferred);

            MongoClient client = new MongoClient(set);

            return client.GetServer().GetDatabase(databaseName);
        }


        public static MongoCollection<T> GetCollectionFromCluster<T>(string databaseName) 
            where T : IMongoEntity<U>
        {
            return GetDatabaseFromAppSettings(databaseName).GetCollection<T>(GetCollectionName<T>());
        }

        public static MongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString)
            where T : IMongoEntity<U>
        {
            return Extentions<U>.GetCollectionFromConnectionString<T>(connectionString, GetCollectionName<T>());
        }

        public static MongoCollection<T> GetCollectionFromConnectionString<T>(string connectionString, string collectionName)
            where T : IMongoEntity<U>
        {
            return Extentions<U>.GetDatabaseFromUrl(new MongoUrl(connectionString))
                .GetCollection<T>(collectionName);
        }

        public static MongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url)
            where T : IMongoEntity<U>
        {
            return Extentions<U>.GetCollectionFromUrl<T>(url, GetCollectionName<T>());
        }

        public static MongoCollection<T> GetCollectionFromUrl<T>(MongoUrl url, string collectionName)
            where T : IMongoEntity<U>
        {
            return Extentions<U>.GetDatabaseFromUrl(url)
                .GetCollection<T>(collectionName);
        }

        private static string GetCollectionName<T>() where T : IMongoEntity<U>
        {
            string collectionName;
            if (typeof(T).BaseType.Equals(typeof(object)))
            {
                collectionName = GetCollectioNameFromInterface<T>();
            }
            else
            {
                collectionName = GetCollectionNameFromType(typeof(T));
            }

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }
            return collectionName;
        }

        private static string GetCollectioNameFromInterface<T>()
        {
            string collectionname;

            // 查看对象（实体继承）有一个collectionname属性
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionBase));
            if (att != null)
            {
                // 返回由collectionname属性指定的值
                collectionname = ((CollectionNameAttribute)att).Name;
            }
            else
            {
                collectionname = typeof(T).Name;
            }

            return collectionname;
        }

        /// <summary>
        /// 从指定CollectionName确定类型。
        /// </summary>
        /// <param name="entitytype"></param>
        /// <returns>返回指定类型的collectionname.</returns>
        private static string GetCollectionNameFromType(Type entitytype)
        {
            string collectionname;

            var att = Attribute.GetCustomAttribute(entitytype, typeof(CollectionNameAttribute));
            if (att != null)
            {
                collectionname = ((CollectionNameAttribute)att).Name;
            }
            else
            {
                if (typeof(MongoEntity).IsAssignableFrom(entitytype))
                {
                    while (!entitytype.BaseType.Equals(typeof(MongoEntity)))
                    {
                        entitytype = entitytype.BaseType;
                    }
                }
                collectionname = entitytype.Name;
            }

            return collectionname;
        }

      
    }

    internal static class Extentions
    {
        public static int ParseInt(this string str)
        {
            int result = 0;

            if (!int.TryParse(str, out result))
            {
                result = 0;
            }
            return result;
        }
    }
}