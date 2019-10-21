using DecipheringHelp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace DecipheringHelp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel;
        List<FileTreeModel> fileTreeData = new List<FileTreeModel>();
        public static int total = 0;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainWindowViewModel();
            viewModel.Model.SelectFileleName = 0;
            Stackpanel11.DataContext = viewModel;
            //Stackpanel11.DataContext = this;
            Init();
        }
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        private Student stuData;
        public void Init()
        {
            stuData = new Student();
            stuData.ID = 1001;
            stuData.Name = "小明";
            stuData.Age = 19;
            students.Add(stuData);
            //this.DataContext = stuData;//整个窗口内的所有元素都可以绑定此数据  
            test.DataContext = students;//仅stackPanel内的所有元素可以绑定此数据  
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_RemoveSuffix_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_AddSuffix_Click(object sender, RoutedEventArgs e)
        {
            stuData.ID += 1;
            stuData.Name = "小红";
            stuData.Age += 1;
        }
        /// <summary>
        /// 获取目录集合
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public List<FileTreeModel> GetAllFiles(DirectoryInfo dir, FileTreeModel d, int pictureType=0)
        {
            List<FileTreeModel> FileList = new List<FileTreeModel>();
            FileInfo[] allFile = dir.GetFiles();
            total = allFile.Count();
            foreach (FileInfo fi in allFile)
            {
                if (fi.Extension!=null)
                {
                    switch (fi.Extension)
                    {
                        case ".jpg":
                            d.Subitem.Add(new FileTreeModel() { FileName = fi.Name, FilePath = fi.FullName, FileType = (int)FieleTypeEnum.Picture, Icon = "" });
                            break;
                        case ".png":
                            d.Subitem.Add(new FileTreeModel() { FileName = fi.Name, FilePath = fi.FullName, FileType = (int)FieleTypeEnum.Picture, Icon = "" });
                            break;
                        case ".bmp":
                            d.Subitem.Add(new FileTreeModel() { FileName = fi.Name, FilePath = fi.FullName, FileType = (int)FieleTypeEnum.Picture, Icon = "" });
                            break;
                        case ".ini":
                            d.Subitem.Add(new FileTreeModel() { FileName = fi.Name, FilePath = fi.FullName, FileType = (int)FieleTypeEnum.IniFile, Icon = "" });
                            break;
                        default:
                            if(pictureType==0)
                                d.Subitem.Add(new FileTreeModel() { FileName = fi.Name, FilePath = fi.FullName, FileType = (int)FieleTypeEnum.OtherFile, Icon = "" });
                            break;
                    }

                }
            }

            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo dif in allDir)
            {
                FileTreeModel fileDir = new FileTreeModel() { FileName = dif.Name, FilePath = dif.FullName, FileType = (int)FieleTypeEnum.Folder, Icon = "" };
                GetAllFiles(dif, fileDir, pictureType);
                fileDir.SubitemCount = string.Format($"({fileDir.Subitem.Count})");// string.Format($"({total})");//等价于string.Format("{0}",total)
                d.Subitem.Add(fileDir);
                FileList.Add(fileDir);
            }
            return FileList;
        }
        private void Btn_LoadFiles_Click(object sender, RoutedEventArgs e)
        {
            //stuData.ID += 1;
            //stuData.Name = "小红";
            //stuData.Age += 1;
            //students[0] = new Student() { ID= stuData.ID + 1,Name= stuData.Name, Age= stuData.Age + 1};

            //viewModel = new MainWindowViewModel();
            //viewModel.Model.SelectFileleName = 1;
            //Stackpanel11.DataContext = viewModel;
        }

        private void Btn_Click_SendFiles(object sender, RoutedEventArgs e)
        {

        }

        private void Tree_Directory_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (e.AddedItems != null && e.AddedItems.Count > 0)
                {
                    if (e.AddedItems[0] is TabItem)
                    {
                        TabItem tabItem = e.AddedItems[0] as TabItem;
                        if (tabItem.Header.ToString() == "文件预览")
                        {
                            string dataDir = @"D:\项目资料\前端模板";
                            fileTreeData = GetAllFiles(new System.IO.DirectoryInfo(dataDir), new FileTreeModel()).OrderByDescending(s => s.FileName).ToList();
                            this.Tree_Directory.ItemsSource = fileTreeData;
                        }
                        if (tabItem.Header.ToString() == "图片预览")
                        {
                            //string dataDir = AppDomain.CurrentDomain.BaseDirectory + "ImageLogs\\";
                            string dataDir = @"D:\项目资料\前端模板";
                            fileTreeData = GetAllFiles(new System.IO.DirectoryInfo(dataDir), new FileTreeModel(),1).OrderByDescending(s => s.FileName).ToList();
                            this.Tree_Picture.ItemsSource = fileTreeData;
                        }
                    }
                }
            }
        }

        private void Tree_Picture_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Tree_Picture.SelectedItem != null)
                {
                    FileTreeModel selectedTnh = Tree_Picture.SelectedItem as FileTreeModel;
                    //viewModel.Model.SelectFileleName = selectedTnh.FileName;
                    if (selectedTnh.FileType == (int)FieleTypeEnum.Picture)
                    {
                        BitmapImage imagesouce = new BitmapImage();
                        imagesouce = new BitmapImage(new Uri(selectedTnh.FilePath));//Uri("图片路径")
                        MyImage.Source = imagesouce.Clone();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.DataContext = viewModel;
        }
    }
    public enum FieleTypeEnum
    {
        Folder,//文件夹
        Picture,//图片
        IniFile,//ini文件
        OtherFile//其它文件
    }
    public class FileTreeModel
    {
        public string FileName { get; set; }//名称
        public string FilePath { get; set; }//路径
        public int FileType { get; set; }//文件类型
        public string Icon { get; set; }//图标
        public List<FileTreeModel> Subitem { get; set; }//子项
        public string SubitemCount { get; set; }//子项数目
        public FileTreeModel()
        {
            Subitem = new List<FileTreeModel>();
        }
    }
    internal class PropertyNodeItem
    {
        public string Icon { get; set; }
        public string EditIcon { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }

        public List<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem()
        {
            Children = new List<PropertyNodeItem>();
        }
    }
    public class MainWindowViewModel
    {
        public VModel Model { get; set; }
        public MainWindowViewModel()
        {
            Model = new VModel();
        }
    }
    public class VModel
    {
        public int SelectFileleName { get; set; }
    }
    public class Student : BindableObject
    {
        private int _id = 0;
        private string _name = "";
        private int _age = 0;
        public int ID { get { return _id; } set { SetProperty(ref _id, value); } }
        public string Name { get { return _name; } set { SetProperty(ref _name, value); } }
        public int Age { get { return _age; } set { SetProperty(ref _age, value); } }
    }
    public abstract class BindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                OnPropertyChanged(propertyName);
            }
        }
    }

}
