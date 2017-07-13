﻿/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：dd7c4a8d-46b8-40ae-8c3e-0f6cb3824747
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：MongoDBOperator
 * 类名称：MongoOperator
 * 创建时间：2017/7/13 14:43:27
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDBOperator.Extention;
using MongoDBOperator.Interface;
using MongoDBOperator.Model;


namespace MongoDBOperator
{
    /// <summary>
    /// MongoDB实体操作类
    /// </summary>
    /// <typeparam name="T">存储库中包含的类型.</typeparam>
    /// <typeparam name="TKey">用于实体ID的类型。</typeparam>
    public class MongoRepository<T, TKey> : IOperator<T, TKey>
        where T : IMongoEntity<TKey>
    {
        protected internal MongoCollection<T> collection;

        public MongoRepository()
            : this(Extentions<TKey>.GetDefaultConnectionString())
        {
        }

        public MongoRepository(string connectionString)
        {
            this.collection = Extentions<TKey>.GetCollectionFromConnectionString<T>(connectionString);
        }

        public MongoRepository(string connectionString, string collectionName)
        {
            this.collection = Extentions<TKey>.GetCollectionFromConnectionString<T>(connectionString, collectionName);
        }

        public MongoRepository(MongoUrl url)
        {
            this.collection = Extentions<TKey>.GetCollectionFromUrl<T>(url);
        }

        public MongoRepository(MongoUrl url, string collectionName)
        {
            this.collection = Extentions<TKey>.GetCollectionFromUrl<T>(url, collectionName);
        }

        public MongoCollection<T> Collection
        {
            get
            {
                return this.collection;
            }
        }

        public string CollectionName
        {
            get
            {
                return this.collection.Name;
            }
        }

        public virtual T GetById(TKey id)
        {
            if (typeof(T).IsSubclassOf(typeof(MongoEntity)))
            {
                return this.GetById(new ObjectId(id as string));
            }

            return this.collection.FindOneByIdAs<T>(BsonValue.Create(id));
        }

        public virtual T GetById(ObjectId id)
        {
            return this.collection.FindOneByIdAs<T>(id);
        }

        public virtual T Add(T entity)
        {
            this.collection.Insert<T>(entity);

            return entity;
        }


        public virtual void Add(IEnumerable<T> entities)
        {
            this.collection.InsertBatch<T>(entities);
        }

        public virtual T Update(T entity)
        {
            this.collection.Save<T>(entity);

            return entity;
        }


        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.collection.Save<T>(entity);
            }
        }

        public virtual void Delete(TKey id)
        {
            if (typeof(T).IsSubclassOf(typeof(MongoEntity)))
            {
                this.collection.Remove(Query.EQ("_id", new ObjectId(id as string)));
            }
            else
            {
                this.collection.Remove(Query.EQ("_id", BsonValue.Create(id)));
            }
        }

        public virtual void Delete(ObjectId id)
        {
            this.collection.Remove(Query.EQ("_id", id));
        }

        public virtual void Delete(T entity)
        {
            this.Delete(entity.Id);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            foreach (T entity in this.collection.AsQueryable<T>().Where(predicate))
            {
                this.Delete(entity.Id);
            }
        }

        public virtual void DeleteAll()
        {
            this.collection.RemoveAll();
        }

        public virtual long Count()
        {
            return this.collection.Count();
        }


        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return this.collection.AsQueryable<T>().Any(predicate);
        }



        #region IQueryable<T>

        public virtual IEnumerator<T> GetEnumerator()
        {
            return this.collection.AsQueryable<T>().GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.collection.AsQueryable<T>().GetEnumerator();
        }


        public virtual Type ElementType
        {
            get
            {
                return this.collection.AsQueryable<T>().ElementType;
            }
        }

        public virtual Expression Expression
        {
            get
            {
                return this.collection.AsQueryable<T>().Expression;
            }
        }

        public virtual IQueryProvider Provider
        {
            get
            {
                return this.collection.AsQueryable<T>().Provider;
            }
        }
        #endregion
    }


    public class MongoOperator<T> : MongoRepository<T, string>, IOperator<T>
        where T : IMongoEntity<string>
    {

        public MongoOperator()
            : base() { }


        public MongoOperator(MongoUrl url)
            : base(url) { }


        public MongoOperator(MongoUrl url, string collectionName)
            : base(url, collectionName) { }


        public MongoOperator(string connectionString)
            : base(connectionString) { }


        public MongoOperator(string connectionString, string collectionName)
            : base(connectionString, collectionName) { }
    }
}