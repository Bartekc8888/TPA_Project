using Microsoft.Win32;
using ViewModel.Logic;

namespace GUI
{
    public class DialogFileChooser : IFileChooser
    {
        public string ChooseFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = ".dll",
                Filter = "Executable Files (*.exe)|*.exe|DLL Files (*.dll)|*.dll"
            };

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                return dialog.FileName;
            }

            return "";
        }
    }
}