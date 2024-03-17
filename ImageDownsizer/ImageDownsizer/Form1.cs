using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageDownsizer
{
    public partial class Form1 : Form
    {
        private Image? originalImage;

        private Bitmap? baseImage;
        private Bitmap? downscaledImage;

        private void SetOriginalImage(Image image)
        {
            originalImage = image; 
            pictureBox1.Image = image;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void image_load_btn_Click(object sender, EventArgs e)
        {
        }

        private void newImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadImage(openFileDialog);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async Task LoadImage(OpenFileDialog file)
        {
            Cursor.Current = Cursors.WaitCursor;
            loadingLabel.Visible = true;
            await Task.Run(() =>
            {
                baseImage = new Bitmap(file.FileName);
                pictureBox1.Image = baseImage;
            });
            loadingLabel.Visible = false;
            Cursor.Current = Cursors.Default;
        }

        public static Color[][] BitmapToArray(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            Color[][] result = new Color[height][];
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(data.PixelFormat) / 8;
            int stride = data.Stride;
            IntPtr scan0 = data.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)scan0;

                for (int y = 0; y < height; y++)
                {
                    result[y] = new Color[width];
                    for (int x = 0; x < width; x++)
                    {
                        int idx = (y * stride) + (x * bytesPerPixel);
                        byte b = p[idx];
                        byte g = p[idx + 1];
                        byte r = p[idx + 2];
                        byte a = bytesPerPixel == 4 ? p[idx + 3] : (byte)255;
                        result[y][x] = Color.FromArgb(a, r, g, b);
                    }
                }
            }
            bitmap.UnlockBits(data);

            return result;
        }
        public static Bitmap ResizeImageWithBilinearInterpolation(Bitmap originalBitmap, double scaleFactor)
        {
            scaleFactor /= 100.0; 

            int newWidth = (int)(originalBitmap.Width * scaleFactor);
            int newHeight = (int)(originalBitmap.Height * scaleFactor);
            Bitmap newBitmap = new Bitmap(newWidth, newHeight, originalBitmap.PixelFormat);

            BitmapData originalData = originalBitmap.LockBits(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height), ImageLockMode.ReadOnly, originalBitmap.PixelFormat);
            BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newWidth, newHeight), ImageLockMode.WriteOnly, newBitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(originalBitmap.PixelFormat) / 8;
            byte[] pixelBuffer = new byte[bytesPerPixel]; 

            unsafe
            {
                for (int y = 0; y < newHeight; y++)
                {
                    byte* newRow = (byte*)newData.Scan0 + (y * newData.Stride);
                    for (int x = 0; x < newWidth; x++)
                    {
                        float originalX = (float)(x / scaleFactor);
                        float originalY = (float)(y / scaleFactor);

                        int x1 = (int)originalX;
                        int y1 = (int)originalY;
                        int x2 = Math.Min(x1 + 1, originalBitmap.Width - 1);
                        int y2 = Math.Min(y1 + 1, originalBitmap.Height - 1);

                        byte* top_left = (byte*)originalData.Scan0 + (y1 * originalData.Stride) + (x1 * bytesPerPixel);
                        byte* top_right = (byte*)originalData.Scan0 + (y1 * originalData.Stride) + (x2 * bytesPerPixel);
                        byte* bottom_left = (byte*)originalData.Scan0 + (y2 * originalData.Stride) + (x1 * bytesPerPixel);
                        byte* bottom_right = (byte*)originalData.Scan0 + (y2 * originalData.Stride) + (x2 * bytesPerPixel);

                        float x_diff = originalX - x1;
                        float y_diff = originalY - y1;

                        for (int byteIndex = 0; byteIndex < bytesPerPixel; byteIndex++)
                        {
                            double topLeft = top_left[byteIndex];
                            double topRight = top_right[byteIndex];
                            double bottomLeft = bottom_left[byteIndex];
                            double bottomRight = bottom_right[byteIndex];

                            double top = topLeft + (topRight - topLeft) * x_diff;
                            double bottom = bottomLeft + (bottomRight - bottomLeft) * x_diff;
                            double newValue = top + (bottom - top) * y_diff;

                            pixelBuffer[byteIndex] = (byte)newValue;
                        }

                        byte* newPixel = newRow + (x * bytesPerPixel);
                        for (int k = 0; k < bytesPerPixel; k++)
                        {
                            newPixel[k] = pixelBuffer[k];
                        }
                    }
                }
            }

            originalBitmap.UnlockBits(originalData);
            newBitmap.UnlockBits(newData);

            return newBitmap;
        }

        public static Bitmap ResizeImageWithBilinearInterpolationParallel(Bitmap originalBitmap, double scaleFactor)
        {
            scaleFactor /= 100.0;

            int originalWidth = originalBitmap.Width;
            int originalHeight = originalBitmap.Height;

            int newWidth = (int)(originalWidth * scaleFactor);
            int newHeight = (int)(originalHeight * scaleFactor);
            Bitmap newBitmap = new Bitmap(newWidth, newHeight, originalBitmap.PixelFormat);

            BitmapData originalData = originalBitmap.LockBits(new Rectangle(0, 0, originalWidth, originalHeight), ImageLockMode.ReadOnly, originalBitmap.PixelFormat);
            BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newWidth, newHeight), ImageLockMode.WriteOnly, newBitmap.PixelFormat);

            int bytesPerPixel = Image.GetPixelFormatSize(originalBitmap.PixelFormat) / 8;
            int originalStride = originalData.Stride;
            int newStride = newData.Stride;

            unsafe
            {
                byte* originalPtr = (byte*)originalData.Scan0;
                byte* newPtr = (byte*)newData.Scan0;

                Parallel.For(0, newHeight, y =>
                {
                    for (int x = 0; x < newWidth; x++)
                    {
                        float originalX = (float)(x / scaleFactor);
                        float originalY = (float)(y / scaleFactor);

                        int x1 = (int)originalX;
                        int y1 = (int)originalY;
                        int x2 = Math.Min(x1 + 1, originalWidth - 1); 
                        int y2 = Math.Min(y1 + 1, originalHeight - 1);

                        byte* pTopLeft = originalPtr + (y1 * originalStride) + (x1 * bytesPerPixel);
                        byte* pTopRight = originalPtr + (y1 * originalStride) + (x2 * bytesPerPixel);
                        byte* pBottomLeft = originalPtr + (y2 * originalStride) + (x1 * bytesPerPixel);
                        byte* pBottomRight = originalPtr + (y2 * originalStride) + (x2 * bytesPerPixel);

                        for (int byteIndex = 0; byteIndex < bytesPerPixel; byteIndex++)
                        {
                            double topLeft = pTopLeft[byteIndex];
                            double topRight = pTopRight[byteIndex];
                            double bottomLeft = pBottomLeft[byteIndex];
                            double bottomRight = pBottomRight[byteIndex];

                            float xDiff = originalX - x1;
                            float yDiff = originalY - y1;

                            double value = topLeft * (1 - xDiff) * (1 - yDiff) + topRight * xDiff * (1 - yDiff) +
                                           bottomLeft * (1 - xDiff) * yDiff + bottomRight * xDiff * yDiff;

                            byte* pNewPixel = newPtr + (y * newStride) + (x * bytesPerPixel);
                            pNewPixel[byteIndex] = (byte)(value);
                        }
                    }
                });
            }

            originalBitmap.UnlockBits(originalData);
            newBitmap.UnlockBits(newData);

            return newBitmap;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void scaleFactorNumericField_ValueChanged(object sender, EventArgs e)
        {
        }

        private async void downsizeImageButton_Click(object sender, EventArgs e)
        {
            if (baseImage == null)
            {
                MessageBox.Show("Please select an image to downsize", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (scaleFactorNumericField.Value <= 0 || scaleFactorNumericField.Value > 100)
            {
                MessageBox.Show("Please select valid downscaling factor (0-100)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            loadingLabel.Visible = true;

            Bitmap resultBitmap = null;

            await Task.Run(() =>
            {
                using (Bitmap processingImage = new Bitmap(baseImage))
                {
                    if (parallelRun.Checked)
                    {
                        resultBitmap = ResizeImageWithBilinearInterpolationParallel(processingImage, (double)scaleFactorNumericField.Value);
                    }
                    else
                    {
                        resultBitmap = ResizeImageWithBilinearInterpolation(processingImage, (double)scaleFactorNumericField.Value);
                    }
                }
            });

            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new MethodInvoker(delegate { pictureBox1.Image = resultBitmap; }));
            }
            else
            {
                pictureBox1.Image = resultBitmap;
            }

            loadingLabel.Visible = false;
            Cursor.Current = Cursors.Default;
        }
    }
}
