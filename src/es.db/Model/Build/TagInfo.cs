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
	public partial class TagInfo {
		#region fields
		private int? _Id;
		private string _Name;
		#endregion

		public TagInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Tag(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static TagInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 2, StringSplitOptions.None);
			if (ret.Length != 2) throw new Exception($"格式不正确，TagInfo：{stringify}");
			TagInfo item = new TagInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Name = ret[1].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(TagInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (!__jsonIgnore.ContainsKey("Name")) ht["Name"] = Name;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		/// <summary>
		/// 标签id（自增）
		/// </summary>
		[JsonProperty] public int? Id {
			get { return _Id; }
			set { _Id = value; }
		}
		/// <summary>
		/// 标签名
		/// </summary>
		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}
		private List<GoodsInfo> _obj_goodss;
		public List<GoodsInfo> Obj_goodss => _obj_goodss ?? (_obj_goodss = BLL.Goods.SelectByTag_id(_Id.Value).ToList());
		#endregion

		public es.DAL.Tag.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Tag.UpdateDiy(new List<TagInfo> { this });

		#region sync methods

		public TagInfo Save() {
			if (this.Id != null) {
				if (BLL.Tag.Update(this) == 0) return BLL.Tag.Insert(this);
				return this;
			}
			return BLL.Tag.Insert(this);
		}
		public Goods_tagInfo FlagGoods(GoodsInfo Goods) => FlagGoods(Goods.Id);
		public Goods_tagInfo FlagGoods(int? Goods_id) {
			Goods_tagInfo item = BLL.Goods_tag.GetItem(Goods_id.Value, this.Id.Value);
			if (item == null) item = BLL.Goods_tag.Insert(new Goods_tagInfo {
				Goods_id = Goods_id, 
				Tag_id = this.Id});
			return item;
		}

		public Goods_tagInfo UnflagGoods(GoodsInfo Goods) => UnflagGoods(Goods.Id);
		public Goods_tagInfo UnflagGoods(int? Goods_id) => BLL.Goods_tag.Delete(Goods_id.Value, this.Id.Value);
		public List<Goods_tagInfo> UnflagGoodsALL() => BLL.Goods_tag.DeleteByTag_id(this.Id.Value);

		#endregion

		#region async methods

		async public Task<TagInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Tag.UpdateAsync(this) == 0) return await BLL.Tag.InsertAsync(this);
				return this;
			}
			return await BLL.Tag.InsertAsync(this);
		}
		async public Task<Goods_tagInfo> FlagGoodsAsync(GoodsInfo Goods) => await FlagGoodsAsync(Goods.Id);
		async public Task<Goods_tagInfo> FlagGoodsAsync(int? Goods_id) {
			Goods_tagInfo item = await BLL.Goods_tag.GetItemAsync(Goods_id.Value, this.Id.Value);
			if (item == null) item = await BLL.Goods_tag.InsertAsync(new Goods_tagInfo {
				Goods_id = Goods_id, 
				Tag_id = this.Id});
			return item;
		}

		async public Task<Goods_tagInfo> UnflagGoodsAsync(GoodsInfo Goods) => await UnflagGoodsAsync(Goods.Id);
		async public Task<Goods_tagInfo> UnflagGoodsAsync(int? Goods_id) => await BLL.Goods_tag.DeleteAsync(Goods_id.Value, this.Id.Value);
		async public Task<List<Goods_tagInfo>> UnflagGoodsALLAsync() => await BLL.Goods_tag.DeleteByTag_idAsync(this.Id.Value);

		#endregion
	}
}
