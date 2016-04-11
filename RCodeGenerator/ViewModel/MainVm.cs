using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Win32;
using RCodeGenerator.Template;
using RCodeGenerator.Utility;
using Repository;
using Service;
using Service.ViewModel;
using Repository = RCodeGenerator.Template.Repository;

namespace RCodeGenerator.ViewModel
{
    public class MainVm:BaseVm
    {
        private readonly DbService _dbService;
        public MainVm(DbService dbService)
        {
           
            NameSpace = Properties.Settings.Default.codenamespace;
            CodePath = Properties.Settings.Default.path;
            UsingSpace = Properties.Settings.Default.usingspace;
            RepositoryNameSapce = Properties.Settings.Default.repositorynamespace;
            RepositoryUsingArea = Properties.Settings.Default.rpusingArea;
            TableInfoVms = dbService.GetTableName(Properties.Settings.Default.dbname);
            this._dbService = dbService;
        }

        public bool CheckAll
        {
            get { return _checkAll; }
            set
            {
                _checkAll = value;
                TableInfoVms.ForEach(d => d.IsCheck=value);
            }
        }
 

        public List<TableInfoVm> TableInfoVms
        {
            get { return _tableInfoVms; }
            set
            {
                _tableInfoVms = value;
                OnPropertyChanged("TableInfoVms");
            }
        }
         
        private string _nameSpace;
        private string _codePath;
        private ICommand _browseDir;
        private List<TableInfoVm> _tableInfoVms;
        private bool _checkAll;
        private ICommand _generateCommand;
        private string _usingNameSpace;
        private string _repositoryNameSapce;
        private string _repositoryUsingArea;

        public string NameSpace
        {
            get { return _nameSpace; }
            set
            {
                _nameSpace = value;
                Properties.Settings.Default.codenamespace = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged("NameSpace");
            }
        }

        public string RepositoryNameSapce
        {
            get { return _repositoryNameSapce; }
            set
            {
                _repositoryNameSapce = value;
                OnPropertyChanged("RepositoryNameSapce");
                Properties.Settings.Default.repositorynamespace = value;
                Properties.Settings.Default.Save();
            }
        }

        public string RepositoryUsingArea
        {
            get { return _repositoryUsingArea; }
            set
            {
                _repositoryUsingArea = value;
                OnPropertyChanged("RepositoryUsingArea");
                Properties.Settings.Default.rpusingArea = value;
                Properties.Settings.Default.Save();
            }
        }
         
        public string CodePath
        {
            get { return _codePath; }
            set
            {
               
                _codePath = value;
                Properties.Settings.Default.path = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged("CodePath");
            }
        }

        public string UsingSpace
        { 
            get { return _usingNameSpace; }
            set
            {
                _usingNameSpace = value;
                Properties.Settings.Default.usingspace = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged("UsingSpace");
            }
        }
         
        public ICommand BrowseDir
        {
            get { return _browseDir??(_browseDir=new RelayCommand(d =>
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();
                 
                if (result ==DialogResult.Cancel)
                {
                    return;
                }
                CodePath = dialog.SelectedPath.Trim();
            })); }
        }

        public ICommand GenerateCommand
        {
            get { return _generateCommand??(_generateCommand=new RelayCommand((d) =>
            {
                foreach (var tb in TableInfoVms.Where(t=>t.IsCheck))
                {
                    var columns = _dbService.GetColumnInfos(Properties.Settings.Default.dbname, tb.TableName);
                    Model model = new Model(tb.TableName,columns);
                    model.NameSpace = NameSpace;
                    model.UsingArea = UsingSpace;
                    var data = Encoding.UTF8.GetBytes(model.TransformText());
                    using (var file = File.Open(Path.Combine(CodePath,model.TableName+".cs"),FileMode.OpenOrCreate))
                    {
                        file.Write(data,0,data.Length);
                    }
                    
                    Template.Repository repository=new Template.Repository();
                    repository.TableName = tb.TableName;
                    repository.ColumnInfos = columns;
                    repository.NameSpace =RepositoryNameSapce;
                    repository.UsingArea = RepositoryUsingArea;
                    data = Encoding.UTF8.GetBytes(repository.TransformText());
                    using (var file=File.Open(Path.Combine(CodePath,model.TableName+"Repository.cs"),FileMode.OpenOrCreate))
                    {
                        file.Write(data, 0, data.Length);
                    }
                }
                MessageBox.Show("生成成功！");
            }, (d) =>  TableInfoVms.Any(a => a.IsCheck))); }
        
        }
    }
}