﻿/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：f3a48d99-dc41-4514-afcd-4060c390d91f
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：MongoDBOperator
 * 类名称：MongoOperatorManager
 * 创建时间：2017/7/13 14:54:21
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDBOperator.Extention;
using MongoDBOperator.Interface;

namespace MongoDBOperator
{
    public class MongoOperatorManager<T, TKey> : IOperatorManager<T, TKey>
        where T : IMongoEntity<TKey>
    {

        private MongoCollection<T> collection;

        public MongoOperatorManager()
            : this(Extentions<TKey>.GetDefaultConnectionString())
        {
        }


        public MongoOperatorManager(string connectionString)
        {
            this.collection = Extentions<TKey>.GetCollectionFromConnectionString<T>(connectionString);
        }


        public MongoOperatorManager(string connectionString, string collectionName)
        {
            this.collection = Extentions<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }

        public virtual bool Exists
        {
            get
            {
                return this.collection.Exists();
            }
        }


        public virtual string Name
        {
            get
            {
                return this.collection.Name;
            }
        }


        public virtual void Drop()
        {
            this.collection.Drop();
        }


        public virtual bool IsCapped()
        {
            return this.collection.IsCapped();
        }


        public virtual void DropIndex(string keyname)
        {
            this.DropIndexes(new string[] { keyname });
        }


        public virtual void DropIndexes(IEnumerable<string> keynames)
        {
            this.collection.DropIndex(keynames.ToArray());
        }


        public virtual void DropAllIndexes()
        {
            this.collection.DropAllIndexes();
        }

        /// <summary>
        /// 确保所需索引存在并在不存在时创建它
        /// </summary>
        /// <param name="keyname"></param>
        public virtual void EnsureIndex(string keyname)
        {
            this.EnsureIndexes(new string[] { keyname });
        }

        /// <summary>
        /// 确保所需索引存在并在不存在时创建它
        /// </summary>
        /// <param name="keyname"></param>
        /// <param name="descending"></param>
        /// <param name="unique"></param>
        /// <param name="sparse"></param>
        public virtual void EnsureIndex(string keyname, bool descending, bool unique, bool sparse)
        {
            this.EnsureIndexes(new string[] { keyname }, descending, unique, sparse);
        }

        /// <summary>
        /// 确保所需索引存在并在不存在时创建它
        /// </summary>
        /// <param name="keynames"></param>
        public virtual void EnsureIndexes(IEnumerable<string> keynames)
        {
            this.EnsureIndexes(keynames, false, false, false);
        }

        /// <summary>
        /// 确保所需索引存在并在不存在时创建它
        /// </summary>
        /// <param name="keynames"></param>
        /// <param name="descending"></param>
        /// <param name="unique"></param>
        /// <param name="sparse"></param>
        public virtual void EnsureIndexes(IEnumerable<string> keynames, bool descending, bool unique, bool sparse)
        {
            var ixk = new IndexKeysBuilder();
            if (descending)
            {
                ixk.Descending(keynames.ToArray());
            }
            else
            {
                ixk.Ascending(keynames.ToArray());
            }

            this.EnsureIndexes(
                ixk,
                new IndexOptionsBuilder().SetUnique(unique).SetSparse(sparse));
        }

        /// <summary>
        /// 确保所需索引存在并在不存在时创建它
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="options"></param>
        public virtual void EnsureIndexes(IMongoIndexKeys keys, IMongoIndexOptions options)
        {
            this.collection.CreateIndex(keys, options);
        }

        /// <summary>
        /// 测试索引是否存在
        /// </summary>
        /// <param name="keyname"></param>
        /// <returns></returns>
        public virtual bool IndexExists(string keyname)
        {
            return this.IndexesExists(new string[] { keyname });
        }

        /// <summary>
        /// 测试索引是否存在
        /// </summary>
        /// <param name="keynames"></param>
        /// <returns></returns>
        public virtual bool IndexesExists(IEnumerable<string> keynames)
        {
            return this.collection.IndexExists(keynames.ToArray());
        }

        /// <summary>
        /// 测试索引是否存在
        /// </summary>
        public virtual void ReIndex()
        {
            this.collection.ReIndex();
        }


        /// <summary>
        /// 验证存储库的完整性。
        /// </summary>
        /// <returns></returns>
        public virtual ValidateCollectionResult Validate()
        {
            return this.collection.Validate();
        }

        public virtual CollectionStatsResult GetStats()
        {
            return this.collection.GetStats();
        }


        public virtual GetIndexesResult GetIndexes()
        {
            return this.collection.GetIndexes();
        }
    }

    public class MongoOperatorManager<T> : MongoOperatorManager<T, string>, IOperatorManager<T>
        where T : IMongoEntity<string>
    {

        public MongoOperatorManager()
            : base() { }


        public MongoOperatorManager(string connectionString)
            : base(connectionString) { }

        public MongoOperatorManager(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }
    }
}