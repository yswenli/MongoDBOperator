﻿/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：fbe5fbe8-a6ac-4021-b2cc-c5cab2c18d87
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：MongoDBOperator.Model
 * 类名称：MongoEntity
 * 创建时间：2017/7/13 14:36:39
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDBOperator.Interface;

namespace MongoDBOperator.Model
{
    [DataContract]
    [Serializable]
    [BsonIgnoreExtraElements(Inherited = true)]
    public abstract class MongoEntity : IMongoEntity<string>
    {
        /// <summary>
        /// 获取或设置此对象的ID（实体的主要记录）。
        /// </summary>
        /// <value>此对象的ID（实体的主要记录）。</value>
        [DataMember]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id
        {
            get; set;
        }
    }
}