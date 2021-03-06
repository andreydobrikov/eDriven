#region License

/*
 
Copyright (c) 2012 Danko Kozar

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 
*/

#endregion License

using UnityEngine;

namespace eDriven.Networking.Rpc.Multiplicators
{
    public class ImageLoaderBase : TokenMultiplicator<Texture2D>
    {
        public ImageLoaderBase()
        {
            ResponseExtractor = delegate(WWW response)
            {
                Texture2D texture = new Texture2D(4, 4, TextureFormat.RGB24, true);
                //Texture2D texture = new Texture2D(descriptor.Width, descriptor.Height, textureFormat, descriptor.DoGenerateMipMaps);
                response.LoadImageIntoTexture(texture);

                return texture;
            };

            TokenUpdater = delegate(AsyncToken token, Texture2D texture)
            {
                
            };
        }
    }
}