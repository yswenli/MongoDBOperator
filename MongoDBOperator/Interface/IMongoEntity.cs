﻿/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：211fda82-abdd-46a9-b05d-2d2b3c340aa3
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：MongoDBOperator.Interface
 * 类名称：IMongoEntity
 * 创建时间：2017/7/13 14:29:50
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBOperator.Interface
{
    public interface IMongoEntity : IMongoEntity<string>
    {
    }

    public interface IMongoEntity<TKey>
    {
        [BsonId]
        TKey Id
        {
            get; set;
        }
    }
}