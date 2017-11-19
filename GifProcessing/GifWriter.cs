using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace GifProcessing
{
	/// <summary>
	/// Content writer for gifs.
	/// </summary>
	public class GifWriter : ContentTypeWriter<GifData>
	{
		/// <summary>
		/// Gets runtime information.
		/// </summary>
		public override string GetRuntimeReader(TargetPlatform targetPlatform)
		{
			//return typeof(GifReader).AssemblyQualifiedName;
			return null;
		}

		/// <summary>
		/// Writes gif data.
		/// </summary>
		protected override void Write(ContentWriter output, GifData value)
		{
			output.Write(value.Frames.Length);

			foreach (TextureData data in value.Frames)
			{
				output.Write((int)data.SurfaceFormat);
				output.Write(data.Width);
				output.Write(data.Height);
				output.Write(data.Levels);
				output.Write(data.Data.Length);
				output.Write(data.Data);
				output.Flush();
			}
		}
	}
}
