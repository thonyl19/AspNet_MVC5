using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Utility
{
	public static class Json
	{
		public static string ToJson(this object obj, JsonSerializerSettings settings = null)
		{
			settings = settings ?? Json.SerializerSetting;
			return JsonConvert.SerializeObject(obj, settings);
		}
		/// <summary>
		/// Json 通用的序列化設定
		/// </summary>
		public static JsonSerializerSettings SerializerSetting = new JsonSerializerSettings()
		{
			DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
			DateFormatString = "yyyy-MM-dd HH:mm:ss",
			NullValueHandling = NullValueHandling.Ignore,
			Error = (serializer, err) =>
			{
				err.ErrorContext.Handled = true;

			}
		};

		/// <summary>
		/// 資料物件轉換程序,詳細說明參見註解
		/// </summary>
		/// <typeparam name="T">指定要轉換的對象型別</typeparam>
		/// <param name="src">被轉換的資料型別</param>
		/// <param name="IgnoreErr">是否忽略型別不符的錯誤 T:忽略(def) F:拋出異常</param>
		/// <returns></returns>
		/// SPEC) Anthony - 2020/02/10
		/// 此程序的需求源自於 希望比照 2015版,在 LotInfo.Operation 資料時,
		///		可以呈現 [Operation.NO]~Operation.Name 的格式 .
		///	但是因為 LotInfo 物件的欄位,是以 get/set 方式,對 DataRow 做存取的,
		///		因此在對 Operation 欄位做變更時,會出現 存取失敗的 issue.
		///	此因才會開發這個轉換程序,讓 LotInfo 的資料 (來源,以下簡稱為A) ,
		///		可以搬到 相對應的 MDL.MES.WP_LOT (要轉換的對象型別,以下簡稱為B),
		///		以此滿足類似的存取需求.
		///	但這個程序有以下三個限制,就是 
		///		1.以 B 為主, 不具備 把 A 有而 B 沒有的欄位,移植過來的功能
		///		2.只有欄位名稱,欄位型別一致的情形下,A 欄位的資料才會被轉換 到 B的對應欄位.
		///		3.除了第 2 點,還有一個情形會出現無法正常轉換 , 那就是 A欄位在取資料時,
		///			就出現了異常,程序處理 這類無法正常轉換的問題,預設都是設成 null 並忽略錯誤,
		///			如果需要確實掌握轉換的情形,避免出現莫明空值的問題,
		///			可以選擇傳入 IgnoreErr = false
		public static T TransTo<T>(this object src, bool IgnoreErr = true) where T : new()
		{
			Type _src = src.GetType();
			T targetObj = new T();
			Type _target = targetObj.GetType();

			Dictionary<string, object> _srcDC
					= _src
					.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
					.ToLookup(t => t.Name, t => t)
					.ToDictionary(p => p.Key, p =>
					{
						try
						{
							return p.First().GetValue(src);
						}
						catch (Exception Ex)
						{
							if (!IgnoreErr) throw Ex;
						}
						return null;
					});

			PropertyInfo[] _targetFileds
					= _target
					.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			Parallel.ForEach(_targetFileds, (field) =>
			{
				object _val = null;
				if (_srcDC.TryGetValue(field.Name, out _val))
				{
					try
					{
						field.SetValue(targetObj, _val);
					}
					catch (Exception Ex)
					{
						if (!IgnoreErr) throw Ex;
					}
				}
			});
			return targetObj;
		}
	}

}
