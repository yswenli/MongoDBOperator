﻿/*****************************************************************************************************
 * 本代码版权归@wenli所有，All Rights Reserved (C) 2015-2017
 *****************************************************************************************************
 * CLR版本：4.0.30319.42000
 * 唯一标识：63fcdf18-8930-4a86-93ca-f99f5a020844
 * 机器名称：WENLI-PC
 * 联系人邮箱：wenguoli_520@qq.com
 *****************************************************************************************************
 * 项目名称：$projectname$
 * 命名空间：MongoDBOperator
 * 类名称：CollectionNameAttribute
 * 创建时间：2017/7/13 14:39:44
 * 创建人：wenli
 * 创建说明：
 *****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDBOperator
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class CollectionNameAttribute : Attribute
    {
        /// <summary>
        /// initializes新实例of the收藏类属性with the desired name。
        /// </summary>
        /// <param name="value"></param>
        public CollectionNameAttribute(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("不允许空collection name", "value");
            this.Name = value;
        }

        /// <summary>
        /// 获取集合的名称
        /// </summary>
        /// <value>.</value>
        public virtual string Name
        {
            get; private set;
        }
    }
}