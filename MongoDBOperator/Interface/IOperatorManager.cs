﻿/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：1e09f8f2-fb86-4786-98b3-6228cf35a1b7
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：MongoDBOperator.Interface
 * 类名称：IOperatorManager
 * 创建时间：2017/7/13 14:52:56
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace MongoDBOperator.Interface
{
    public interface IOperatorManager<T, TKey>
        where T : IMongoEntity<TKey>
    {

        bool Exists
        {
            get;
        }


        string Name
        {
            get;
        }


        void Drop();


        bool IsCapped();


        void DropIndex(string keyname);


        void DropIndexes(IEnumerable<string> keynames);


        void DropAllIndexes();


        void EnsureIndex(string keyname);


        void EnsureIndex(string keyname, bool descending, bool unique, bool sparse);


        void EnsureIndexes(IEnumerable<string> keynames);

        void EnsureIndexes(IEnumerable<string> keynames, bool descending, bool unique, bool sparse);

        void EnsureIndexes(IMongoIndexKeys keys, IMongoIndexOptions options);


        bool IndexExists(string keyname);


        bool IndexesExists(IEnumerable<string> keynames);

        void ReIndex();

        ValidateCollectionResult Validate();

        CollectionStatsResult GetStats();

        GetIndexesResult GetIndexes();
    }

    public interface IOperatorManager<T> : IOperatorManager<T, string>
        where T : IMongoEntity<string>
    {
    }
}