using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//RESERVED IN ORDER TO SUPPORT MULTIPLE USERS
namespace PassPortable
{
    [Serializable]
    public class UserData : ObservableObject
    {
        public ObservableCollection<User> Userdata {get;set;}
    }
}
