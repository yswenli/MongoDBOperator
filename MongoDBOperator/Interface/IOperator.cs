﻿/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：819bcc0e-1043-4dc0-9add-9c9662854e99
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：MongoDBOperator.Interface
 * 类名称：IOperator
 * 创建时间：2017/7/13 14:42:42
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB.Driver;

namespace MongoDBOperator.Interface
{
    public interface IOperator<T, TKey> : IQueryable<T>
        where T : IMongoEntity<TKey>
    {
        MongoCollection<T> Collection
        {
            get;
        }

        T GetById(TKey id);

        T Add(T entity);

        void Add(IEnumerable<T> entities);

        T Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(TKey id);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> predicate);

        void DeleteAll();


        long Count();

        bool Exists(Expression<Func<T, bool>> predicate);
    }

    public interface IOperator<T> : IQueryable<T>, IOperator<T, string>
        where T : IMongoEntity<string>
    {
    }
}