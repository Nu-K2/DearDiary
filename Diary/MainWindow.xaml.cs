using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private readonly NoteManager _noteManager = new NoteManager();

        public MainWindow()
        {
            InitializeComponent();
            DemonstrateTextComponents();
        }

        /// демонстрация работы компонентов
        private void DemonstrateTextComponents()
        {
            _noteManager.AddNote("Заметка №1: купить продукты");
            _noteManager.AddNote("Заметка №2: подготовить отчёт");
            _noteManager.AddNote("Заметка №3: позвонить родителям");

            RefreshListBox();

            string[] demoNotes =
            {
                "Демо: короткая заметка",
                "Демо: заметка с числом 42 и датой 2025-01-01",
                "Демо: длинная заметка, " +
                "которая демонстрирует, что TextBox и ListBox корректно " +
                "работают с многострочным и длинным текстом."
            };

            foreach (var note in demoNotes)
                _noteManager.AddNote(note);

            RefreshListBox();
            UpdateStatus();
        }

        private void RefreshListBox()
        {
            ListBox_Notes.Items.Clear();
            foreach (var note in _noteManager.GetAllNotes())
                ListBox_Notes.Items.Add(note);
        }

        private void UpdateStatus()
        {
            TextBlock_Status.Text = $"Заметок: {_noteManager.Count}";
        }


        private void Button_add_Click(object sender, RoutedEventArgs e)
        {
            string text = TextBox_Note.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Поле ввода пустое. Введите текст заметки.",
                                "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _noteManager.AddNote(text);
            TextBox_Note.Clear();
            RefreshListBox();
            UpdateStatus();

            MessageBox.Show($"Заметка добавлена. Всего заметок: {_noteManager.Count}",
                            "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_show_Click(object sender, RoutedEventArgs e)
        {
            RefreshListBox();
            UpdateStatus();
            MessageBox.Show($"Показаны все заметки. Всего заметок: {_noteManager.Count}",
                            "Список заметок", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_clear_Click(object sender, RoutedEventArgs e)
        {
            TextBox_Note.Clear();
            MessageBox.Show($"Поле ввода очищено. Всего заметок: {_noteManager.Count}",
                            "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_delete_Click(object sender, RoutedEventArgs e)
        {
            _noteManager.ClearAllNotes();
            ListBox_Notes.Items.Clear();
            UpdateStatus();

            MessageBox.Show($"Все заметки удалены. Всего заметок: {_noteManager.Count}",
                            "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public class NoteManager
        {
            private readonly List<string> _notes = new List<string>();

            public int Count => _notes.Count;

            public void AddNote(string text)
            {
                if (!string.IsNullOrWhiteSpace(text))
                    _notes.Add(text.Trim());
            }

            public List<string> GetAllNotes()
            {
                return new List<string>(_notes);
            }

            public void ClearAllNotes()
            {
                _notes.Clear();
            }
        }
    }
}