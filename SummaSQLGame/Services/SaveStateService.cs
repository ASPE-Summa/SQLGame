using Newtonsoft.Json;
using SummaSQLGame.Dialog;
using SummaSQLGame.Models;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SummaSQLGame.Services
{
    public class SaveStateService : ISaveStateService
    {
        private readonly string _jsonPath = @"Assets/SaveState.json";
        private readonly string _basePath = System.AppDomain.CurrentDomain.BaseDirectory;

        public SaveState Load()
        {
            string path = Path.Combine(_basePath, _jsonPath);
            if (!File.Exists(path))
            {
                NameDialog nameDialog = new NameDialog();
                String name = nameDialog.ShowDialog() == true ? nameDialog.ResponseText : "Player";
                return new SaveState(name);
            }
            try
            {
                string jsonString = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<SaveState>(jsonString)!;
            }
            catch (JsonReaderException ex)
            {
                Debug.Write(ex);
                MessageBox.Show("Error bij het lezen van je opgeslagen voortgang. Herstart de applicatie");
                File.Delete(path);
                return new SaveState("");
            }
        }

        public void Save(SaveState state)
        {
            string path = Path.Combine(_basePath, _jsonPath);
            using (StreamWriter sw = File.CreateText(path))
            {
                string contents = JsonConvert.SerializeObject(state, Formatting.Indented);
                sw.Write(contents);
            }
        }
    }
}
