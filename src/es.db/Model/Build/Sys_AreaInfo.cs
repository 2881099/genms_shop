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
	public partial class Sys_AreaInfo {
		#region fields
		private string _F_Id;
		private DateTime? _F_CreatorTime;
		private string _F_CreatorUserId;
		private bool? _F_DeleteMark;
		private DateTime? _F_DeleteTime;
		private string _F_DeleteUserId;
		private string _F_Description;
		private bool? _F_EnabledMark;
		private string _F_EnCode;
		private string _F_FullName;
		private DateTime? _F_LastModifyTime;
		private string _F_LastModifyUserId;
		private int? _F_Layers;
		private string _F_ParentId;
		private string _F_SimpleSpelling;
		private int? _F_SortCode;
		#endregion

		public Sys_AreaInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Sys_Area(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_F_Id == null ? "null" : _F_Id.Replace("|", StringifySplit), "|",
				_F_CreatorTime == null ? "null" : _F_CreatorTime.Value.Ticks.ToString(), "|",
				_F_CreatorUserId == null ? "null" : _F_CreatorUserId.Replace("|", StringifySplit), "|",
				_F_DeleteMark == null ? "null" : (_F_DeleteMark == true ? "1" : "0"), "|",
				_F_DeleteTime == null ? "null" : _F_DeleteTime.Value.Ticks.ToString(), "|",
				_F_DeleteUserId == null ? "null" : _F_DeleteUserId.Replace("|", StringifySplit), "|",
				_F_Description == null ? "null" : _F_Description.Replace("|", StringifySplit), "|",
				_F_EnabledMark == null ? "null" : (_F_EnabledMark == true ? "1" : "0"), "|",
				_F_EnCode == null ? "null" : _F_EnCode.Replace("|", StringifySplit), "|",
				_F_FullName == null ? "null" : _F_FullName.Replace("|", StringifySplit), "|",
				_F_LastModifyTime == null ? "null" : _F_LastModifyTime.Value.Ticks.ToString(), "|",
				_F_LastModifyUserId == null ? "null" : _F_LastModifyUserId.Replace("|", StringifySplit), "|",
				_F_Layers == null ? "null" : _F_Layers.ToString(), "|",
				_F_ParentId == null ? "null" : _F_ParentId.Replace("|", StringifySplit), "|",
				_F_SimpleSpelling == null ? "null" : _F_SimpleSpelling.Replace("|", StringifySplit), "|",
				_F_SortCode == null ? "null" : _F_SortCode.ToString());
		}
		public static Sys_AreaInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 16, StringSplitOptions.None);
			if (ret.Length != 16) throw new Exception($"格式不正确，Sys_AreaInfo：{stringify}");
			Sys_AreaInfo item = new Sys_AreaInfo();
			if (string.Compare("null", ret[0]) != 0) item.F_Id = ret[0].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[1]) != 0) item.F_CreatorTime = new DateTime(long.Parse(ret[1]));
			if (string.Compare("null", ret[2]) != 0) item.F_CreatorUserId = ret[2].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[3]) != 0) item.F_DeleteMark = ret[3] == "1";
			if (string.Compare("null", ret[4]) != 0) item.F_DeleteTime = new DateTime(long.Parse(ret[4]));
			if (string.Compare("null", ret[5]) != 0) item.F_DeleteUserId = ret[5].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[6]) != 0) item.F_Description = ret[6].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[7]) != 0) item.F_EnabledMark = ret[7] == "1";
			if (string.Compare("null", ret[8]) != 0) item.F_EnCode = ret[8].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[9]) != 0) item.F_FullName = ret[9].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[10]) != 0) item.F_LastModifyTime = new DateTime(long.Parse(ret[10]));
			if (string.Compare("null", ret[11]) != 0) item.F_LastModifyUserId = ret[11].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[12]) != 0) item.F_Layers = int.Parse(ret[12]);
			if (string.Compare("null", ret[13]) != 0) item.F_ParentId = ret[13].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[14]) != 0) item.F_SimpleSpelling = ret[14].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[15]) != 0) item.F_SortCode = int.Parse(ret[15]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Sys_AreaInfo).GetField("JsonIgnore");
			Dictionary<string, bool> ret = new Dictionary<string, bool>();
			if (field != null) string.Concat(field.GetValue(null)).Split(',').ToList().ForEach(f => {
				if (!string.IsNullOrEmpty(f)) ret[f] = true;
			});
			return ret;
		});
		private static Dictionary<string, bool> __jsonIgnore => __jsonIgnoreLazy.Value;
		public override string ToString() {
			string json = string.Concat(
				__jsonIgnore.ContainsKey("F_Id") ? string.Empty : string.Format(", F_Id : {0}", F_Id == null ? "null" : string.Format("'{0}'", F_Id.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_CreatorTime") ? string.Empty : string.Format(", F_CreatorTime : {0}", F_CreatorTime == null ? "null" : string.Concat("", F_CreatorTime.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("F_CreatorUserId") ? string.Empty : string.Format(", F_CreatorUserId : {0}", F_CreatorUserId == null ? "null" : string.Format("'{0}'", F_CreatorUserId.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_DeleteMark") ? string.Empty : string.Format(", F_DeleteMark : {0}", F_DeleteMark == null ? "null" : (F_DeleteMark == true ? "true" : "false")), 
				__jsonIgnore.ContainsKey("F_DeleteTime") ? string.Empty : string.Format(", F_DeleteTime : {0}", F_DeleteTime == null ? "null" : string.Concat("", F_DeleteTime.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("F_DeleteUserId") ? string.Empty : string.Format(", F_DeleteUserId : {0}", F_DeleteUserId == null ? "null" : string.Format("'{0}'", F_DeleteUserId.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_Description") ? string.Empty : string.Format(", F_Description : {0}", F_Description == null ? "null" : string.Format("'{0}'", F_Description.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_EnabledMark") ? string.Empty : string.Format(", F_EnabledMark : {0}", F_EnabledMark == null ? "null" : (F_EnabledMark == true ? "true" : "false")), 
				__jsonIgnore.ContainsKey("F_EnCode") ? string.Empty : string.Format(", F_EnCode : {0}", F_EnCode == null ? "null" : string.Format("'{0}'", F_EnCode.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_FullName") ? string.Empty : string.Format(", F_FullName : {0}", F_FullName == null ? "null" : string.Format("'{0}'", F_FullName.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_LastModifyTime") ? string.Empty : string.Format(", F_LastModifyTime : {0}", F_LastModifyTime == null ? "null" : string.Concat("", F_LastModifyTime.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("F_LastModifyUserId") ? string.Empty : string.Format(", F_LastModifyUserId : {0}", F_LastModifyUserId == null ? "null" : string.Format("'{0}'", F_LastModifyUserId.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_Layers") ? string.Empty : string.Format(", F_Layers : {0}", F_Layers == null ? "null" : F_Layers.ToString()), 
				__jsonIgnore.ContainsKey("F_ParentId") ? string.Empty : string.Format(", F_ParentId : {0}", F_ParentId == null ? "null" : string.Format("'{0}'", F_ParentId.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_SimpleSpelling") ? string.Empty : string.Format(", F_SimpleSpelling : {0}", F_SimpleSpelling == null ? "null" : string.Format("'{0}'", F_SimpleSpelling.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("F_SortCode") ? string.Empty : string.Format(", F_SortCode : {0}", F_SortCode == null ? "null" : F_SortCode.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("F_Id")) ht["F_Id"] = F_Id;
			if (!__jsonIgnore.ContainsKey("F_CreatorTime")) ht["F_CreatorTime"] = F_CreatorTime;
			if (!__jsonIgnore.ContainsKey("F_CreatorUserId")) ht["F_CreatorUserId"] = F_CreatorUserId;
			if (!__jsonIgnore.ContainsKey("F_DeleteMark")) ht["F_DeleteMark"] = F_DeleteMark;
			if (!__jsonIgnore.ContainsKey("F_DeleteTime")) ht["F_DeleteTime"] = F_DeleteTime;
			if (!__jsonIgnore.ContainsKey("F_DeleteUserId")) ht["F_DeleteUserId"] = F_DeleteUserId;
			if (!__jsonIgnore.ContainsKey("F_Description")) ht["F_Description"] = F_Description;
			if (!__jsonIgnore.ContainsKey("F_EnabledMark")) ht["F_EnabledMark"] = F_EnabledMark;
			if (!__jsonIgnore.ContainsKey("F_EnCode")) ht["F_EnCode"] = F_EnCode;
			if (!__jsonIgnore.ContainsKey("F_FullName")) ht["F_FullName"] = F_FullName;
			if (!__jsonIgnore.ContainsKey("F_LastModifyTime")) ht["F_LastModifyTime"] = F_LastModifyTime;
			if (!__jsonIgnore.ContainsKey("F_LastModifyUserId")) ht["F_LastModifyUserId"] = F_LastModifyUserId;
			if (!__jsonIgnore.ContainsKey("F_Layers")) ht["F_Layers"] = F_Layers;
			if (!__jsonIgnore.ContainsKey("F_ParentId")) ht["F_ParentId"] = F_ParentId;
			if (!__jsonIgnore.ContainsKey("F_SimpleSpelling")) ht["F_SimpleSpelling"] = F_SimpleSpelling;
			if (!__jsonIgnore.ContainsKey("F_SortCode")) ht["F_SortCode"] = F_SortCode;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		/// <summary>
		/// 主键
		/// </summary>
		[JsonProperty] public string F_Id {
			get { return _F_Id; }
			set { _F_Id = value; }
		}

		/// <summary>
		/// 创建日期
		/// </summary>
		[JsonProperty] public DateTime? F_CreatorTime {
			get { return _F_CreatorTime; }
			set { _F_CreatorTime = value; }
		}

		/// <summary>
		/// 创建用户主键
		/// </summary>
		[JsonProperty] public string F_CreatorUserId {
			get { return _F_CreatorUserId; }
			set { _F_CreatorUserId = value; }
		}

		/// <summary>
		/// 删除标志
		/// </summary>
		[JsonProperty] public bool? F_DeleteMark {
			get { return _F_DeleteMark; }
			set { _F_DeleteMark = value; }
		}

		/// <summary>
		/// 删除时间
		/// </summary>
		[JsonProperty] public DateTime? F_DeleteTime {
			get { return _F_DeleteTime; }
			set { _F_DeleteTime = value; }
		}

		/// <summary>
		/// 删除用户
		/// </summary>
		[JsonProperty] public string F_DeleteUserId {
			get { return _F_DeleteUserId; }
			set { _F_DeleteUserId = value; }
		}

		/// <summary>
		/// 描述
		/// </summary>
		[JsonProperty] public string F_Description {
			get { return _F_Description; }
			set { _F_Description = value; }
		}

		/// <summary>
		/// 有效标志
		/// </summary>
		[JsonProperty] public bool? F_EnabledMark {
			get { return _F_EnabledMark; }
			set { _F_EnabledMark = value; }
		}

		/// <summary>
		/// 编码
		/// </summary>
		[JsonProperty] public string F_EnCode {
			get { return _F_EnCode; }
			set { _F_EnCode = value; }
		}

		/// <summary>
		/// 名称
		/// </summary>
		[JsonProperty] public string F_FullName {
			get { return _F_FullName; }
			set { _F_FullName = value; }
		}

		/// <summary>
		/// 最后修改时间
		/// </summary>
		[JsonProperty] public DateTime? F_LastModifyTime {
			get { return _F_LastModifyTime; }
			set { _F_LastModifyTime = value; }
		}

		/// <summary>
		/// 最后修改用户
		/// </summary>
		[JsonProperty] public string F_LastModifyUserId {
			get { return _F_LastModifyUserId; }
			set { _F_LastModifyUserId = value; }
		}

		/// <summary>
		/// 层次
		/// </summary>
		[JsonProperty] public int? F_Layers {
			get { return _F_Layers; }
			set { _F_Layers = value; }
		}

		/// <summary>
		/// 父级
		/// </summary>
		[JsonProperty] public string F_ParentId {
			get { return _F_ParentId; }
			set { _F_ParentId = value; }
		}

		/// <summary>
		/// 简拼
		/// </summary>
		[JsonProperty] public string F_SimpleSpelling {
			get { return _F_SimpleSpelling; }
			set { _F_SimpleSpelling = value; }
		}

		/// <summary>
		/// 排序码
		/// </summary>
		[JsonProperty] public int? F_SortCode {
			get { return _F_SortCode; }
			set { _F_SortCode = value; }
		}

		#endregion

		public es.DAL.Sys_Area.SqlUpdateBuild UpdateDiy => BLL.Sys_Area.UpdateDiy(new List<Sys_AreaInfo> { this });

		#region sync methods

		public Sys_AreaInfo Save() {
			if (this.F_Id != null) {
				if (BLL.Sys_Area.Update(this) == 0) return BLL.Sys_Area.Insert(this);
				return this;
			}
			return BLL.Sys_Area.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<Sys_AreaInfo> SaveAsync() {
			if (this.F_Id != null) {
				if (await BLL.Sys_Area.UpdateAsync(this) == 0) return await BLL.Sys_Area.InsertAsync(this);
				return this;
			}
			return await BLL.Sys_Area.InsertAsync(this);
		}
		#endregion
	}
}
