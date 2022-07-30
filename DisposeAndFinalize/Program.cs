using System;

namespace DisposeAndFinalize
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");

            using (var someClass = new CustomWriter())
            {
                someClass.Write();
            }

            // Push any key for invoke finalize
            Console.ReadKey();
        }
    }

    public class CustomWriter : IDisposable
    {
        private bool _disposed;      

        public void Write()
        {
            // Some unsafe code for write in file
            Console.WriteLine("Write Executed");
        }

        private void CloseUnmanagedRes()
        {
            // Some unsafe code for correct close file
            Console.WriteLine("CloseUnmanagedRes Executed");
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            CloseUnmanagedRes();
            _disposed = true;
            // Commented for show finalize
            //GC.SuppressFinalize(this); 
            Console.WriteLine("Dispose Executed");
        }

        // Start without Debugging for show finalize
        ~CustomWriter()
        {
            CloseUnmanagedRes();
            Console.WriteLine("Finilize Executed");
        }
    }
}
