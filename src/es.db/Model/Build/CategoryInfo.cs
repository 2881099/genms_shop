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
	public partial class CategoryInfo {
		#region fields
		private int? _Id;
		private int? _Parent_id;
		private CategoryInfo _obj_category;
		private DateTime? _Create_time;
		private string _Name;
		#endregion

		public CategoryInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Category(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_Parent_id == null ? "null" : _Parent_id.ToString(), "|",
				_Create_time == null ? "null" : _Create_time.Value.Ticks.ToString(), "|",
				_Name == null ? "null" : _Name.Replace("|", StringifySplit));
		}
		public static CategoryInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 4, StringSplitOptions.None);
			if (ret.Length != 4) throw new Exception($"格式不正确，CategoryInfo：{stringify}");
			CategoryInfo item = new CategoryInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.Parent_id = int.Parse(ret[1]);
			if (string.Compare("null", ret[2]) != 0) item.Create_time = new DateTime(long.Parse(ret[2]));
			if (string.Compare("null", ret[3]) != 0) item.Name = ret[3].Replace(StringifySplit, "|");
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(CategoryInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("Parent_id") ? string.Empty : string.Format(", Parent_id : {0}", Parent_id == null ? "null" : Parent_id.ToString()), 
				__jsonIgnore.ContainsKey("Create_time") ? string.Empty : string.Format(", Create_time : {0}", Create_time == null ? "null" : string.Concat("", Create_time.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("Name") ? string.Empty : string.Format(", Name : {0}", Name == null ? "null" : string.Format("'{0}'", Name.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (!__jsonIgnore.ContainsKey("Parent_id")) ht["Parent_id"] = Parent_id;
			if (!__jsonIgnore.ContainsKey("Create_time")) ht["Create_time"] = Create_time;
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
		/// 分类id（自增）
		/// </summary>
		[JsonProperty] public int? Id {
			get { return _Id; }
			set { _Id = value; }
		}
		/// <summary>
		/// 父级分类id
		/// </summary>
		[JsonProperty] public int? Parent_id {
			get { return _Parent_id; }
			set {
				if (_Parent_id != value) _obj_category = null;
				_Parent_id = value;
			}
		}
		public CategoryInfo Obj_category {
			get {
				if (_obj_category == null && _Parent_id != null) _obj_category = es.BLL.Category.GetItem(_Parent_id.Value);
				return _obj_category;
			}
			internal set { _obj_category = value; }
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		[JsonProperty] public DateTime? Create_time {
			get { return _Create_time; }
			set { _Create_time = value; }
		}
		/// <summary>
		/// 分类名称
		/// </summary>
		[JsonProperty] public string Name {
			get { return _Name; }
			set { _Name = value; }
		}
		private List<CategoryInfo> _obj_categorys;
		public List<CategoryInfo> Obj_categorys => _obj_categorys ?? (_obj_categorys = BLL.Category.SelectByParent_id(_Id).Limit(500).ToList());
		private List<GoodsInfo> _obj_goodss;
		public List<GoodsInfo> Obj_goodss => _obj_goodss ?? (_obj_goodss = BLL.Goods.SelectByCategory_id(_Id).Limit(500).ToList());
		#endregion

		public es.DAL.Category.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Category.UpdateDiy(new List<CategoryInfo> { this });

		#region sync methods

		public CategoryInfo Save() {
			if (this.Id != null) {
				if (BLL.Category.Update(this) == 0) return BLL.Category.Insert(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return BLL.Category.Insert(this);
		}
		public CategoryInfo AddCategory(string Name) => AddCategory(new CategoryInfo {
			Name = Name});
		public CategoryInfo AddCategory(CategoryInfo item) {
			item.Parent_id = this.Id;
			return BLL.Category.Insert(item);
		}

		public GoodsInfo AddGoods(string Content, string Imgs, int? Stock, string Title) => AddGoods(new GoodsInfo {
			Content = Content, 
			Imgs = Imgs, 
			Stock = Stock, 
			Title = Title});
		public GoodsInfo AddGoods(GoodsInfo item) {
			item.Category_id = this.Id;
			return BLL.Goods.Insert(item);
		}

		#endregion

		#region async methods

		async public Task<CategoryInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Category.UpdateAsync(this) == 0) return await BLL.Category.InsertAsync(this);
				return this;
			}
			this.Create_time = DateTime.Now;
			return await BLL.Category.InsertAsync(this);
		}
		async public Task<CategoryInfo> AddCategoryAsync(string Name) => await AddCategoryAsync(new CategoryInfo {
			Name = Name});
		async public Task<CategoryInfo> AddCategoryAsync(CategoryInfo item) {
			item.Parent_id = this.Id;
			return await BLL.Category.InsertAsync(item);
		}

		async public Task<GoodsInfo> AddGoodsAsync(string Content, string Imgs, int? Stock, string Title) => await AddGoodsAsync(new GoodsInfo {
			Content = Content, 
			Imgs = Imgs, 
			Stock = Stock, 
			Title = Title});
		async public Task<GoodsInfo> AddGoodsAsync(GoodsInfo item) {
			item.Category_id = this.Id;
			return await BLL.Goods.InsertAsync(item);
		}

		#endregion
	}
}
