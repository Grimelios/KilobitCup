using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KilobitCup.Data;

namespace KilobitCup
{
	using PropertyMap = Dictionary<string, string>;
	using PropertyCache = Dictionary<string, Dictionary<string, string>>;

	/// <summary>
	/// Utility class used to load properties.
	/// </summary>
	public static class PropertyLoader
	{
		private static PropertyCache cache = new PropertyCache();

		/// <summary>
		/// Loads values from the given property file.
		/// </summary>
		public static object[] Load(string filename, FieldData[] dataArray)
		{
			PropertyMap map;

			if (!cache.TryGetValue(filename, out map))
			{
				// The map is cached within the LoadMap function.
				map = LoadMap(filename);
			}

			object[] values = new object[dataArray.Length];

			for (int i = 0; i < dataArray.Length; i++)
			{
				values[i] = ParseValue(map, dataArray[i]);
			}

			return values;
		}

		/// <summary>
		/// Loads a property map from the given file.
		/// </summary>
		public static PropertyMap LoadMap(string filename)
		{
			PropertyMap map = new PropertyMap();

			foreach (string line in File.ReadAllLines(Paths.Content + filename))
			{
				if (line.Length == 0)
				{
					continue;
				}

				string[] tokens = line.Split('=');
				string key = tokens[0].TrimEnd();
				string value = tokens[1].TrimStart();

				map.Add(key, value);
			}

			cache.Add(filename, map);

			return map;
		}

		/// <summary>
		/// Parses a map value using the given data.
		/// </summary>
		private static object ParseValue(PropertyMap map, FieldData data)
		{
			string rawValue = map[data.Name];

			switch (data.Type)
			{
				case FieldTypes.Integer:
					return int.Parse(rawValue);
			}

			return null;
		}
	}
}
