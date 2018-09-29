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
	public partial class CommentInfo {
		#region fields
		private int? _Id;
		private int? _Goods_id;
		private GoodsInfo _obj_goods;
		private string _Content;
		private DateTime? _Create_time;
		private string _Nickname;
		private DateTime? _Update_time;
		#endregion

		public CommentInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Comment(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Goods_id == null ? "null" : _Goods_id.ToString(), "|",
				_Content == null ? "null" : _Content.Replace("|", StringifySplit), "|",
				_Create_time == null ? "null" : _Create_time.Value.Ticks.ToString(), "|",
				_Nickname == null ? "null" : _Nickname.Replace("|", StringifySplit), "|",
				_Update_time == null ? "null" : _Update_time.Value.Ticks.ToString());
		}
		public static CommentInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 6, StringSplitOptions.None);
			if (ret.Length != 6) throw new Exception($"格式不正确，CommentInfo：{stringify}");
			CommentInfo item = new CommentInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Goods_id = int.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.Content = ret[2].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[3]) != 0) item.Create_time = new DateTime(long.Parse(ret[3]));
			if (string.Compare("null", ret[4]) != 0) item.Nickname = ret[4].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[5]) != 0) item.Update_time = new DateTime(long.Parse(ret[5]));
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(CommentInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("Id") ? string.Empty : string.Format(", Id : {0}", Id == null ? "null" : Id.ToString()), 
				__jsonIgnore.ContainsKey("Goods_id") ? string.Empty : string.Format(", Goods_id : {0}", Goods_id == null ? "null" : Goods_id.ToString()), 
				__jsonIgnore.ContainsKey("Content") ? string.Empty : string.Format(", Content : {0}", Content == null ? "null" : string.Format("'{0}'", Content.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Create_time") ? string.Empty : string.Format(", Create_time : {0}", Create_time == null ? "null" : string.Concat("", Create_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("Nickname") ? string.Empty : string.Format(", Nickname : {0}", Nickname == null ? "null" : string.Format("'{0}'", Nickname.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("Update_time") ? string.Empty : string.Format(", Update_time : {0}", Update_time == null ? "null" : string.Concat("", Update_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (!__jsonIgnore.ContainsKey("Goods_id")) ht["Goods_id"] = Goods_id;
			if (!__jsonIgnore.ContainsKey("Content")) ht["Content"] = Content;
			if (!__jsonIgnore.ContainsKey("Create_time")) ht["Create_time"] = Create_time;
			if (!__jsonIgnore.ContainsKey("Nickname")) ht["Nickname"] = Nickname;
			if (!__jsonIgnore.ContainsKey("Update_time")) ht["Update_time"] = Update_time;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		/// <summary>
		/// 评论id（自增）
		/// </summary>
		[JsonProperty] public int? Id {
			get { return _Id; }
			set { _Id = value; }
		}

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
		/// 评论内容
		/// </summary>
		[JsonProperty] public string Content {
			get { return _Content; }
			set { _Content = value; }
		}

		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty] public DateTime? Create_time {
			get { return _Create_time; }
			set { _Create_time = value; }
		}

		/// <summary>
		/// 评论者
		/// </summary>
		[JsonProperty] public string Nickname {
			get { return _Nickname; }
			set { _Nickname = value; }
		}

		/// <summary>
		/// 更新时间
		/// </summary>
		[JsonProperty] public DateTime? Update_time {
			get { return _Update_time; }
			set { _Update_time = value; }
		}

		#endregion

		public es.DAL.Comment.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Comment.UpdateDiy(new List<CommentInfo> { this });

		#region sync methods

		public CommentInfo Save() {
			this.Update_time = DateTime.Now;
			if (this.Id != null) {
				if (BLL.Comment.Update(this) == 0) return BLL.Comment.Insert(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return BLL.Comment.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<CommentInfo> SaveAsync() {
			this.Update_time = DateTime.Now;
			if (this.Id != null) {
				if (await BLL.Comment.UpdateAsync(this) == 0) return await BLL.Comment.InsertAsync(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return await BLL.Comment.InsertAsync(this);
		}
		#endregion
	}
}
