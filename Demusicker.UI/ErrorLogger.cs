using Demusicker.Core;
using System.Diagnostics;

namespace Demusicker.UI;

public class ErrorLogger : IErrorLogger
{
    public void DisplayError(string message)
    {
        Debug.WriteLine(message, "ERROR--");
        MessageBox.Show(message);
    }
}