using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace es.Model {
	[JsonObject(MemberSerialization.OptIn)]
	public partial class Goods_tagInfo {
		#region fields
		private int? _Goods_id;
		private GoodsInfo _obj_goods;
		private int? _Tag_id;
		private TagInfo _obj_tag;
		#endregion

		public Goods_tagInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Goods_tag(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Goods_id == null ? "null" : _Goods_id.ToString(), "|",
				_Tag_id == null ? "null" : _Tag_id.ToString());
		}
		public static Goods_tagInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，Goods_tagInfo：{stringify}");
			Goods_tagInfo item = new Goods_tagInfo();
			if (string.Compare("null", ret[0]) != 0) item.Goods_id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Tag_id = int.Parse(ret[1]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Goods_tagInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Goods_id") ? string.Empty : string.Format(", Goods_id : {0}", Goods_id == null ? "null" : Goods_id.ToString()), 
				__jsonIgnore.ContainsKey("Tag_id") ? string.Empty : string.Format(", Tag_id : {0}", Tag_id == null ? "null" : Tag_id.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("Goods_id")) ht["Goods_id"] = Goods_id;
			if (!__jsonIgnore.ContainsKey("Tag_id")) ht["Tag_id"] = Tag_id;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		/// <summary>
		/// 产品id
		/// </summary>
		[JsonProperty] public int? Goods_id {
			get { return _Goods_id; }
			set {
				if (_Goods_id != value) _obj_goods = null;
				_Goods_id = value;
			}
		}
		public GoodsInfo Obj_goods {
			get {
				if (_obj_goods == null && _Goods_id != null) _obj_goods = es.BLL.Goods.GetItem(_Goods_id.Value);
				return _obj_goods;
			}
			internal set { _obj_goods = value; }
		}
		/// <summary>
		/// 标签id
		/// </summary>
		[JsonProperty] public int? Tag_id {
			get { return _Tag_id; }
			set {
				if (_Tag_id != value) _obj_tag = null;
				_Tag_id = value;
			}
		}
		public TagInfo Obj_tag {
			get {
				if (_obj_tag == null && _Tag_id != null) _obj_tag = es.BLL.Tag.GetItem(_Tag_id.Value);
				return _obj_tag;
			}
			internal set { _obj_tag = value; }
		}
		#endregion

		public es.DAL.Goods_tag.SqlUpdateBuild UpdateDiy => _Goods_id == null || _Tag_id == null ? null : BLL.Goods_tag.UpdateDiy(new List<Goods_tagInfo> { this });

		#region sync methods

		#endregion

		#region async methods

		#endregion
	}
}
