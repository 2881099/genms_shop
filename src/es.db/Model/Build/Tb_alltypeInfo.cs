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
	public partial class Tb_alltypeInfo {
		#region fields
		private int? _Id;
		private bool? _TestFieldBool1111;
		private bool? _TestFieldBoolNullable;
		private byte? _TestFieldByte;
		private byte? _TestFieldByteNullable;
		private byte[] _TestFieldBytes;
		private DateTime? _TestFieldDateTime;
		private DateTime? _TestFieldDateTimeNullable;
		private DateTimeOffset? _TestFieldDateTimeNullableOffset;
		private DateTimeOffset? _TestFieldDateTimeOffset;
		private decimal? _TestFieldDecimal;
		private decimal? _TestFieldDecimalNullable;
		private double? _TestFieldDouble;
		private double? _TestFieldDoubleNullable;
		private int? _TestFieldEnum1;
		private int? _TestFieldEnum1Nullable;
		private long? _TestFieldEnum2;
		private long? _TestFieldEnum2Nullable;
		private float? _TestFieldFloat;
		private float? _TestFieldFloatNullable;
		private Guid? _TestFieldGuid;
		private Guid? _TestFieldGuidNullable;
		private int? _TestFieldInt;
		private int? _TestFieldIntNullable;
		private long? _TestFieldLong;
		private byte? _TestFieldSByte;
		private byte? _TestFieldSByteNullable;
		private short? _TestFieldShort;
		private short? _TestFieldShortNullable;
		private string _TestFieldString;
		private TimeSpan? _TestFieldTimeSpan;
		private TimeSpan? _TestFieldTimeSpanNullable;
		private int? _TestFieldUInt;
		private int? _TestFieldUIntNullable;
		private long? _TestFieldULong;
		private long? _TestFieldULongNullable;
		private short? _TestFieldUShort;
		private short? _TestFieldUShortNullable;
		private long? _TestFielLongNullable;
		#endregion

		public Tb_alltypeInfo() { }

		#region 序列化，反序列化
		protected static readonly string StringifySplit = "@<Tb_alltype(Info]?#>";
		public string Stringify() {
			return string.Concat(
				_Id == null ? "null" : _Id.ToString(), "|",
				_TestFieldBool1111 == null ? "null" : (_TestFieldBool1111 == true ? "1" : "0"), "|",
				_TestFieldBoolNullable == null ? "null" : (_TestFieldBoolNullable == true ? "1" : "0"), "|",
				_TestFieldByte == null ? "null" : _TestFieldByte.ToString(), "|",
				_TestFieldByteNullable == null ? "null" : _TestFieldByteNullable.ToString(), "|",
				_TestFieldBytes == null ? "null" : Convert.ToBase64String(_TestFieldBytes), "|",
				_TestFieldDateTime == null ? "null" : _TestFieldDateTime.Value.Ticks.ToString(), "|",
				_TestFieldDateTimeNullable == null ? "null" : _TestFieldDateTimeNullable.Value.Ticks.ToString(), "|",
				_TestFieldDateTimeNullableOffset == null ? "null" : _TestFieldDateTimeNullableOffset.Value.Ticks.ToString(), "|",
				_TestFieldDateTimeOffset == null ? "null" : _TestFieldDateTimeOffset.Value.Ticks.ToString(), "|",
				_TestFieldDecimal == null ? "null" : _TestFieldDecimal.ToString(), "|",
				_TestFieldDecimalNullable == null ? "null" : _TestFieldDecimalNullable.ToString(), "|",
				_TestFieldDouble == null ? "null" : _TestFieldDouble.ToString(), "|",
				_TestFieldDoubleNullable == null ? "null" : _TestFieldDoubleNullable.ToString(), "|",
				_TestFieldEnum1 == null ? "null" : _TestFieldEnum1.ToString(), "|",
				_TestFieldEnum1Nullable == null ? "null" : _TestFieldEnum1Nullable.ToString(), "|",
				_TestFieldEnum2 == null ? "null" : _TestFieldEnum2.ToString(), "|",
				_TestFieldEnum2Nullable == null ? "null" : _TestFieldEnum2Nullable.ToString(), "|",
				_TestFieldFloat == null ? "null" : _TestFieldFloat.ToString(), "|",
				_TestFieldFloatNullable == null ? "null" : _TestFieldFloatNullable.ToString(), "|",
				_TestFieldGuid == null ? "null" : _TestFieldGuid.ToString(), "|",
				_TestFieldGuidNullable == null ? "null" : _TestFieldGuidNullable.ToString(), "|",
				_TestFieldInt == null ? "null" : _TestFieldInt.ToString(), "|",
				_TestFieldIntNullable == null ? "null" : _TestFieldIntNullable.ToString(), "|",
				_TestFieldLong == null ? "null" : _TestFieldLong.ToString(), "|",
				_TestFieldSByte == null ? "null" : _TestFieldSByte.ToString(), "|",
				_TestFieldSByteNullable == null ? "null" : _TestFieldSByteNullable.ToString(), "|",
				_TestFieldShort == null ? "null" : _TestFieldShort.ToString(), "|",
				_TestFieldShortNullable == null ? "null" : _TestFieldShortNullable.ToString(), "|",
				_TestFieldString == null ? "null" : _TestFieldString.Replace("|", StringifySplit), "|",
				_TestFieldTimeSpan == null ? "null" : _TestFieldTimeSpan.Value.Ticks.ToString(), "|",
				_TestFieldTimeSpanNullable == null ? "null" : _TestFieldTimeSpanNullable.Value.Ticks.ToString(), "|",
				_TestFieldUInt == null ? "null" : _TestFieldUInt.ToString(), "|",
				_TestFieldUIntNullable == null ? "null" : _TestFieldUIntNullable.ToString(), "|",
				_TestFieldULong == null ? "null" : _TestFieldULong.ToString(), "|",
				_TestFieldULongNullable == null ? "null" : _TestFieldULongNullable.ToString(), "|",
				_TestFieldUShort == null ? "null" : _TestFieldUShort.ToString(), "|",
				_TestFieldUShortNullable == null ? "null" : _TestFieldUShortNullable.ToString(), "|",
				_TestFielLongNullable == null ? "null" : _TestFielLongNullable.ToString());
		}
		public static Tb_alltypeInfo Parse(string stringify) {
			if (string.IsNullOrEmpty(stringify) || stringify == "null") return null;
			string[] ret = stringify.Split(new char[] { '|' }, 39, StringSplitOptions.None);
			if (ret.Length != 39) throw new Exception($"格式不正确，Tb_alltypeInfo：{stringify}");
			Tb_alltypeInfo item = new Tb_alltypeInfo();
			if (string.Compare("null", ret[0]) != 0) item.Id = int.Parse(ret[0]);
			if (string.Compare("null", ret[1]) != 0) item.TestFieldBool1111 = ret[1] == "1";
			if (string.Compare("null", ret[2]) != 0) item.TestFieldBoolNullable = ret[2] == "1";
			if (string.Compare("null", ret[3]) != 0) item.TestFieldByte = byte.Parse(ret[3]);
			if (string.Compare("null", ret[4]) != 0) item.TestFieldByteNullable = byte.Parse(ret[4]);
			if (string.Compare("null", ret[5]) != 0) item.TestFieldBytes = Convert.FromBase64String(ret[5]);
			if (string.Compare("null", ret[6]) != 0) item.TestFieldDateTime = new DateTime(long.Parse(ret[6]));
			if (string.Compare("null", ret[7]) != 0) item.TestFieldDateTimeNullable = new DateTime(long.Parse(ret[7]));
			if (string.Compare("null", ret[8]) != 0) item.TestFieldDateTimeNullableOffset = new DateTimeOffset(new DateTime(long.Parse(ret[8])));
			if (string.Compare("null", ret[9]) != 0) item.TestFieldDateTimeOffset = new DateTimeOffset(new DateTime(long.Parse(ret[9])));
			if (string.Compare("null", ret[10]) != 0) item.TestFieldDecimal = decimal.Parse(ret[10]);
			if (string.Compare("null", ret[11]) != 0) item.TestFieldDecimalNullable = decimal.Parse(ret[11]);
			if (string.Compare("null", ret[12]) != 0) item.TestFieldDouble = double.Parse(ret[12]);
			if (string.Compare("null", ret[13]) != 0) item.TestFieldDoubleNullable = double.Parse(ret[13]);
			if (string.Compare("null", ret[14]) != 0) item.TestFieldEnum1 = int.Parse(ret[14]);
			if (string.Compare("null", ret[15]) != 0) item.TestFieldEnum1Nullable = int.Parse(ret[15]);
			if (string.Compare("null", ret[16]) != 0) item.TestFieldEnum2 = long.Parse(ret[16]);
			if (string.Compare("null", ret[17]) != 0) item.TestFieldEnum2Nullable = long.Parse(ret[17]);
			if (string.Compare("null", ret[18]) != 0) item.TestFieldFloat = float.Parse(ret[18]);
			if (string.Compare("null", ret[19]) != 0) item.TestFieldFloatNullable = float.Parse(ret[19]);
			if (string.Compare("null", ret[20]) != 0) item.TestFieldGuid = Guid.Parse(ret[20]);
			if (string.Compare("null", ret[21]) != 0) item.TestFieldGuidNullable = Guid.Parse(ret[21]);
			if (string.Compare("null", ret[22]) != 0) item.TestFieldInt = int.Parse(ret[22]);
			if (string.Compare("null", ret[23]) != 0) item.TestFieldIntNullable = int.Parse(ret[23]);
			if (string.Compare("null", ret[24]) != 0) item.TestFieldLong = long.Parse(ret[24]);
			if (string.Compare("null", ret[25]) != 0) item.TestFieldSByte = byte.Parse(ret[25]);
			if (string.Compare("null", ret[26]) != 0) item.TestFieldSByteNullable = byte.Parse(ret[26]);
			if (string.Compare("null", ret[27]) != 0) item.TestFieldShort = short.Parse(ret[27]);
			if (string.Compare("null", ret[28]) != 0) item.TestFieldShortNullable = short.Parse(ret[28]);
			if (string.Compare("null", ret[29]) != 0) item.TestFieldString = ret[29].Replace(StringifySplit, "|");
			if (string.Compare("null", ret[30]) != 0) item.TestFieldTimeSpan = TimeSpan.Parse(ret[30]);
			if (string.Compare("null", ret[31]) != 0) item.TestFieldTimeSpanNullable = TimeSpan.Parse(ret[31]);
			if (string.Compare("null", ret[32]) != 0) item.TestFieldUInt = int.Parse(ret[32]);
			if (string.Compare("null", ret[33]) != 0) item.TestFieldUIntNullable = int.Parse(ret[33]);
			if (string.Compare("null", ret[34]) != 0) item.TestFieldULong = long.Parse(ret[34]);
			if (string.Compare("null", ret[35]) != 0) item.TestFieldULongNullable = long.Parse(ret[35]);
			if (string.Compare("null", ret[36]) != 0) item.TestFieldUShort = short.Parse(ret[36]);
			if (string.Compare("null", ret[37]) != 0) item.TestFieldUShortNullable = short.Parse(ret[37]);
			if (string.Compare("null", ret[38]) != 0) item.TestFielLongNullable = long.Parse(ret[38]);
			return item;
		}
		#endregion

		#region override
		private static Lazy<Dictionary<string, bool>> __jsonIgnoreLazy = new Lazy<Dictionary<string, bool>>(() => {
			FieldInfo field = typeof(Tb_alltypeInfo).GetField("JsonIgnore");
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
				__jsonIgnore.ContainsKey("TestFieldBool1111") ? string.Empty : string.Format(", TestFieldBool1111 : {0}", TestFieldBool1111 == null ? "null" : (TestFieldBool1111 == true ? "true" : "false")), 
				__jsonIgnore.ContainsKey("TestFieldBoolNullable") ? string.Empty : string.Format(", TestFieldBoolNullable : {0}", TestFieldBoolNullable == null ? "null" : (TestFieldBoolNullable == true ? "true" : "false")), 
				__jsonIgnore.ContainsKey("TestFieldByte") ? string.Empty : string.Format(", TestFieldByte : {0}", TestFieldByte == null ? "null" : TestFieldByte.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldByteNullable") ? string.Empty : string.Format(", TestFieldByteNullable : {0}", TestFieldByteNullable == null ? "null" : TestFieldByteNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldBytes") ? string.Empty : string.Format(", TestFieldBytes : {0}", TestFieldBytes == null ? "null" : Convert.ToBase64String(TestFieldBytes)), 
				__jsonIgnore.ContainsKey("TestFieldDateTime") ? string.Empty : string.Format(", TestFieldDateTime : {0}", TestFieldDateTime == null ? "null" : string.Concat("", TestFieldDateTime.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("TestFieldDateTimeNullable") ? string.Empty : string.Format(", TestFieldDateTimeNullable : {0}", TestFieldDateTimeNullable == null ? "null" : string.Concat("", TestFieldDateTimeNullable.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("TestFieldDateTimeNullableOffset") ? string.Empty : string.Format(", TestFieldDateTimeNullableOffset : {0}", TestFieldDateTimeNullableOffset == null ? "null" : string.Concat("", TestFieldDateTimeNullableOffset.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("TestFieldDateTimeOffset") ? string.Empty : string.Format(", TestFieldDateTimeOffset : {0}", TestFieldDateTimeOffset == null ? "null" : string.Concat("", TestFieldDateTimeOffset.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, "")), 
				__jsonIgnore.ContainsKey("TestFieldDecimal") ? string.Empty : string.Format(", TestFieldDecimal : {0}", TestFieldDecimal == null ? "null" : TestFieldDecimal.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldDecimalNullable") ? string.Empty : string.Format(", TestFieldDecimalNullable : {0}", TestFieldDecimalNullable == null ? "null" : TestFieldDecimalNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldDouble") ? string.Empty : string.Format(", TestFieldDouble : {0}", TestFieldDouble == null ? "null" : TestFieldDouble.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldDoubleNullable") ? string.Empty : string.Format(", TestFieldDoubleNullable : {0}", TestFieldDoubleNullable == null ? "null" : TestFieldDoubleNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldEnum1") ? string.Empty : string.Format(", TestFieldEnum1 : {0}", TestFieldEnum1 == null ? "null" : TestFieldEnum1.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldEnum1Nullable") ? string.Empty : string.Format(", TestFieldEnum1Nullable : {0}", TestFieldEnum1Nullable == null ? "null" : TestFieldEnum1Nullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldEnum2") ? string.Empty : string.Format(", TestFieldEnum2 : {0}", TestFieldEnum2 == null ? "null" : TestFieldEnum2.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldEnum2Nullable") ? string.Empty : string.Format(", TestFieldEnum2Nullable : {0}", TestFieldEnum2Nullable == null ? "null" : TestFieldEnum2Nullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldFloat") ? string.Empty : string.Format(", TestFieldFloat : {0}", TestFieldFloat == null ? "null" : TestFieldFloat.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldFloatNullable") ? string.Empty : string.Format(", TestFieldFloatNullable : {0}", TestFieldFloatNullable == null ? "null" : TestFieldFloatNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldGuid") ? string.Empty : string.Format(", TestFieldGuid : {0}", TestFieldGuid == null ? "null" : TestFieldGuid.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldGuidNullable") ? string.Empty : string.Format(", TestFieldGuidNullable : {0}", TestFieldGuidNullable == null ? "null" : TestFieldGuidNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldInt") ? string.Empty : string.Format(", TestFieldInt : {0}", TestFieldInt == null ? "null" : TestFieldInt.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldIntNullable") ? string.Empty : string.Format(", TestFieldIntNullable : {0}", TestFieldIntNullable == null ? "null" : TestFieldIntNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldLong") ? string.Empty : string.Format(", TestFieldLong : {0}", TestFieldLong == null ? "null" : TestFieldLong.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldSByte") ? string.Empty : string.Format(", TestFieldSByte : {0}", TestFieldSByte == null ? "null" : TestFieldSByte.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldSByteNullable") ? string.Empty : string.Format(", TestFieldSByteNullable : {0}", TestFieldSByteNullable == null ? "null" : TestFieldSByteNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldShort") ? string.Empty : string.Format(", TestFieldShort : {0}", TestFieldShort == null ? "null" : TestFieldShort.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldShortNullable") ? string.Empty : string.Format(", TestFieldShortNullable : {0}", TestFieldShortNullable == null ? "null" : TestFieldShortNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldString") ? string.Empty : string.Format(", TestFieldString : {0}", TestFieldString == null ? "null" : string.Format("'{0}'", TestFieldString.Replace("\\", "\\\\").Replace("\r\n", "\\r\\n").Replace("'", "\\'"))), 
				__jsonIgnore.ContainsKey("TestFieldTimeSpan") ? string.Empty : string.Format(", TestFieldTimeSpan : {0}", TestFieldTimeSpan == null ? "null" : string.Concat("", TestFieldTimeSpan.Value.Ticks, "")), 
				__jsonIgnore.ContainsKey("TestFieldTimeSpanNullable") ? string.Empty : string.Format(", TestFieldTimeSpanNullable : {0}", TestFieldTimeSpanNullable == null ? "null" : string.Concat("", TestFieldTimeSpanNullable.Value.Ticks, "")), 
				__jsonIgnore.ContainsKey("TestFieldUInt") ? string.Empty : string.Format(", TestFieldUInt : {0}", TestFieldUInt == null ? "null" : TestFieldUInt.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldUIntNullable") ? string.Empty : string.Format(", TestFieldUIntNullable : {0}", TestFieldUIntNullable == null ? "null" : TestFieldUIntNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldULong") ? string.Empty : string.Format(", TestFieldULong : {0}", TestFieldULong == null ? "null" : TestFieldULong.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldULongNullable") ? string.Empty : string.Format(", TestFieldULongNullable : {0}", TestFieldULongNullable == null ? "null" : TestFieldULongNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldUShort") ? string.Empty : string.Format(", TestFieldUShort : {0}", TestFieldUShort == null ? "null" : TestFieldUShort.ToString()), 
				__jsonIgnore.ContainsKey("TestFieldUShortNullable") ? string.Empty : string.Format(", TestFieldUShortNullable : {0}", TestFieldUShortNullable == null ? "null" : TestFieldUShortNullable.ToString()), 
				__jsonIgnore.ContainsKey("TestFielLongNullable") ? string.Empty : string.Format(", TestFielLongNullable : {0}", TestFielLongNullable == null ? "null" : TestFielLongNullable.ToString()), " }");
			return string.Concat("{", json.Substring(1));
		}
		public IDictionary ToBson(bool allField = false) {
			IDictionary ht = new Hashtable();
			if (!__jsonIgnore.ContainsKey("Id")) ht["Id"] = Id;
			if (!__jsonIgnore.ContainsKey("TestFieldBool1111")) ht["TestFieldBool1111"] = TestFieldBool1111;
			if (!__jsonIgnore.ContainsKey("TestFieldBoolNullable")) ht["TestFieldBoolNullable"] = TestFieldBoolNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldByte")) ht["TestFieldByte"] = TestFieldByte;
			if (!__jsonIgnore.ContainsKey("TestFieldByteNullable")) ht["TestFieldByteNullable"] = TestFieldByteNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldBytes")) ht["TestFieldBytes"] = TestFieldBytes;
			if (!__jsonIgnore.ContainsKey("TestFieldDateTime")) ht["TestFieldDateTime"] = TestFieldDateTime;
			if (!__jsonIgnore.ContainsKey("TestFieldDateTimeNullable")) ht["TestFieldDateTimeNullable"] = TestFieldDateTimeNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldDateTimeNullableOffset")) ht["TestFieldDateTimeNullableOffset"] = TestFieldDateTimeNullableOffset;
			if (!__jsonIgnore.ContainsKey("TestFieldDateTimeOffset")) ht["TestFieldDateTimeOffset"] = TestFieldDateTimeOffset;
			if (!__jsonIgnore.ContainsKey("TestFieldDecimal")) ht["TestFieldDecimal"] = TestFieldDecimal;
			if (!__jsonIgnore.ContainsKey("TestFieldDecimalNullable")) ht["TestFieldDecimalNullable"] = TestFieldDecimalNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldDouble")) ht["TestFieldDouble"] = TestFieldDouble;
			if (!__jsonIgnore.ContainsKey("TestFieldDoubleNullable")) ht["TestFieldDoubleNullable"] = TestFieldDoubleNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldEnum1")) ht["TestFieldEnum1"] = TestFieldEnum1;
			if (!__jsonIgnore.ContainsKey("TestFieldEnum1Nullable")) ht["TestFieldEnum1Nullable"] = TestFieldEnum1Nullable;
			if (!__jsonIgnore.ContainsKey("TestFieldEnum2")) ht["TestFieldEnum2"] = TestFieldEnum2;
			if (!__jsonIgnore.ContainsKey("TestFieldEnum2Nullable")) ht["TestFieldEnum2Nullable"] = TestFieldEnum2Nullable;
			if (!__jsonIgnore.ContainsKey("TestFieldFloat")) ht["TestFieldFloat"] = TestFieldFloat;
			if (!__jsonIgnore.ContainsKey("TestFieldFloatNullable")) ht["TestFieldFloatNullable"] = TestFieldFloatNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldGuid")) ht["TestFieldGuid"] = TestFieldGuid;
			if (!__jsonIgnore.ContainsKey("TestFieldGuidNullable")) ht["TestFieldGuidNullable"] = TestFieldGuidNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldInt")) ht["TestFieldInt"] = TestFieldInt;
			if (!__jsonIgnore.ContainsKey("TestFieldIntNullable")) ht["TestFieldIntNullable"] = TestFieldIntNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldLong")) ht["TestFieldLong"] = TestFieldLong;
			if (!__jsonIgnore.ContainsKey("TestFieldSByte")) ht["TestFieldSByte"] = TestFieldSByte;
			if (!__jsonIgnore.ContainsKey("TestFieldSByteNullable")) ht["TestFieldSByteNullable"] = TestFieldSByteNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldShort")) ht["TestFieldShort"] = TestFieldShort;
			if (!__jsonIgnore.ContainsKey("TestFieldShortNullable")) ht["TestFieldShortNullable"] = TestFieldShortNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldString")) ht["TestFieldString"] = TestFieldString;
			if (!__jsonIgnore.ContainsKey("TestFieldTimeSpan")) ht["TestFieldTimeSpan"] = TestFieldTimeSpan;
			if (!__jsonIgnore.ContainsKey("TestFieldTimeSpanNullable")) ht["TestFieldTimeSpanNullable"] = TestFieldTimeSpanNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldUInt")) ht["TestFieldUInt"] = TestFieldUInt;
			if (!__jsonIgnore.ContainsKey("TestFieldUIntNullable")) ht["TestFieldUIntNullable"] = TestFieldUIntNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldULong")) ht["TestFieldULong"] = TestFieldULong;
			if (!__jsonIgnore.ContainsKey("TestFieldULongNullable")) ht["TestFieldULongNullable"] = TestFieldULongNullable;
			if (!__jsonIgnore.ContainsKey("TestFieldUShort")) ht["TestFieldUShort"] = TestFieldUShort;
			if (!__jsonIgnore.ContainsKey("TestFieldUShortNullable")) ht["TestFieldUShortNullable"] = TestFieldUShortNullable;
			if (!__jsonIgnore.ContainsKey("TestFielLongNullable")) ht["TestFielLongNullable"] = TestFielLongNullable;
			return ht;
		}
		public object this[string key] {
			get { return this.GetType().GetProperty(key).GetValue(this); }
			set { this.GetType().GetProperty(key).SetValue(this, value); }
		}
		#endregion

		#region properties
		[JsonProperty] public int? Id {
			get { return _Id; }
			set { _Id = value; }
		}

		[JsonProperty] public bool? TestFieldBool1111 {
			get { return _TestFieldBool1111; }
			set { _TestFieldBool1111 = value; }
		}

		[JsonProperty] public bool? TestFieldBoolNullable {
			get { return _TestFieldBoolNullable; }
			set { _TestFieldBoolNullable = value; }
		}

		[JsonProperty] public byte? TestFieldByte {
			get { return _TestFieldByte; }
			set { _TestFieldByte = value; }
		}

		[JsonProperty] public byte? TestFieldByteNullable {
			get { return _TestFieldByteNullable; }
			set { _TestFieldByteNullable = value; }
		}

		[JsonProperty] public byte[] TestFieldBytes {
			get { return _TestFieldBytes; }
			set { _TestFieldBytes = value; }
		}

		[JsonProperty] public DateTime? TestFieldDateTime {
			get { return _TestFieldDateTime; }
			set { _TestFieldDateTime = value; }
		}

		[JsonProperty] public DateTime? TestFieldDateTimeNullable {
			get { return _TestFieldDateTimeNullable; }
			set { _TestFieldDateTimeNullable = value; }
		}

		[JsonProperty] public DateTimeOffset? TestFieldDateTimeNullableOffset {
			get { return _TestFieldDateTimeNullableOffset; }
			set { _TestFieldDateTimeNullableOffset = value; }
		}

		[JsonProperty] public DateTimeOffset? TestFieldDateTimeOffset {
			get { return _TestFieldDateTimeOffset; }
			set { _TestFieldDateTimeOffset = value; }
		}

		[JsonProperty] public decimal? TestFieldDecimal {
			get { return _TestFieldDecimal; }
			set { _TestFieldDecimal = value; }
		}

		[JsonProperty] public decimal? TestFieldDecimalNullable {
			get { return _TestFieldDecimalNullable; }
			set { _TestFieldDecimalNullable = value; }
		}

		[JsonProperty] public double? TestFieldDouble {
			get { return _TestFieldDouble; }
			set { _TestFieldDouble = value; }
		}

		[JsonProperty] public double? TestFieldDoubleNullable {
			get { return _TestFieldDoubleNullable; }
			set { _TestFieldDoubleNullable = value; }
		}

		[JsonProperty] public int? TestFieldEnum1 {
			get { return _TestFieldEnum1; }
			set { _TestFieldEnum1 = value; }
		}

		[JsonProperty] public int? TestFieldEnum1Nullable {
			get { return _TestFieldEnum1Nullable; }
			set { _TestFieldEnum1Nullable = value; }
		}

		[JsonProperty] public long? TestFieldEnum2 {
			get { return _TestFieldEnum2; }
			set { _TestFieldEnum2 = value; }
		}

		[JsonProperty] public long? TestFieldEnum2Nullable {
			get { return _TestFieldEnum2Nullable; }
			set { _TestFieldEnum2Nullable = value; }
		}

		[JsonProperty] public float? TestFieldFloat {
			get { return _TestFieldFloat; }
			set { _TestFieldFloat = value; }
		}

		[JsonProperty] public float? TestFieldFloatNullable {
			get { return _TestFieldFloatNullable; }
			set { _TestFieldFloatNullable = value; }
		}

		[JsonProperty] public Guid? TestFieldGuid {
			get { return _TestFieldGuid; }
			set { _TestFieldGuid = value; }
		}

		[JsonProperty] public Guid? TestFieldGuidNullable {
			get { return _TestFieldGuidNullable; }
			set { _TestFieldGuidNullable = value; }
		}

		[JsonProperty] public int? TestFieldInt {
			get { return _TestFieldInt; }
			set { _TestFieldInt = value; }
		}

		[JsonProperty] public int? TestFieldIntNullable {
			get { return _TestFieldIntNullable; }
			set { _TestFieldIntNullable = value; }
		}

		[JsonProperty] public long? TestFieldLong {
			get { return _TestFieldLong; }
			set { _TestFieldLong = value; }
		}

		[JsonProperty] public byte? TestFieldSByte {
			get { return _TestFieldSByte; }
			set { _TestFieldSByte = value; }
		}

		[JsonProperty] public byte? TestFieldSByteNullable {
			get { return _TestFieldSByteNullable; }
			set { _TestFieldSByteNullable = value; }
		}

		[JsonProperty] public short? TestFieldShort {
			get { return _TestFieldShort; }
			set { _TestFieldShort = value; }
		}

		[JsonProperty] public short? TestFieldShortNullable {
			get { return _TestFieldShortNullable; }
			set { _TestFieldShortNullable = value; }
		}

		[JsonProperty] public string TestFieldString {
			get { return _TestFieldString; }
			set { _TestFieldString = value; }
		}

		[JsonProperty] public TimeSpan? TestFieldTimeSpan {
			get { return _TestFieldTimeSpan; }
			set { _TestFieldTimeSpan = value; }
		}

		[JsonProperty] public TimeSpan? TestFieldTimeSpanNullable {
			get { return _TestFieldTimeSpanNullable; }
			set { _TestFieldTimeSpanNullable = value; }
		}

		[JsonProperty] public int? TestFieldUInt {
			get { return _TestFieldUInt; }
			set { _TestFieldUInt = value; }
		}

		[JsonProperty] public int? TestFieldUIntNullable {
			get { return _TestFieldUIntNullable; }
			set { _TestFieldUIntNullable = value; }
		}

		[JsonProperty] public long? TestFieldULong {
			get { return _TestFieldULong; }
			set { _TestFieldULong = value; }
		}

		[JsonProperty] public long? TestFieldULongNullable {
			get { return _TestFieldULongNullable; }
			set { _TestFieldULongNullable = value; }
		}

		[JsonProperty] public short? TestFieldUShort {
			get { return _TestFieldUShort; }
			set { _TestFieldUShort = value; }
		}

		[JsonProperty] public short? TestFieldUShortNullable {
			get { return _TestFieldUShortNullable; }
			set { _TestFieldUShortNullable = value; }
		}

		[JsonProperty] public long? TestFielLongNullable {
			get { return _TestFielLongNullable; }
			set { _TestFielLongNullable = value; }
		}

		#endregion

		public es.DAL.Tb_alltype.SqlUpdateBuild UpdateDiy => _Id == null ? null : BLL.Tb_alltype.UpdateDiy(new List<Tb_alltypeInfo> { this });

		#region sync methods

		public Tb_alltypeInfo Save() {
			if (this.Id != null) {
				if (BLL.Tb_alltype.Update(this) == 0) return BLL.Tb_alltype.Insert(this);
				return this;
			}
			return BLL.Tb_alltype.Insert(this);
		}
		#endregion

		#region async methods

		async public Task<Tb_alltypeInfo> SaveAsync() {
			if (this.Id != null) {
				if (await BLL.Tb_alltype.UpdateAsync(this) == 0) return await BLL.Tb_alltype.InsertAsync(this);
				return this;
			}
			return await BLL.Tb_alltype.InsertAsync(this);
		}
		#endregion
	}
}
