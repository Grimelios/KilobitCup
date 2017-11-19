using System;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace GifProcessing
{
	/// <summary>
	/// Content processor for gifs.
	/// </summary>
	[ContentProcessor(DisplayName = "Gif Processor")]
	public class GifProcessor : ContentProcessor<GifData, GifData>
	{
		/// <summary>
		/// Processes gif data.
		/// </summary>
		public override GifData Process(GifData input, ContentProcessorContext context)
		{
			return input;
		}
	}
}