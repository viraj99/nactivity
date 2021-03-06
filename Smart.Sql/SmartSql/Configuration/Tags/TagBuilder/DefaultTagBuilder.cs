﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using SmartSql.Exceptions;

namespace SmartSql.Configuration.Tags.TagBuilder
{
    public class DefaultTagBuilder : ITagBuilder
    {
        public IEnumerable<string> NodeNames => new String[] { "#text", "#cdata-section", "Include", "IsEmpty", "IsEqual", "IsGreaterEqual", "IsGreaterThan", "IsLessEqual", "IsLessThan", "IsNotEmpty", "IsNotEqual", "IsNotNull", "IsNull", "IsTrue", "IsFalse", "IsProperty", "Placeholder", "Switch", "Case", "Default", "Dynamic", "Where", "If", "Set", "For", "Env", "#comment" };
        public ITag Build(XmlNode xmlNode)
        {
            ITag tag = null;
            var prepend = xmlNode.Attributes?["Prepend"]?.Value.Trim();
            var property = xmlNode.Attributes?["Property"]?.Value.Trim();
            var compareValue = xmlNode.Attributes?["CompareValue"]?.Value.Trim();
            #region Init Tag
            switch (xmlNode.Name)
            {
                case "#text":
                case "#cdata-section":
                    {
                        var bodyText = " " + xmlNode.InnerText.Replace("\n", "").Trim();
                        return new SqlText
                        {
                            BodyText = bodyText
                        };
                    }
                case "Include":
                    {
                        var refId = xmlNode.Attributes?["RefId"]?.Value;
                        var include_tag = new Include
                        {
                            RefId = refId,
                            Prepend = prepend
                        };
                        //includes.Add(include_tag);
                        tag = include_tag;
                        break;
                    }
                case "IsEmpty":
                    {
                        tag = new IsEmpty
                        {
                            Prepend = prepend,
                            Property = property,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }

                case "IsEqual":
                    {
                        tag = new IsEqual
                        {
                            Prepend = prepend,
                            Property = property,
                            CompareValue = compareValue,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsGreaterEqual":
                    {
                        tag = new IsGreaterEqual
                        {
                            Prepend = prepend,
                            Property = property,
                            CompareValue = compareValue,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsGreaterThan":
                    {
                        tag = new IsGreaterThan
                        {
                            Prepend = prepend,
                            Property = property,
                            CompareValue = compareValue,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsLessEqual":
                    {
                        tag = new IsLessEqual
                        {
                            Prepend = prepend,
                            Property = property,
                            CompareValue = compareValue,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsLessThan":
                    {
                        tag = new IsLessThan
                        {
                            Prepend = prepend,
                            Property = property,
                            CompareValue = compareValue,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsNotEmpty":
                    {
                        tag = new IsNotEmpty
                        {
                            Prepend = prepend,
                            Property = property,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsNotEqual":
                    {
                        tag = new IsNotEqual
                        {
                            Prepend = prepend,
                            Property = property,
                            CompareValue = compareValue,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsNotNull":
                    {
                        tag = new IsNotNull
                        {
                            Prepend = prepend,
                            Property = property,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsNull":
                    {
                        tag = new IsNull
                        {
                            Prepend = prepend,
                            Property = property,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsTrue":
                    {
                        tag = new IsTrue
                        {
                            Prepend = prepend,
                            Property = property,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsFalse":
                    {
                        tag = new IsFalse
                        {
                            Prepend = prepend,
                            Property = property,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "IsProperty":
                    {
                        tag = new IsProperty
                        {
                            Prepend = prepend,
                            Property = property,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "Placeholder":
                    {
                        tag = new Placeholder
                        {
                            Prepend = prepend,
                            Property = property,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "Switch":
                    {
                        tag = new Switch
                        {
                            Property = property,
                            Prepend = prepend,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "Case":
                    {
                        var switchNode = xmlNode.ParentNode;
                        var switchProperty = switchNode.Attributes?["Property"]?.Value.Trim();
                        var switchPrepend = switchNode.Attributes?["Prepend"]?.Value.Trim();
                        tag = new Switch.Case
                        {
                            CompareValue = compareValue,
                            Property = switchProperty,
                            Prepend = switchPrepend,
                            Test = xmlNode.Attributes?["Test"]?.Value.Trim(),
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "Bind":
                    {
                        tag = new BindTag
                        {
                            Name = xmlNode.Attributes["Name"]?.Value,
                            Value = xmlNode.Attributes["Value"]?.Value,
                        };
                        break;
                    }
                case "Trim":
                    {
                        tag = new TrimTag
                        {
                            Prefix = xmlNode.Attributes["Prefix"]?.Value,
                            PrefixOverrides = xmlNode.Attributes["PrefixOverrides"]?.Value,
                            Suffix = xmlNode.Attributes["Suffix"]?.Value,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "Default":
                    {
                        var switchNode = xmlNode.ParentNode;
                        var switchProperty = switchNode.Attributes?["Property"]?.Value.Trim();
                        var switchPrepend = switchNode.Attributes?["Prepend"]?.Value.Trim();
                        tag = new Switch.Defalut
                        {
                            Property = switchProperty,
                            Prepend = switchPrepend,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "Dynamic":
                    {
                        tag = new Dynamic
                        {
                            Prepend = prepend,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "Where":
                    {
                        tag = new Where
                        {
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "If":
                    {
                        var test = xmlNode.Attributes["Test"]?.Value;
                        tag = new IfTag
                        {
                            Test = test
                        };
                    }
                    break;
                case "Set":
                    {
                        tag = new Set
                        {
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "For":
                    {
                        var open = xmlNode.Attributes?["Open"]?.Value.Trim();
                        var separator = xmlNode.Attributes?["Separator"]?.Value.Trim();
                        var close = xmlNode.Attributes?["Close"]?.Value.Trim();
                        var key = xmlNode.Attributes?["Key"]?.Value.Trim();
                        tag = new For
                        {
                            Prepend = prepend,
                            Property = property,
                            Open = open,
                            Close = close,
                            Separator = separator,
                            Key = key,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "Env":
                    {
                        var dbProvider = xmlNode.Attributes?["DbProvider"]?.Value.Trim();
                        tag = new Env
                        {
                            Prepend = prepend,
                            DbProvider = dbProvider,
                            ChildTags = new List<ITag>()
                        };
                        break;
                    }
                case "#comment": { break; }
                default:
                    {
                        throw new SmartSqlException($"Statement.LoadTag unkonw tagName:{xmlNode.Name}.");
                    };
            }
            #endregion
            return tag;
        }
        public bool Filter(string nodeName)
        {
            return NodeNames.Any(n => n == nodeName);
        }
    }
}
