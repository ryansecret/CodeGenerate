namespace Service.ViewModel
{
    public class TableInfoVm:BaseVm
    {
        private bool _isCheck;
      
        public string TableName { get; set; }



        public bool IsCheck
        {
            get { return _isCheck; }
            set
            {
                _isCheck = value;
                OnPropertyChanged("IsCheck");
            }
        }
         
       
    }
}