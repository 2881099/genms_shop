using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using es.Model;

public static partial class esExtensionMethods {

	public static string ToJson(this CategoryInfo item) => string.Concat(item);
	public static string ToJson(this CategoryInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<CategoryInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this CategoryInfo[] items, Func<CategoryInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<CategoryInfo> items, Func<CategoryInfo, object> func = null) => GetBson(items, func);
	public static es.DAL.Category.SqlUpdateBuild UpdateDiy(this List<CategoryInfo> items) => es.BLL.Category.UpdateDiy(items);

	public static string ToJson(this CommentInfo item) => string.Concat(item);
	public static string ToJson(this CommentInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<CommentInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this CommentInfo[] items, Func<CommentInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<CommentInfo> items, Func<CommentInfo, object> func = null) => GetBson(items, func);
	public static es.DAL.Comment.SqlUpdateBuild UpdateDiy(this List<CommentInfo> items) => es.BLL.Comment.UpdateDiy(items);

	public static string ToJson(this GoodsInfo item) => string.Concat(item);
	public static string ToJson(this GoodsInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<GoodsInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this GoodsInfo[] items, Func<GoodsInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<GoodsInfo> items, Func<GoodsInfo, object> func = null) => GetBson(items, func);
	public static es.DAL.Goods.SqlUpdateBuild UpdateDiy(this List<GoodsInfo> items) => es.BLL.Goods.UpdateDiy(items);

	public static string ToJson(this Goods_tagInfo item) => string.Concat(item);
	public static string ToJson(this Goods_tagInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<Goods_tagInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this Goods_tagInfo[] items, Func<Goods_tagInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<Goods_tagInfo> items, Func<Goods_tagInfo, object> func = null) => GetBson(items, func);
	public static es.DAL.Goods_tag.SqlUpdateBuild UpdateDiy(this List<Goods_tagInfo> items) => es.BLL.Goods_tag.UpdateDiy(items);

	public static string ToJson(this TagInfo item) => string.Concat(item);
	public static string ToJson(this TagInfo[] items) => GetJson(items);
	public static string ToJson(this IEnumerable<TagInfo> items) => GetJson(items);
	public static IDictionary[] ToBson(this TagInfo[] items, Func<TagInfo, object> func = null) => GetBson(items, func);
	public static IDictionary[] ToBson(this IEnumerable<TagInfo> items, Func<TagInfo, object> func = null) => GetBson(items, func);
	public static es.DAL.Tag.SqlUpdateBuild UpdateDiy(this List<TagInfo> items) => es.BLL.Tag.UpdateDiy(items);

	public static string GetJson(IEnumerable items) {
		StringBuilder sb = new StringBuilder();
		sb.Append("[");
		IEnumerator ie = items.GetEnumerator();
		if (ie.MoveNext()) {
			while (true) {
				sb.Append(string.Concat(ie.Current));
				if (ie.MoveNext()) sb.Append(",");
				else break;
			}
		}
		sb.Append("]");
		return sb.ToString();
	}
	public static IDictionary[] GetBson(IEnumerable items, Delegate func = null) {
		List<IDictionary> ret = new List<IDictionary>();
		IEnumerator ie = items.GetEnumerator();
		while (ie.MoveNext()) {
			if (ie.Current == null) ret.Add(null);
			else if (func == null) ret.Add(ie.Current.GetType().GetMethod("ToBson").Invoke(ie.Current, new object[] { false }) as IDictionary);
			else {
				object obj = func.GetMethodInfo().Invoke(func.Target, new object[] { ie.Current });
				if (obj is IDictionary) ret.Add(obj as IDictionary);
				else {
					Hashtable ht = new Hashtable();
					PropertyInfo[] pis = obj.GetType().GetProperties();
					foreach (PropertyInfo pi in pis) ht[pi.Name] = pi.GetValue(obj);
					ret.Add(ht);
				}
			}
		}
		return ret.ToArray();
	}
}