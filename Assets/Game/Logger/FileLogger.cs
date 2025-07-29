using System;
using System.IO;

public class FileLogger : IDisposable
{
    private StreamWriter _streamWriter;
    private bool _disposed;

    public FileLogger(string filePath)
    {
        _streamWriter = new StreamWriter(filePath, append: true);
    }

    public void Log(string message)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(FileLogger));

        _streamWriter.WriteLine(message);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing)
        {
            // Закрываем и освобождаем StreamWriter
            _streamWriter?.Close();
            _streamWriter?.Dispose();
        }

        _disposed = true;
    }
}