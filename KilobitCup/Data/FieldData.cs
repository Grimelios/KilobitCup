using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KilobitCup.Data
{
	/// <summary>
	/// Enumeration storing field types. Used to parse values appropriately.
	/// </summary>
	public enum FieldTypes
	{
		Integer
	}

	/// <summary>
	/// Data class used to load values from property files.
	/// </summary>
	public class FieldData
	{
		/// <summary>
		/// Constructs the class.
		/// </summary>
		public FieldData(string name, FieldTypes type)
		{
			Name = name;
			Type = type;
		}

		/// <summary>
		/// Field name.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// Field type.
		/// </summary>
		public FieldTypes Type { get; }
	}
}
